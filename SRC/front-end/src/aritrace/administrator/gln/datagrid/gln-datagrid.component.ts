import { Component, Injector } from '@angular/core';
import { BaseImplementorComponent } from 'src/core/build-implementor/base-implementor.component';
import { AppUrlConsts } from 'src/aritrace/common/app-constants';
import { Company } from '../../../common/models/company.model';
import { CommonService } from '../../../common/services/common.service';
import { Country } from 'src/aritrace/common/models/country.model';

@Component({
  selector: 'gln',
  templateUrl: '../../../../core/build-implementor/base-implementor.component.html',
  styleUrls: ['../../../../core/build-implementor/base-implementor.component.css']
})
export class GLNDatagridComponent extends BaseImplementorComponent {
  companies: Company[];
  allCountries: Country[];
  constructor(
    injector: Injector,
    public commonSvc: CommonService,
  ) {
    super(injector);

    this.setUrlApiRoot(AppUrlConsts.urlApiGLN);
    this.setUrlDetail(AppUrlConsts.urlApiGLNDetail);
  }
  async init() {
    this.companies = (await this.commonSvc.getCompanies()).data;
    this.allCountries = (await this.commonSvc.getCountries()).data;
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
        dataField: 'partnerId',
        caption: 'Partner name',
        lookup: {
          dataSource: this.companies,
          placeholder: 'Select company',
          displayExpr: 'name',
          valueExpr: 'id',
        },
      },
      {
        dataField: 'countryId',
        caption: 'Country name',
        lookup: {
          dataSource: this.allCountries,
          placeholder: 'Select country',
          displayExpr: 'name',
          valueExpr: 'id',
        },
      },
      {
        dataField: 'is_used',
        dataType: 'boolean',
      },
      {
        dataField: 'usedDate',
        dataType: 'datetime',
      },
    );
    this.initialize();
  }

  ngOnInit() {
    super.ngOnInit();
    this.init();
  }
}
