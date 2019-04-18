import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FarmerComponent } from './farmer/master/farmer.component';
import { FarmerDetailComponent } from './farmer/detail/farmer-detail.component';
import { PlanningComponent } from './planning/master/planning.component';
import { PlanningHistoryComponent } from './planning/master-history/planning-history.component';
import { OrderComponent } from './order/master/order.component';
import { OrderHistoryComponent } from './order/master-history/order-history.component';
import { OrderDetailComponent } from './order/detail/order-detail.component';

const routes: Routes = [{
  path: '',
  redirectTo: 'list',
  pathMatch: 'full',
},
{
  path: '',
  children: [
      { path: 'list', component: FarmerComponent , data: { title: 'Farmer' }},
      { path: 'detail', component: FarmerDetailComponent , data: { title: 'Farmer detail' }},
      { path: 'planning', component: PlanningComponent, data: { title: 'Farmer planning' } },
      { path: 'planning-history', component: PlanningHistoryComponent, data: { title: 'Farmer planning history' } },
      { path: 'order', component: OrderComponent, data: { title: 'Farmer order' } },
      { path: 'order-history', component: OrderHistoryComponent, data: { title: 'Farmer order history' } },
      { path: 'order/detail', component: OrderDetailComponent, data: { title: 'Farmer order Detail' } },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FarmerRoutingModule { }
