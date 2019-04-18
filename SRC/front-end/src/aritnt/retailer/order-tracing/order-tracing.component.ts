import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';
import { OrderService, Order, OrderStatus, OrderAudit } from '../order/order.service';

@Component({
  selector: 'order-tracing',
  templateUrl: './order-tracing.component.html',
  styleUrls: ['./order-tracing.component.css']
})
export class OrderTracingComponent extends AppBaseComponent {

  private orders: Order[] = [];
  private order: Order = new Order();

  private statuses: OrderStatus[] = [];

  constructor(
    injector: Injector,
    private orderSvc: OrderService
  ) {
    super(injector);
    this.order.buyingDate = null;
    this.loadDatasource();
  }

  private loadDatasource() {
    this.orderSvc.getStatuses().subscribe(result => {
      if(result.result == ResultCode.Success)
      {
        this.statuses = [];
        result.data.forEach(status => {
          if(status.id != -1)
          {
            this.statuses.push(status);
          }
        });
        
      }
    });

    this.orderSvc.getsUncompleted().subscribe((result) => {
      if (result.result == ResultCode.Success) {
        this.orders = result.data;
      }
    });
  }

  private orderSelectionChange(event: any) {
    if(this.order.audits == null)
    {
      let orderId = this.order.id;
      this.orderSvc.getAudit(orderId).subscribe(rs => {
        if(rs.result == ResultCode.Success)
        {
          let order = this.orders.find(o => o.id == orderId);
          order.audits = rs.data;
        }
      });
    }
  }

  private getAuditOrder(statusId: number) {
    if(this.order.audits == null)
      return new OrderAudit();
    let audit = this.order.audits.find(a => a.statusId == statusId && a.retailerOrderItemId == null);
    if(audit == null)
      return new OrderAudit();
    return audit;
  }

  private getAuditOrderWithOrderItem(statusId: number, orderItemId: number) {
    if(this.order.audits == null)
      return new OrderAudit();
    let audit = this.order.audits.find(a => a.statusId == statusId && a.retailerOrderItemId == orderItemId);
    if(audit == null)
      return new OrderAudit();
    return audit;
  }

  private getStatusName(statusId: number){
    let status = this.statuses.find(s => s.id == statusId);
    if(status == null)
      return '';
    return status.name;
  }

  public getCurrency(num: any) {
    if (num == null) {
      return 0;
    }
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
      num = "0";
    let sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    let cents: any = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
      cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
      num = num.substring(0, num.length - (4 * i + 3)) + '.' +
        num.substring(num.length - (4 * i + 3));
    return (((sign) ? '' : '-') + num);
  }
}
