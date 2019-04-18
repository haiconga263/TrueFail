import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RetailerComponent } from './retailer/master/retailer.component';
import { RetailerDetailComponent } from './retailer/detail/retailer-detail.component';
import { LocationDetailComponent } from './retailer/location-detail/location-detail.component';
import { PlanningComponent } from './planning/master/planning.component';
import { PlanningHistoryComponent } from './planning/master-history/planning-history.component';
import { OrderComponent } from './order/master/order.component';
import { OrderDetailComponent } from './order/detail/order-detail.component';
import { OrderHistoryComponent } from './order/master-history/order-history.component';
import { OrderTempComponent } from './order-homepage/datagrid/order-temp-datagrid.component';


const routes: Routes = [{
  path: '',
  redirectTo: 'list',
  pathMatch: 'full',
},
{
  path: '',
  children: [
      { path: 'list', component: RetailerComponent, data: { title: 'Retailer' } },
      { path: 'detail', component: RetailerDetailComponent, data: { title: 'Retailer detail' } },
      { path: 'location-detail', component: LocationDetailComponent, data: { title: 'Retailer location' } },
      { path: 'planning', component: PlanningComponent, data: { title: 'Retailer planning' } },
      { path: 'planning-history', component: PlanningHistoryComponent, data: { title: 'Retailer planning history' } },
      { path: 'order', component: OrderComponent, data: { title: 'Retailer order' } },
      { path: 'order-detail', component: OrderDetailComponent, data: { title: 'Retailer order detail' } },
      { path: 'order-history', component: OrderHistoryComponent, data: { title: 'Retailer order history' } },
      { path: 'order-temp', component: OrderTempComponent, data: { title: 'Retailer order history' } },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RetailerRoutingModule { }
