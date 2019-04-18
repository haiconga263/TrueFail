import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OrderComponent } from './master/order.component';
import { OrderDetailComponent } from './detail/order-detail.component';

const routes: Routes = [{
  path: '',
  redirectTo: 'list',
  pathMatch: 'full',
},
{
  path: '',
  children: [
      { path: 'list', component: OrderComponent, data: { title: 'Đơn mua hàng' } },
      { path: 'detail', component: OrderDetailComponent, data: { title: 'Đơn mua hàng' } },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrderRoutingModule { }
