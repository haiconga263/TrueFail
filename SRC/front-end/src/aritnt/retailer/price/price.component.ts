import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { PriceService, Product } from './price.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';
import { appUrl } from '../app-url';
import { UoMService, UoM } from '../common/services/uom.service';

@Component({
  selector: 'price',
  templateUrl: './price.component.html',
  styleUrls: ['./price.component.css']
})
export class PriceComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource: ArrayStore;
  selectedRows: number[];

  private uoms: UoM[] = [];

  constructor(
    injector: Injector,
    private priceSvc: PriceService,
    private uoMSvc: UoMService
  ) {
    super(injector);
    this.loadDatasource();
  }

  loadDatasource(callback: () => void = null) {
    this.uoMSvc.gets().subscribe(uomRs => {
      if(uomRs.result == ResultCode.Success)
      {
        this.uoms = uomRs.data;
      }
    });
    this.priceSvc.gets().subscribe((result) => {
      if (result.result == ResultCode.Success) {

        this.dataSource = new ArrayStore({
          key: ["id", "currentUoM"],
          data: result.data
        });

        if (FuncHelper.isFunction(callback))
          callback();
      }
    });
  }
}
