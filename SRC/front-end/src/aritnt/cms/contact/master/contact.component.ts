import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';
import { appUrl } from '../../app-url';
import { ExcelService } from 'src/core/services/excel.service';
import { ContactService } from '../contact.service';

@Component({
  selector: 'contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource:ArrayStore ;
  selectedRows: number[];

  

  constructor(
    injector: Injector,
    private contactSvc: ContactService,
    private excelSvc: ExcelService
  ) {
    super(injector);
    this.loadDatasource();
  }

  loadDatasource(callback: () => void = null) {
    this.contactSvc.getsOnly().subscribe((result) => {
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
        this.contactSvc.delete(this.selectedRows[0]).subscribe((result) => {
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
        this.router.navigate([appUrl.contactDetail],
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
      this.router.navigate([appUrl.contactDetail],
      {
        queryParams: {
          type: 'add'
        }
      });
    }
  };

  // itemClick(data: any) {
  //   console.log(data);
  //   if (data.itemData.action == 'contact') {
  //     // this.abivinSvc.getVehicleType().subscribe(rs => {
  //     //   if(rs.result == ResultCode.Success) {
  //     //     this.excelSvc.exportAsExcelFile(rs.data, "Contact");
  //     //   }
  //     //   else {
  //     //     this.showError(rs.errorContact);
  //     //   }
  //     // });
  //   }
  // }

  ngOnInit() {
  }
}
