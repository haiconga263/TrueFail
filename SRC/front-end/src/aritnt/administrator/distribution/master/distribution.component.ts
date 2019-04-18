import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { DistributionService } from '../distribution.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';
import { appUrl } from '../../app-url';

@Component({
  selector: 'distribution',
  templateUrl: './distribution.component.html',
  styleUrls: ['./distribution.component.css']
})
export class DistributionComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource: ArrayStore;
  selectedRows: number[];

  constructor(
    injector: Injector,
    private prodSvc: DistributionService
  ) {
    super(injector);
    this.loadDatasource();
  }

  loadDatasource(callback: () => void = null) {
    this.prodSvc.gets().subscribe((result) => {
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
      let rs = await this.prodSvc.delete(this.selectedRows[0]).toPromise();
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
        this.router.navigate([appUrl.distributionDetail],
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
      this.router.navigate([appUrl.distributionDetail],
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
