import { Component, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { RetailerService, Retailer } from '../../retailer.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';
import { appUrl } from '../../../app-url';

@Component({
  selector: 'retailer',
  templateUrl: './retailer.component.html',
  styleUrls: ['./retailer.component.css']
})
export class RetailerComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource: ArrayStore;
  selectedRows: number[];

  constructor(
    injector: Injector,
    private retSvc: RetailerService
  ) {
    super(injector);
    this.loadDatasource();
  }

  loadDatasource(callback: () => void = null) {
    this.retSvc.gets().subscribe((result) => {
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
      let rs = await this.retSvc.delete(this.selectedRows[0]).toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.dataGrid.instance.removeRow(this.dataGrid.instance.getRowIndexByKey(this.selectedRows[0]));
      }
      else
      {
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
      if(this.selectedRows.length == 1)
      {
        this.router.navigate([appUrl.retailerDetail],
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
      this.router.navigate([appUrl.retailerDetail],
      {
        queryParams: {
          type: 'add'
        }
      });
    }
  };



  ngOnInit() {
  }
}
