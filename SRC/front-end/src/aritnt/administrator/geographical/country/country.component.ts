import { Component, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { GeoService, Country } from '../geo.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';
import { AppConsts } from 'src/core/constant/AppConsts';

@Component({
  selector: 'country',
  templateUrl: './country.component.html',
  styleUrls: ['./country.component.css']
})
export class CountryComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource: ArrayStore;
  selectedRows: number[];

  private nonCharPattern = AppConsts.nonSpecialCharPattern;
  private vietnamesePattern = AppConsts.nonSpecialCharVietnamesePattern;
  private numberPattern = AppConsts.numberPattern;

  constructor(
    injector: Injector,
    private geoSvc: GeoService
  ) {
    super(injector);
    this.loadDatasource();
  }

  loadDatasource(callback: () => void = null) {
    this.geoSvc.getCountries().subscribe((result) => {
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
      let rs = await this.geoSvc.deleteCountry(this.selectedRows[0]).toPromise();
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
      let rs = await this.geoSvc.updateCountry(model).toPromise();
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
      let model = new Country();
      model.code =  data['data']['code']; 
      model.name =  data['data']['name']; 
      model.phoneCode =  data['data']['phoneCode']; 
      model.isUsed =  data['data']['isUsed'];
      data['data']['id'] = 0;     
      data.cancel = true;
      let rs = await this.geoSvc.addCountry(model).toPromise();
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
