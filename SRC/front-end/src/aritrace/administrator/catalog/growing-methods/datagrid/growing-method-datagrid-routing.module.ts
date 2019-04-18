import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GrowingMethodDatagridComponent } from './growing-method-datagrid.component';

const routes: Routes = [{
  path: '',
  component: GrowingMethodDatagridComponent,
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GrowingMethodDatagridRoutingModule { }
