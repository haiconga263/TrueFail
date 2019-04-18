import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { GeoService, Country, Province, Region } from '../geo.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';
import { ReturnStatement } from '@angular/compiler';
import { AppConsts } from 'src/core/constant/AppConsts';

@Component({
  selector: 'province',
  templateUrl: './province.component.html',
  styleUrls: ['./province.component.css']
})
export class ProvinceComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource: ArrayStore;
  selectedRows: number[];

  private allCountries : Country[] = [];
  private allRegions : Region[] = [];

  private currentSelectedCountryId: number = 0;
  private regions: Region[] = [];

  private nonCharPattern = AppConsts.nonSpecialCharPattern;
  private vietnamesePattern = AppConsts.nonSpecialCharVietnamesePattern;
  private numberPattern = AppConsts.numberPattern;

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

    this.geoSvc.getRegions().subscribe((result) => {
      if (result.result == ResultCode.Success) {
        this.allRegions = result.data;
      }
    });

    this.loadDatasource();
  }

  loadDatasource(callback: () => void = null) {
    this.geoSvc.getProvinces().subscribe((result) => {
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
      let rs = await this.geoSvc.deleteProvince(this.selectedRows[0]).toPromise();
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

    showUpdate: () => {
      console.log('show update');
      let rowIdx = this.dataGrid.instance.getRowIndexByKey(this.selectedRows[0]);
      this.dataGrid.instance.editRow(rowIdx);
      this.dataSource.byKey(this.selectedRows[0]).then((rs) => {
        this.currentSelectedCountryId = rs.countryId;
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
      let rs = await this.geoSvc.updateProvince(model).toPromise();
      if(rs.result == ResultCode.Success)
      {
        //alert
        data.cancel = false;
        this.showSuccess('Common.UpdateSuccess');
        this.loadDatasource(() => {
          this.dataGrid.instance.refresh();
        });

        this.regions = this.allRegions;
        this.currentSelectedCountryId = 0;
      }
      else
      {
        this.showError(rs.errorMessage);
      } 
    },
    create: async (data: any) => {
      console.log('create');
      console.log(data);
      let model = new Province();
      model.code =  data['data']['code']; 
      model.name =  data['data']['name']; 
      model.countryId =  data['data']['countryId']; 
      model.regionId =  data['data']['regionId']
      model.isUsed =  data['data']['isUsed'];
      data['data']['id'] = 0;     
      data.cancel = true;
      let rs = await this.geoSvc.addProvince(model).toPromise();
      if(rs.result == ResultCode.Success)
      {
        //alert
        data.cancel = false;
        this.showSuccess('Common.AddSuccess');
        this.loadDatasource(() => {
          this.dataGrid.instance.refresh();
        });

        this.regions = this.allRegions;
        this.currentSelectedCountryId = 0;
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
    },

    optionChanged(event, component: ProvinceComponent) {
      console.log(event);
      if(event.row.rowType == 'detail')
      {
        if(component.currentSelectedCountryId != event.row.data.countryId)
        {
          component.currentSelectedCountryId = event.row.data.countryId;
          component.regions = component.allRegions.filter(r => r.countryId == event.row.data.countryId);
        }
      }
      else if(event.row.rowType == 'data')
      {
        component.regions = component.allRegions;
        component.currentSelectedCountryId = 0;
      }
    }
  };

  ngOnInit() {
  }
}
