import { Component, Injector } from '@angular/core';
import { BaseImplementorComponent } from 'src/core/build-implementor/base-implementor.component';
import { AppUrlConsts } from 'src/aritrace/common/app-constants';
import { Role } from 'src/aritrace/common/models/role.model';
import { CommonService } from 'src/aritrace/common/services/common.service';
import { Company } from 'src/aritrace/common/models/company.model';

@Component({
  selector: 'account',
  templateUrl: '../../../core/build-implementor/base-implementor.component.html',
  styleUrls: ['../../../core/build-implementor/base-implementor.component.css']
})
export class AccountComponent extends BaseImplementorComponent {

  constructor(
    injector: Injector,
    public commonSvc: CommonService,
  ) {
    super(injector);

    this.setUrlApiRoot(AppUrlConsts.urlApiAccountPartner);
    this.comparisonPassword = this.comparisonPassword.bind(this);
  }

  async init() {


    this.onEditorPreparing = function (data: any) {
      if (data.parentType == "dataRow"
        && (data.dataField == "newPassword" || data.dataField == "confirmNewPassword"))
        data.editorOptions.mode = 'password';
    }

    this.configColumns(
      {
        dataField: 'id',
        dataType: 'string',
        width: 100,
        sortOrder: 'asc',
        alignment: 'center',
        formItem: { visible: false },
      },
      {
        dataField: 'userName',
        dataType: 'string',
        validationRules: [{
          type: 'required',
          message: 'UserName is required'
        }]
      },
      {
        dataField: 'email',
        dataType: 'string',
        validationRules: [{
          type: 'required',
          message: 'Email is required'
        }]
      },
      { dataField: 'newPassword', dataType: 'string', visible: false, },
      {
        dataField: 'confirmNewPassword',
        dataType: 'string',
        visible: false,
        validationRules: [{
          type: "compare",
          message: "'New Password' and 'Confirm New Password' do not match",
          comparisonTarget: this.comparisonPassword,
        }]
      },
      { dataField: 'isUsed', dataType: 'boolean', },
      { dataField: 'isActived', dataType: 'boolean', },
    );

    this.initialize();
  }

  comparisonPassword() {
    if (this.rowCurrent != null)
      return this.rowCurrent.row.data['newPassword'];
    return ''
  }

  ngOnInit() {
    super.ngOnInit();
    this.init();
  }
}
