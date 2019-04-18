import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OrderHistoryComponent } from './master/order-history.component';
import { OrderHistoryDetailComponent } from './detail/order-history-detail.component';

const routes: Routes = [{
  path: '',
  redirectTo: 'list',
  pathMatch: 'full',
},
{
  path: '',
  children: [
      { path: 'list', component: OrderHistoryComponent, data: { title: 'Lịch sử mua hàng' } },
      { path: 'detail', component: OrderHistoryDetailComponent, data: { title: 'Lịch sử mua hàng' } },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrderHistoryRoutingModule { }
