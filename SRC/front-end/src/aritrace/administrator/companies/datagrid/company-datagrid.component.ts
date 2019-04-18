import { Component, OnInit, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { AppUrlConsts } from 'src/aritrace/common/app-constants';
import { BaseImplementorComponent } from 'src/core/build-implementor/base-implementor.component';
import { CompanyType } from 'src/aritrace/common/models/company-type.model';
import { CommonService } from 'src/aritrace/common/services/common.service';

@Component({
  selector: 'country-datagrid',
  templateUrl: '../../../../core/build-implementor/base-implementor.component.html',
  styleUrls: ['../../../../core/build-implementor/base-implementor.component.css']
})
export class CompanyDatagridComponent extends BaseImplementorComponent {
  companyTypes: CompanyType[];

  constructor(
    injector: Injector,
    public commonSvc: CommonService,
  ) {
    super(injector);

    this.setUrlApiRoot(AppUrlConsts.urlApiCompany);
    this.setUrlDetail(AppUrlConsts.urlCompanyDetail);

  }
  
  async init() {

    this.companyTypes = (await this.commonSvc.getCompanyTypes()).data;

    this.configColumns(
      //{ dataField: 'logoPath', dataType: 'string' },
      { dataField: 'id', dataType: 'string' },
      { dataField: 'name', dataType: 'string' },
      { dataField: 'taxCode', dataType: 'string' },
      { dataField: 'website', dataType: 'string' },
      //{ dataField: 'contactId', dataType: 'string' },
      //{ dataField: 'addressId', dataType: 'string' },
      { dataField: 'description', dataType: 'string', visible: false },
      {
        dataField: 'companyTypeId',
        caption: 'Type',
        lookup: {
          dataSource: this.companyTypes,
          placeholder: 'Select country',
          displayExpr: 'name',
          valueExpr: 'id',
        },
      },
      { dataField: 'isPartner', dataType: 'boolean' },
      { dataField: 'gS1Code', dataType: 'string', caption: 'GS1 code' },
      { dataField: 'isUsed', dataType: 'boolean' },
      //{ dataField: 'createdDate', dataType: 'datetime' },
      //{ dataField: 'createdBy', dataType: 'string' },
      //{ dataField: 'modifiedDate', dataType: 'datetime' },
      //{ dataField: 'modifiedBy', dataType: 'string' },
    );

    this.initialize();
  }

  ngOnInit() {
    super.ngOnInit();
    this.init();
  }
}
