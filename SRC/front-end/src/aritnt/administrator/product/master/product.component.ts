import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { ProductService, Product } from '../product.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';
import { appUrl } from '../../app-url';
import { ExcelService } from 'src/core/services/excel.service';
import { AbivinService } from '../../common/services/abivin.service';

@Component({
  selector: 'product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource: ArrayStore;
  selectedRows: number[];

  menuItems = [
    {
      text: "Abivin",
      icon: "fa fa-file-excel-o",
      items: [
        { text: this.lang.instant("Admin.Product"), action: 'product' },
      ]
    }
  ];

  constructor(
    injector: Injector,
    private prodSvc: ProductService,
    private excelSvc: ExcelService,
    private abivinSvc: AbivinService
  ) {
    super(injector);
    this.loadDatasource();
  }

  loadDatasource(callback: () => void = null) {
    this.prodSvc.getsOnly().subscribe((result) => {
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
        this.router.navigate([appUrl.productDetail],
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
      this.router.navigate([appUrl.productDetail],
      {
        queryParams: {
          type: 'add'
        }
      });
    }
  };

  itemClick(data: any) {
    console.log(data);
    if (data.itemData.action == 'product') {
      this.abivinSvc.getVehicleType().subscribe(rs => {
        if(rs.result == ResultCode.Success) {
          this.excelSvc.exportAsExcelFile(rs.data, "Product");
        }
        else {
          this.showError(rs.errorMessage);
        }
      });
    }
  }

  ngOnInit() {
  }
}
