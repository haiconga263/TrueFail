import { Component, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { EmployeeService, Employee } from '../employee.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from '../../app-url';
import { ActivatedRoute, Params } from '@angular/router';
import { ResultModel } from 'src/core/models/http.model';
import { AppConsts } from 'src/core/constant/AppConsts'
import { User, UserService } from '../../user/user.service';

@Component({
  selector: 'employee-detail',
  templateUrl: './employee-detail.component.html',
  styleUrls: ['./employee-detail.component.css']
})
export class EmployeeDetailComponent extends AppBaseComponent {
  private params: Params;
  private type: string = 'add';
  private id: number = 0;

  private employee: Employee = new Employee();
  private genders: any[] = [];

  private managers: Employee[] = null;
  private users: User[] = null;

  private isNameValid: boolean = false;
  private isEmailValid: boolean = false;
  private isPhoneValid: boolean = false;
  private isGenderValid: boolean = false;
  private isJobTitleValid: boolean = false;

  private phonePattern = AppConsts.vietnamPhonePattern;
  private emailPattern = AppConsts.emailPattern;
  private vietnamesePattern = AppConsts.nonSpecialCharVietnamesePattern;

  //Language
  private modifyLanguages: any[] = [];

  private now: Date = new Date(Date.now());

  constructor(
    injector: Injector,
    private empSvc: EmployeeService,
    private useSvc: UserService,
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
    this.genders = this.empSvc.getGenders();
    if (this.params['type'] == 'update') {
      await this.loadDatasource();
    }
  }

  private async loadDatasource() {

    var rs = await this.empSvc.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.employee = rs.data;
      this.employee.imageData = AppConsts.imageDataUrl + this.employee.imageURL;

      if(this.employee.userId != null && this.employee.userId != 0 && this.users == null)
      {
        this.useSvc.getUser(this.employee.userId).subscribe((result) => {
          if(rs.result == ResultCode.Success)
          {
            this.users = [];
            this.users.push(result.data);
          }
        });
      }

      if(this.employee.reportTo != null && this.employee.userId != 0&& this.managers == null)
      {
        this.empSvc.get(this.employee.reportTo).subscribe((result) => {
          if(rs.result == ResultCode.Success)
          {
            this.managers = [];
            this.managers.push(result.data);
          }
        });
      }
    }
  }
  avatarChangeEvent(fileInput: any, component: EmployeeDetailComponent) {
    if (fileInput.target.files && fileInput.target.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e: any) {
            component.employee.imageData = e.target.result;
        }
        reader.readAsDataURL(fileInput.target.files[0]);
    }
  }

  private return() {
    console.log('return');
    this.router.navigate([appUrl.employeeList]);
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    console.log('save');
    console.log(this.employee);
    if (!this.checkValid()) {
      return;
    }

    if(this.type == "update")
    {
      this.empSvc.update(this.employee).subscribe((result: ResultModel<any>) => {
        if(result.result == ResultCode.Success)
        {
          //alert
          this.showSuccess(this.lang.instant('Common.UpdateSuccess'));
          this.return();
        }
        else
        {
          //alert
          this.showError(result.errorMessage);
        }
      });
    }
      else{
        this.empSvc.add(this.employee).subscribe((result: ResultModel<any>) => {
          if(result.result == ResultCode.Success)
          {
            //alert
            this.showSuccess(this.lang.instant('Common.AddSuccess'));
            this.return();
          }
          else
          {
            //alert
            this.showError(result.errorMessage);
          }
        });
      }
  }

  private async managerFocusIn(event: any) {
    if(this.managers == null)
    {
      var rs = await this.empSvc.gets().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.managers = rs.data;
      }
    }
    else if(this.managers.length == 1 && this.managers[0].id == this.employee.reportTo)
    {

      var rs = await this.empSvc.gets().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.managers = rs.data;
      }
    }
  }

  private async userFocusIn(event: any) {
    if(this.users == null)
    {
      var rs = await this.useSvc.getUsersNotAssignBy(false, "").toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.users = rs.data;
      }
    }
    else if(this.users.length == 1 && this.users[0].id == this.employee.userId)
    {
      var currentUser = this.users[0];
      var rs = await this.useSvc.getUsersNotAssignBy(false, "").toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.users = rs.data;
        if(this.users.find(u => u.id == currentUser.id) == null)
        {
          this.users.push(currentUser);
        }
      }
    }
  }

  private checkValid() : boolean{
    return this.isNameValid && this.isPhoneValid && this.isEmailValid && this.isGenderValid && this.isJobTitleValid;
  }
}
