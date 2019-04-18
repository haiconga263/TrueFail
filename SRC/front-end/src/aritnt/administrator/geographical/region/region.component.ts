import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { GeoService, Country, Region } from '../geo.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';
import { AppConsts } from 'src/core/constant/AppConsts';

@Component({
  selector: 'region',
  templateUrl: './region.component.html',
  styleUrls: ['./region.component.css']
})
export class RegionComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource: ArrayStore;
  selectedRows: number[];

  private countries : Country[] = [];

  private nonCharPattern = AppConsts.nonSpecialCharPattern;
  private vietnamesePattern = AppConsts.nonSpecialCharVietnamesePattern;

  constructor(
    injector: Injector,
    private geoSvc: GeoService
  ) {
    super(injector);

    this.geoSvc.getCountries().subscribe((result) => {
      if (result.result == ResultCode.Success) {
        this.countries = result.data;
      }
    });

    this.loadDatasource();
  }

  loadDatasource(callback: () => void = null) {
    this.geoSvc.getRegions().subscribe((result) => {
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
      let rs = await this.geoSvc.deleteRegion(this.selectedRows[0]).toPromise();
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
      let rs = await this.geoSvc.updateRegion(model).toPromise();
      if(rs.result == ResultCode.Success)
      {
        //alert
        data.cancel = false;
        this.showSuccess('Common.UpdateSuccess');
        this.loadDatasource(() => {
          this.dataGrid.instance.refresh();
        });
      }
      else
      {
        this.showError(rs.errorMessage);
      } 
    },
    create: async (data: any) => {
      console.log('create');
      console.log(data);
      let model = new Region();
      model.code =  data['data']['code']; 
      model.name =  data['data']['name']; 
      model.countryId =  data['data']['countryId']; 
      model.isUsed =  data['data']['isUsed'];   
      data.cancel = true;
      let rs = await this.geoSvc.addRegion(model).toPromise();
      if(rs.result == ResultCode.Success)
      {
        //alert
        data.cancel = false;
        this.showSuccess('Common.AddSuccess');
        this.loadDatasource(() => {
          this.dataGrid.instance.refresh();
        });
      }
      else
      {
        this.showError(rs.errorMessage);
      }      
    },
    showCreate: () => {
      console.log('show create');
      this.dataGrid.instance.addRow();
    },
  };

  ngOnInit() {
  }
}
