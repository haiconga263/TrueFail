import { Component, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from '../../app-url';
import { ActivatedRoute, Params } from '@angular/router';
import { ResultModel } from 'src/core/models/http.model';
import { Language } from 'src/core/common/language.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { Contact, ContactService, Status } from '../contact.service';

@Component({
  selector: 'contact-detail',
  templateUrl: './contact-detail.component.html',
  styleUrls: ['./contact-detail.component.css']
})
export class ContactDetailComponent extends AppBaseComponent {
  private params: Params;
  private type: string = 'add';
  private id: number = 0;
  private contact: Contact = new Contact();


  constructor(
    injector: Injector,
    private contactSvc: ContactService,
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

  async Init() {
    if (this.params['type'] == 'update') {
      await this.loadDatasource();
    }
  }

  private async loadDatasource() {

    var rs = await this.contactSvc.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.contact = rs.data;
      this.contactSvc.update(this.contact).toPromise();
      debugger
    }
  }

  private return() {
    console.log('return');
    this.router.navigate([appUrl.contactList]);
  }
  private sendClick(){
    alert("Chức năng chưa hoàn thiện");
  };
  private refresh() {
    this.loadDatasource();
  }

  private save() {
    console.log('save');
    console.log(this.contact);

    if (this.type == "update") {
      this.contactSvc.update(this.contact).subscribe((result: ResultModel<any>) => {
        if (result.result == ResultCode.Success) {
          //alert
          this.showSuccess(this.lang.instant('Common.UpdateSuccess'));
          this.return();
        }
        else {
          //alert
          this.showError(result.errorMessage);
        }
      });
    }
    else {
      this.contactSvc.add(this.contact).subscribe((result: ResultModel<any>) => {
        if (result.result == ResultCode.Success) {
          //alert
          this.showSuccess(this.lang.instant('Common.AddSuccess'));
          this.return();
        }
        else {
          //alert
          this.showError(result.errorMessage);
        }
      });
    }
  }

 
  ngOnInit() {
  }
}
