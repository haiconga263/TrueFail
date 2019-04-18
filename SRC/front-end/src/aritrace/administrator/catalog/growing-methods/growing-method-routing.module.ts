import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GrowingMethodComponent } from './growing-method.component';

const routes: Routes = [{
  path: '',
  component: GrowingMethodComponent,
  children: [
    { path: '', redirectTo: 'datagrid', pathMatch: 'full' },
    { path: 'datagrid', loadChildren: 'src/aritrace/administrator/catalog/growing-methods/datagrid/growing-method-datagrid.module#GrowingMethodDatagridModule', data: { title: 'Growing Methods' }, },
    { path: 'detail', loadChildren: 'src/aritrace/administrator/catalog/growing-methods/detail/growing-method-detail.module#GrowingMethodDetailModule', data: { title: 'Growing Method Detail' }, },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GrowingMethodRoutingModule { }
