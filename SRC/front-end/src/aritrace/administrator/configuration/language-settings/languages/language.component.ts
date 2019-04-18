import { Component, Injector } from '@angular/core';
import { BaseImplementorComponent } from 'src/core/build-implementor/base-implementor.component';
import { AppUrlConsts } from 'src/aritrace/common/app-constants';
import { Country } from 'src/aritrace/common/models/country.model';
import { CommonService } from 'src/aritrace/common/services/common.service';

@Component({
  selector: 'language',
  templateUrl: '../../../../../core/build-implementor/base-implementor.component.html',
  styleUrls: ['../../../../../core/build-implementor/base-implementor.component.css']
})
export class LanguageComponent extends BaseImplementorComponent {

  allCountries: Country[];

  constructor(
    injector: Injector,
    public commonSvc: CommonService,
  ) {
    super(injector);
    this.setUrlApiRoot(AppUrlConsts.urlApiLanguage);
  }

  async init() {

    this.allCountries = (await this.commonSvc.getCountries()).data;
    this.allCountries.unshift(new Country({ id: null, name: '-- none --' }));

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
        dataField: 'classIcon',
        dataType: 'string',
        validationRules: [{
          type: 'required',
          message: 'Name is required'
        }]
      },
      {
        dataField: 'countryId',
        dataType: 'string',
        lookup: {
          dataSource: this.allCountries,
          displayExpr: 'name',
          valueExpr: 'id',
        },
      },
      {
        dataField: 'desciption',
        dataType: 'string',
      },
      {
        dataField: 'isUsed',
        dataType: 'boolean',
      }
    );

    this.initialize();
  }

  ngOnInit() {
    super.ngOnInit();
    this.init();
  }
}
