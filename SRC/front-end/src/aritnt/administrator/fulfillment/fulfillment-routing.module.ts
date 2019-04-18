import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FulfillmentComponent } from './master/fulfillment.component';
import { FulfillmentDetailComponent } from './detail/fulfillment-detail.component';

const routes: Routes = [{
  path: '',
  redirectTo: 'list',
  pathMatch: 'full',
},
{
  path: '',
  children: [
      { path: 'list', component: FulfillmentComponent },
      { path: 'detail', component: FulfillmentDetailComponent }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FulfillmentRoutingModule { }
