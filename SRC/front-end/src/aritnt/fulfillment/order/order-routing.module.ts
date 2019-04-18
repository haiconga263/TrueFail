import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OrderMasterComponent } from './order-master/order-master.component';
import { OrderDetailComponent } from './order-detail/order-detail.component';

const routes: Routes = [{
  path: '',
  redirectTo: 'list',
  pathMatch: 'full',
},
{
  path: '',
  children: [
      { path: 'list', component: OrderMasterComponent, data: { title: 'Đóng gói' } },
      { path: 'detail', component: OrderDetailComponent, data: { title: 'Caption Detail' } }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrderRoutingModule { }
