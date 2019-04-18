import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { FaqService, Faq } from '../faq.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';
import { appUrl } from '../../app-url';
import { ExcelService } from 'src/core/services/excel.service';

@Component({
  selector: 'faq',
  templateUrl: './faq.component.html',
  styleUrls: ['./faq.component.css']
})
export class FaqComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource: ArrayStore;
  selectedRows: number[];

  

  constructor(
    injector: Injector,
    private faqSvc: FaqService,
    private excelSvc: ExcelService
  ) {
    super(injector);
    this.loadDatasource();
  }

  loadDatasource(callback: () => void = null) {
    this.faqSvc.getsOnly().subscribe((result) => {
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
      if(this.selectedRows.length == 1) {
        this.faqSvc.delete(this.selectedRows[0]).subscribe((result) => {
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
        this.router.navigate([appUrl.faqDetail],
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
      this.router.navigate([appUrl.faqDetail],
      {
        queryParams: {
          type: 'add'
        }
      });
    }
  };

  // itemClick(data: any) {
  //   console.log(data);
  //   if (data.itemData.action == 'faq') {
  //     // this.abivinSvc.getVehicleType().subscribe(rs => {
  //     //   if(rs.result == ResultCode.Success) {
  //     //     this.excelSvc.exportAsExcelFile(rs.data, "Faq");
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
