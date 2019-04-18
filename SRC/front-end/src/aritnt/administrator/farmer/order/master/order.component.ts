import { Component, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { FarmerOrderService, FarmerOrder } from '../../farmer-order.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';
import { FarmerOrderStatus } from '../../farmer-order.service';
import { appUrl } from 'src/aritnt/administrator/app-url';

@Component({
  selector: 'order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent extends AppBaseComponent {
  private orders: FarmerOrder[] = [];
  selectedRows: number[];

  private from: Date = null;
  private to: Date = null;

  private statuses: FarmerOrderStatus[] = [];

  constructor(
    injector: Injector,
    private orderSvc: FarmerOrderService
  ) {
    super(injector);

    this.from = new Date(Date.now());
    this.to = new Date(Date.now());

    this.orderSvc.getStatuses().subscribe(rs => {
      if(rs.result == ResultCode.Success)
      {
        this.statuses = rs.data;
      }
    });

    this.loadDatasource();
  }

  loadDatasource(callback: () => void = null) {
    this.orderSvc.getsUncompleted().subscribe((result) => {
      if (result.result == ResultCode.Success) {

        this.orders = result.data;

        if (FuncHelper.isFunction(callback))
          callback();
      }
    });
  }

  private refresh()
  {
    console.log(this.from);
    console.log(this.to);

    this.loadDatasource();
  }

  private add() {
    this.router.navigate([appUrl.farmerOrderDetail],
      {
        queryParams: {
          type: 'add',
          returnUrl: appUrl.farmerOrder
        }
      });
  }

  private infor(orderId: number) {
    this.router.navigate([appUrl.farmerOrderDetail],
      {
        queryParams: {
          type: 'infor',
          id: this.selectedRows[0],
          returnUrl: appUrl.farmerOrder
        }
      });
  }

  private async update(orderId: number)
  {
    if(!this.checkCantUpdate(orderId)) {
      this.router.navigate([appUrl.farmerOrderDetail],
        {
          queryParams: {
            type: 'update',
            id: this.selectedRows[0],
            returnUrl: appUrl.farmerOrder
          }
        });
    }
  }

  private getOrder(orderId: number) {
    return this.orders.find(o => o.id == orderId);
  }

  private checkCantUpdate(orderId: number) {
    let order = this.orders.find(o => o.id == orderId);
    if(order != null && order.statusId != 1){ // Đã comfirmed
      return true;
    }
    return false;
  }

  private setStatus(orderId: number, statusId: number)
  {
    let order = this.orders.find(o => o.id == orderId);
    if(order != null){
      order.statusId = statusId;
    }
  }
}
