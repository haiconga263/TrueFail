import { Component, Injector } from '@angular/core';
import { BaseImplementorComponent } from 'src/core/build-implementor/base-implementor.component';
import { AppUrlConsts } from 'src/aritrace/common/app-constants';
import { Country } from 'src/aritrace/common/models/country.model';
import { CommonService } from 'src/aritrace/common/services/common.service';
import { Province } from 'src/aritrace/common/models/province.model';
import { Setting } from 'src/aritrace/common/models/setting.model';

@Component({
  selector: 'setting',
  templateUrl: '../../../../core/build-implementor/base-implementor.component.html',
  styleUrls: ['../../../../core/build-implementor/base-implementor.component.css']
})
export class SettingComponent extends BaseImplementorComponent {

  allCountries: Country[];
  allProvinces: Province[];

  constructor(
    injector: Injector,
    public commonSvc: CommonService,
  ) {
    super(injector);
    this.setUrlApiRoot(AppUrlConsts.urlApiSetting);
  }

  async init() {

    this.allCountries = (await this.commonSvc.getCountries()).data;
    this.allProvinces = (await this.commonSvc.getProvinces()).data;

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
        dataField: 'name',
        dataType: 'string',
        validationRules: [{
          type: 'required',
          message: 'Name is required'
        }]
      },
      {
        dataField: 'value',
        dataType: 'string',
        validationRules: [{
          type: 'required',
          message: 'Value is required'
        }]
      },
      {
        dataField: 'desciption',
        dataType: 'string',
      }
    );

    this.initialize();
  }

  ngOnInit() {
    super.ngOnInit();
    this.init();
  }
}
