import { Component, Injector } from '@angular/core';
import { BaseImplementorComponent } from 'src/core/build-implementor/base-implementor.component';
import { AppUrlConsts } from 'src/aritrace/common/app-constants';
import { Country } from 'src/aritrace/common/models/country.model';
import { CommonService } from 'src/aritrace/common/services/common.service';
import { Region } from 'src/aritrace/common/models/region.model';
import { SelectListItem } from 'src/core/models/input.model';

@Component({
  selector: 'province',
  templateUrl: '../../../../../core/build-implementor/base-implementor.component.html',
  styleUrls: ['../../../../../core/build-implementor/base-implementor.component.css']
})
export class ProvinceComponent extends BaseImplementorComponent {

  countries: Country[];
  allRegions: Region[];
  constructor(
    injector: Injector,
    public commonSvc: CommonService,
  ) {
    super(injector);
    this.loadRegions = this.loadRegions.bind(this);
    this.setUrlApiRoot(AppUrlConsts.urlApiProvince);
  }

  async init() {

    this.countries = (await this.commonSvc.getCountries()).data;
    this.allRegions = (await this.commonSvc.getRegions()).data;

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
        }],
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
        dataField: 'phoneCode',
        dataType: 'string',
        validationRules: [{
          type: 'required',
          message: 'Phone code is required'
        }],
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
          placeholder: 'Select country',
          displayExpr: 'name',
          valueExpr: 'id',
        },
        setCellValue: function (rowData, value) {
          rowData.regionId = null;
          this.defaultSetCellValue(rowData, value);
        },
      },
      {
        dataField: 'regionId',
        dataType: 'string',
        caption: 'Region',
        lookup: {
          dataSource: this.loadRegions,
          placeholder: 'Select region',
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

  loadRegions(options: any): Region[] {
    if (options.data) {
      return this.allRegions.filter(x => x.countryId == options.data.countryId);
    }
    return this.allRegions;
  }

  ngOnInit() {
    super.ngOnInit();
    this.init();
  }
}
