import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OrderTracingComponent } from './order-tracing.component';

const routes: Routes = [{
  path: '',
  component: OrderTracingComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrderTracingRoutingModule { }
