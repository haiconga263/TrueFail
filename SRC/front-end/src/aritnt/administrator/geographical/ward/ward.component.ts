import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { GeoService, Country, Province, District, Ward } from '../geo.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';
import { AppConsts } from 'src/core/constant/AppConsts';

@Component({
  selector: 'ward',
  templateUrl: './ward.component.html',
  styleUrls: ['./ward.component.css']
})
export class WardComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource: ArrayStore;
  selectedRows: number[];

  private allCountries : Country[] = [];
  private allProvinces : Province[] = [];
  private allDistricts : District[] = [];

  private currentSelectedCountryId: number = 0;
  private currentSelectedProvinceId: number = 0;
  private provinces: Province[] = [];
  private districts : District[] = [];

  private nonCharPattern = AppConsts.nonSpecialCharPattern;
  private vietnamesePattern = AppConsts.nonSpecialCharVietnamesePattern;

  constructor(
    injector: Injector,
    private geoSvc: GeoService
  ) {
    super(injector);

    this.geoSvc.getCountries().subscribe((result) => {
      if (result.result == ResultCode.Success) {
        this.allCountries = result.data;
      }
    });

    this.geoSvc.getProvinces().subscribe((result) => {
      if (result.result == ResultCode.Success) {
        this.allProvinces = result.data;
      }
    });

    this.geoSvc.getDistricts().subscribe((result) => {
      if (result.result == ResultCode.Success) {
        this.allDistricts = result.data;
      }
    });

    this.loadDatasource();
  }

  loadDatasource(callback: () => void = null) {
    this.geoSvc.getWards().subscribe((result) => {
      if (result.result == ResultCode.Success) {

        this.dataSource = new ArrayStore({
          key: "id",
          data: result.data
        });

        if (FuncHelper.isFunction(callback))
          callback();
      }
    });
  }

  grid = {
    delete: async () => {
      console.log('delete');
      let rs = await this.geoSvc.deleteWard(this.selectedRows[0]).toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.dataGrid.instance.removeRow(this.dataGrid.instance.getRowIndexByKey(this.selectedRows[0]));
      }
      else
      {
        this.showError(rs.errorMessage);
      }
    },
    refresh: () => {
      this.loadDatasource(() => {
        this.dataGrid.instance.refresh();
      });
    },

    showUpdate: async () => {
      console.log('show update');
      let rowIdx = this.dataGrid.instance.getRowIndexByKey(this.selectedRows[0]);
      this.dataGrid.instance.editRow(rowIdx);
      this.dataSource.byKey(this.selectedRows[0]).then((rs) => {
        this.currentSelectedCountryId = rs.countryId;
        this.currentSelectedProvinceId = rs.provinceId;
      });
    },
    update: async (data: any) => {
      console.log('update');
      console.log(data);
      let model = data['oldData'];
      for(var k in data.newData)
      {
        model[k] = data.newData[k];
      }    
      data.cancel = true;
      let rs = await this.geoSvc.updateWard(model).toPromise();
      if(rs.result == ResultCode.Success)
      {
        //alert
        data.cancel = false;
        this.showSuccess('Common.UpdateSuccess');
        this.loadDatasource(() => {
          this.dataGrid.instance.refresh();
        });

        this.provinces = this.allProvinces;
        this.districts = this.allDistricts;
        this.currentSelectedCountryId = 0;
        this.currentSelectedProvinceId = 0;
      }
      else
      {
        this.showError(rs.errorMessage);
      } 
    },
    create: async (data: any) => {
      console.log('create');
      console.log(data);
      let model = new Ward();
      model.code =  data['data']['code']; 
      model.name =  data['data']['name']; 
      model.countryId =  data['data']['countryId']; 
      model.provinceId =  data['data']['provinceId'];
      model.districtId =  data['data']['districtId'];
      model.isUsed =  data['data']['isUsed'];
      data['data']['id'] = 0;     
      data.cancel = true;
      let rs = await this.geoSvc.addWard(model).toPromise();
      if(rs.result == ResultCode.Success)
      {
        //alert
        data.cancel = false;
        this.showSuccess('Common.AddSuccess');
        this.loadDatasource(() => {
          this.dataGrid.instance.refresh();
        });

        this.provinces = this.allProvinces;
        this.districts = this.allDistricts;
        this.currentSelectedCountryId = 0;
        this.currentSelectedProvinceId = 0;
      }
      else
      {
        this.showError(rs.errorMessage);
      }      
    },
    showCreate: () => {
      console.log('show create');
      this.dataGrid.instance.addRow();
      this.currentSelectedCountryId = 0;
      this.currentSelectedProvinceId = 0;
    },

    optionChanged(event, component: WardComponent) {
      console.log(event);
      if(event.row.rowType == 'detail')
      {
        if(component.currentSelectedCountryId != event.row.data.countryId)
        {
          component.currentSelectedCountryId = event.row.data.countryId;
          component.provinces = component.allProvinces.filter(r => r.countryId == event.row.data.countryId);
        }

        if(component.currentSelectedProvinceId != event.row.data.provinceId)
          {
            component.currentSelectedProvinceId = event.row.data.provinceId;
            component.districts = component.allDistricts.filter(r => r.provinceId == event.row.data.provinceId);
          }
      }
      else if(event.row.rowType == 'data')
      {
        component.provinces = component.allProvinces;
        component.districts = component.allDistricts;
        component.currentSelectedCountryId = 0;
        component.currentSelectedProvinceId = 0;
      }
    }
  };

  ngOnInit() {
  }
}
