import { Component, Injector } from '@angular/core';
import { BaseImplementorComponent } from 'src/core/build-implementor/base-implementor.component';
import { AppUrlConsts } from 'src/aritrace/common/app-constants';
import { Country } from 'src/aritrace/common/models/country.model';
import { CommonService } from 'src/aritrace/common/services/common.service';
import { Province } from 'src/aritrace/common/models/province.model';
import { District } from 'src/aritrace/common/models/district.model';

@Component({
  selector: 'ward',
  templateUrl: '../../../../../core/build-implementor/base-implementor.component.html',
  styleUrls: ['../../../../../core/build-implementor/base-implementor.component.css']
})
export class WardComponent extends BaseImplementorComponent {

  allCountries: Country[];
  allProvinces: Province[];
  allDistricts: District[];

  constructor(
    injector: Injector,
    public commonSvc: CommonService,
  ) {
    super(injector);
    this.loadProvinces = this.loadProvinces.bind(this);
    this.loadDistricts = this.loadDistricts.bind(this);

    this.setUrlApiRoot(AppUrlConsts.urlApiWard);
  }

  async init() {

    this.allCountries = (await this.commonSvc.getCountries()).data;
    this.allProvinces = (await this.commonSvc.getProvinces()).data;
    this.allDistricts = (await this.commonSvc.getDistricts()).data;

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
        setCellValue: function (rowData, value) {
          rowData.districtId = null;
          this.defaultSetCellValue(rowData, value);
        },
      },
      {
        dataField: 'districtId',
        dataType: 'string',
        caption: 'District',
        validationRules: [{
          type: 'required',
          message: 'District is required'
        }],
        lookup: {
          dataSource: this.loadDistricts,
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

  loadDistricts(options: any): District[] {
    if (options.data) {
      return this.allDistricts.filter(x => x.provinceId == options.data.provinceId);
    }
    return this.allDistricts;
  }

  ngOnInit() {
    super.ngOnInit();
    this.init();
  }
}
