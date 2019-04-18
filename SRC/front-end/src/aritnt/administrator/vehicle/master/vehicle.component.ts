import { Component, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { VehicleService } from '../vehicle.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';
import { appUrl } from '../../app-url';
import { AbivinService } from '../../common/services/abivin.service';

@Component({
  selector: 'vehicle',
  templateUrl: './vehicle.component.html',
  styleUrls: ['./vehicle.component.css']
})
export class VehicleComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource: ArrayStore;
  selectedRows: number[];

  menuItems = [
    {
      text: "Abivin",
      icon: "fa fa-file-excel-o",
      items: [
        { text: this.lang.instant("Admin.Vehicle"), action: 'vehicle' },
        { text: this.lang.instant("Admin.Vehicle.Type"), action: 'vehicle-type' },
      ]
    }
  ];

  constructor(
    injector: Injector,
    private vehSvc: VehicleService,
    private abvSvc: AbivinService
  ) {
    super(injector);
    this.loadDatasource();
  }

  loadDatasource(callback: () => void = null) {
    this.vehSvc.gets().subscribe((result) => {
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
      let rs = await this.vehSvc.delete(this.selectedRows[0]).toPromise();
      if (rs.result == ResultCode.Success) {
        this.dataGrid.instance.removeRow(this.dataGrid.instance.getRowIndexByKey(this.selectedRows[0]));
      }
      else {
        this.showError(this.lang.instant("Common.DeleteFail"));
        //alert
      }
    },
    refresh: () => {
      this.loadDatasource(() => {
        this.dataGrid.instance.refresh();
      });
    },
    update: () => {
      console.log('update');
      if (this.selectedRows.length == 1) {
        this.router.navigate([appUrl.vehicleDetail],
          {
            queryParams: {
              type: 'update',
              id: this.selectedRows[0]
            }
          });
      }
    },
    create: () => {
      console.log('create');
      this.router.navigate([appUrl.vehicleDetail],
        {
          queryParams: {
            type: 'add'
          }
        });
    }
  };

  itemClick(data: any) {
    console.log(data);
    if (data.itemData.action == 'vehicle') {
      this.abvSvc.downloadVehicle();
    }
    else if (data.itemData.action == 'vehicle-type') {
      this.abvSvc.downloadVehicleType();
    }
  }
}
