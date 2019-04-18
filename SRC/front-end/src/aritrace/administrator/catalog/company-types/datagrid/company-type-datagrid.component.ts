import { Component, Injector } from '@angular/core';
import { BaseImplementorComponent } from 'src/core/build-implementor/base-implementor.component';
import { AppUrlConsts } from 'src/aritrace/common/app-constants';

@Component({
  selector: 'companyType',
  templateUrl: '../../../../../core/build-implementor/base-implementor.component.html',
  styleUrls: ['../../../../../core/build-implementor/base-implementor.component.css']
})
export class CompanyTypeDatagridComponent extends BaseImplementorComponent {
  constructor(
    injector: Injector
  ) {
    super(injector);

    this.setUrlApiRoot(AppUrlConsts.urlApiCompanyType);
    this.setUrlDetail(AppUrlConsts.urlCompanyTypeDetail);

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
        dataField: 'code',
        dataType: 'string',
        validationRules: [{
          type: 'required',
          message: 'Code is required'
        }]
      },
      {
        dataField: 'name',
        dataType: 'string',
        validationRules: [{
          type: 'required',
          message: 'Name is required'
        }]
      },
      {
        dataField: 'isUsed',
        dataType: 'boolean',
      },
    );

    this.initialize();
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
