import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TripHistoryComponent } from './trip-history/trip-history.component';
import { OrderHistoryComponent } from './order-history/order-history.component';

const routes: Routes = [{
  path: '',
  redirectTo: 'trip-history',
  pathMatch: 'full',
},
{
  path: '',
  children: [
      { path: 'trip-history', component: TripHistoryComponent, data: { title: 'Trip History' } },
      { path: 'order-history', component: OrderHistoryComponent, data: { title: 'Order History' } }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportRoutingModule { }
