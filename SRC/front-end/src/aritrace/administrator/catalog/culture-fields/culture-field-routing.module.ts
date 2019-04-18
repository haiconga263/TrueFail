import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CultureFieldComponent } from './culture-field.component';

const routes: Routes = [{
  path: '',
  component: CultureFieldComponent,
  children: [
    { path: '', redirectTo: 'datagrid', pathMatch: 'full' },
    { path: 'datagrid', loadChildren: 'src/aritrace/administrator/catalog/culture-fields/datagrid/culture-field-datagrid.module#CultureFieldDatagridModule', data: { title: 'Culture Fields' }, },
    { path: 'detail', loadChildren: 'src/aritrace/administrator/catalog/culture-fields/detail/culture-field-detail.module#CultureFieldDetailModule', data: { title: 'Culture Field Detail' }, },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CultureFieldRoutingModule { }
