import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { UserService, User, Role } from '../user.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from '../../app-url';
import { ActivatedRoute, Params } from '@angular/router';
import { ResultModel } from 'src/core/models/http.model';
import { AppConsts } from 'src/core/constant/AppConsts';

@Component({
  selector: 'user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent extends AppBaseComponent {
  private params: Params;
  private type: string = 'add';
  private id: number = 0;

  selectedRows: Role[] = [];

  private user: User = new User();
  private roles: Role[] = [];

  private isUserNameValid: boolean = false;
  private isEmailValid: boolean = false;

  //Language
  private modifyLanguages: any[] = [];
  
  private userNamePattern = AppConsts.userNamePattern;
  private passwordPattern = AppConsts.nonSpecialCharPattern;

  constructor(
    injector: Injector,
    private userSvc: UserService,
    private activatedRoute: ActivatedRoute
  ) {
    super(injector);
      this.activatedRoute.queryParams.subscribe((params: Params) => {
        this.params = params;
        if (this.params['type'] == 'update') {
          this.type = 'update';
          this.id = this.params["id"];
        }
      });
      this.Init();
  }

  async Init()
  {
    let rolesRs = await this.userSvc.getRoles(0).toPromise();
    if(rolesRs.result == ResultCode.Success)
    {
      this.roles = rolesRs.data;
    }

    if (this.params['type'] == 'update') {
      await this.loadDatasource();
    }
  }

  private async loadDatasource() {

    var rs = await this.userSvc.getUserWithRole(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.user = rs.data;
      this.user.password = "******"; //Dump data
    }
  }

  private return() {
    console.log('return');
    this.router.navigate([appUrl.userList]);
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    console.log('save');
    console.log(this.user);
    if (!this.checkValid()) {
      return;
    }

    if(this.type == "update")
    {
      this.userSvc.updateUser(this.user).subscribe((result: ResultModel<any>) => {
        if(result.result == ResultCode.Success)
        {
          //alert
          this.showSuccess(this.lang.instant('Common.UpdateSuccess'));
          this.return();
        }
        else
        {
          //alert
          this.showError(this.lang.instant(result.errorMessage));
        }
      });
    }
      else{
        this.userSvc.addUser(this.user).subscribe((result: ResultModel<any>) => {
          if(result.result == ResultCode.Success)
          {
            //alert
            this.showSuccess(this.lang.instant('Common.AddSuccess'));
            this.return();
          }
          else
          {
            //alert
            this.showError(this.lang.instant(result.errorMessage));
          }
        });
      }
  }

  private saveRole() {
    console.log('save');
    console.log(this.user.roles);

    let rolesIds: number[] = [];
    this.user.roles.forEach(role => {
      rolesIds.push(role.id);
    });

    this.userSvc.saveRoles(this.user.id, rolesIds).subscribe((result: ResultModel<any>) => {
      if(result.result == ResultCode.Success)
      {
        //alert
        this.showSuccess("User.SaveRoleSuccess");
      }
      else
      {
        //alert
        this.showError("User.SaveRoleFail");
      }
    });
  }

  private deleteRole() {
    console.log(this.selectedRows[0]);
    let roles = [];
    this.user.roles.forEach((role, index) => {
      if(role.id != this.selectedRows[0].id)
      {
        roles.push(role);
      }
    });

    this.user.roles = roles;
  }

  private checkRoles(event) {
    console.log(event);
    if(this.user.roles.find(r => r.id == event.data.id) != null)
    {
      event.cancel = true;
      //alert
      this.showError("User.DuplicateRole");
    }
  }

  private checkValid() : boolean{
    return this.isUserNameValid && this.isEmailValid;
  }


  ngOnInit() {
  }
}
