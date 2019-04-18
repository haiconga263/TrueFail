import { Component, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { OrderService, Order, OrderStatus } from '../order.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';
import { appUrl } from '../../app-url';

@Component({
  selector: 'order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent extends AppBaseComponent {
  selectedRows: Order[] = [];

  private orders: Order[] = [];

  private statuses: OrderStatus[] = [];

  constructor(
    injector: Injector,
    private planSvc: OrderService
  ) {
    super(injector);

    this.planSvc.getStatuses().subscribe(statusRs => {
      if(statusRs.result == ResultCode.Success)
      {
        this.statuses = statusRs.data;
      }
    });

    this.loadDatasource();
  }

  loadDatasource(callback: () => void = null) {
    this.planSvc.getsUncompleted().subscribe((result) => {
      if (result.result == ResultCode.Success) {

        this.orders = result.data;

        if (FuncHelper.isFunction(callback))
          callback();
      }
    });
  }

  private create() {
    console.log('create');
    this.router.navigate([appUrl.orderDetail],
    {
      queryParams: {
        type: 'add'
      }
    });
  }

  private update() {
    console.log('update');
    if(this.selectedRows.length == 1)
    {
      this.router.navigate([appUrl.orderDetail],
      {
        queryParams: {
          type: 'update',
          id: this.selectedRows[0].id
        }
      });
    }
  }

  private delete() {
    console.log('delete');
    if(this.selectedRows.length == 1)
    {
      this.planSvc.delete(this.selectedRows[0].id).subscribe(rs => {
        if(rs.result == ResultCode.Success)
        {
          this.showSuccess(this.lang.instant("Common.DeleteSuccess"));
          this.orders = FuncHelper.removeItemInArray(this.orders, "id", this.selectedRows[0].id);
        }
        else
        {
          this.showError(rs.errorMessage);
        }
      });
    }
  }

  private isCantAction() {
    if(this.selectedRows.length == 1) {
      return this.selectedRows[0].statusId != 1;
    }
    else {
      return true;
    }
  }
}
