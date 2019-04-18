import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmployeeHistoryComponent } from './employee-history/employee-history.component';
import { OrderHistoryComponent } from './order-history/order-history.component';
import { InventoryHistoryComponent } from './inventory-history/inventory-history.component';
import { ShippingHistoryComponent } from './shipping-history/shipping-history.component';

const routes: Routes = [{
  path: '',
  redirectTo: 'order-history',
  pathMatch: 'full',
},
{
  path: '',
  children: [
      { path: 'employee-history', component: EmployeeHistoryComponent, data: { title: 'Lịch sử nhân viên nhận hàng' } },
      { path: 'order-history', component: OrderHistoryComponent, data: { title: 'Lịch sử nhận hàng' } },
      { path: 'inventory-history', component: InventoryHistoryComponent, data: { title: 'Lịch sử nhập xuất kho' } },
      { path: 'shipping-history', component: ShippingHistoryComponent, data: { title: 'Lịch sử chuyển hàng' } }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportRoutingModule { }
