import { Component, Injector } from '@angular/core';
import { BaseImplementorComponent } from 'src/core/build-implementor/base-implementor.component';
import { AppUrlConsts } from 'src/aritrace/common/app-constants';
import { Country } from 'src/aritrace/common/models/country.model';
import { CommonService } from 'src/aritrace/common/services/common.service';
import { Province } from 'src/aritrace/common/models/province.model';
import { District } from 'src/aritrace/common/models/district.model';

@Component({
  selector: 'district',
  templateUrl: '../../../../../core/build-implementor/base-implementor.component.html',
  styleUrls: ['../../../../../core/build-implementor/base-implementor.component.css']
})
export class DistrictComponent extends BaseImplementorComponent {

  allCountries: Country[];
  allProvinces: Province[];

  constructor(
    injector: Injector,
    public commonSvc: CommonService,
  ) {
    super(injector);
    this.loadProvinces = this.loadProvinces.bind(this);
    this.setUrlApiRoot(AppUrlConsts.urlApiDistrict);
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
          dataSource: this.allCountries,
          displayExpr: 'name',
          valueExpr: 'id',
        },
        setCellValue: function (rowData, value) {
          rowData.provideId = null;
          this.defaultSetCellValue(rowData, value);
        },
      },
      {
        dataField: 'provinceId',
        dataType: 'string',
        caption: 'Province',
        validationRules: [{
          type: 'required',
          message: 'Province is required'
        }],
        lookup: {
          dataSource: this.loadProvinces,
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

  loadProvinces(options: any): Province[] {
    if (options.data) {
      return this.allProvinces.filter(x => x.countryId == options.data.countryId);
    }
    return this.allProvinces;
  }

  ngOnInit() {
    super.ngOnInit();
    this.init();
  }
}
