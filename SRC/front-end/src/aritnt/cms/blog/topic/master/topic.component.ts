import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { TopicService, Topic } from '../topic.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';

import { ExcelService } from 'src/core/services/excel.service';
import { appUrl } from 'src/aritnt/cms/app-url';

@Component({
  selector: 'topic',
  templateUrl: './topic.component.html',
  styleUrls: ['./topic.component.css']
})
export class TopicComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource: ArrayStore;
  selectedRows: number[];

  

  constructor(
    injector: Injector,
    private topicSvc: TopicService
  ) {
    super(injector);
    this.loadDatasource();
  }

  loadDatasource(callback: () => void = null) {
    this.topicSvc.getsOnly().subscribe((result) => {
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
      // let rs = await this.topicSvc.delete(this.selectedRows[0]).toPromise();
      // if(rs.result == ResultCode.Success)
      // {
      //   this.dataGrid.instance.removeRow(this.dataGrid.instance.getRowIndexByKey(this.selectedRows[0]));
      // }
      // else
      // {
      //   this.showError(this.lang.instant("Common.DeleteFail"));
      //   //alert
      // }
      if(this.selectedRows.length == 1) {
        this.topicSvc.delete(this.selectedRows[0]).subscribe((result) => {
          if(result.result == ResultCode.Success)
          {
            //alert
            this.showSuccess(this.lang.instant('Common.DeleteSuccess'));
            this.grid.refresh();
          }
          else
          {
            //alert
            this.showError(this.lang.instant(result.errorMessage));
          }
        });
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
        this.router.navigate([appUrl.topicDetail],
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
      this.router.navigate([appUrl.topicDetail],
      {
        queryParams: {
          type: 'add'
        }
      });
    }
  };

  // itemClick(data: any) {
  //   console.log(data);
  //   if (data.itemData.action == 'topic') {
  //     // this.abivinSvc.getVehicleType().subscribe(rs => {
  //     //   if(rs.result == ResultCode.Success) {
  //     //     this.excelSvc.exportAsExcelFile(rs.data, "Topic");
  //     //   }
  //     //   else {
  //     //     this.showError(rs.errorMessage);
  //     //   }
  //     // });
  //   }
  // }

  ngOnInit() {
  }
}
