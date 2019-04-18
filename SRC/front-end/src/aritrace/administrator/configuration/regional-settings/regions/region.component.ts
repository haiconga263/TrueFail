import { Component, Injector } from '@angular/core';
import { BaseImplementorComponent } from 'src/core/build-implementor/base-implementor.component';
import { AppUrlConsts } from 'src/aritrace/common/app-constants';
import { Country } from 'src/aritrace/common/models/country.model';
import { CommonService } from 'src/aritrace/common/services/common.service';

@Component({
  selector: 'region',
  templateUrl: '../../../../../core/build-implementor/base-implementor.component.html',
  styleUrls: ['../../../../../core/build-implementor/base-implementor.component.css']
})
export class RegionComponent extends BaseImplementorComponent {

  countries: Country[];
  constructor(
    injector: Injector,
    public commonSvc: CommonService,
  ) {
    super(injector);

    this.setUrlApiRoot(AppUrlConsts.urlApiRegion);
  }

  async init() {

    this.countries = (await this.commonSvc.getCountries()).data;

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
        dataField: 'countryId',
        dataType: 'string',
        caption: 'Country',
        validationRules: [{
          type: 'required',
          message: 'Country is required'
        }],
        lookup: {
          dataSource: this.countries,
          displayExpr: 'name',
          valueExpr: 'id',
        },
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
    this.init();
  }
}
