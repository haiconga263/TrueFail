import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MaterialComponent } from './material.component';

const routes: Routes = [{
  path: '',
  component: MaterialComponent,
  children: [
    { path: '', redirectTo: 'datagrid', pathMatch: 'full' },
    { path: 'datagrid', loadChildren: 'src/aritrace/partner/materials/datagrid/material-datagrid.module#MaterialDatagridModule', data: { title: 'Material', breadcrumbs: 'Datagrid' }, },
    { path: 'detail', loadChildren: 'src/aritrace/partner/materials/detail/material-detail.module#MaterialDetailModule', data: { title: 'Material Detail', breadcrumbs: 'Detail' }, },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MaterialRoutingModule { }
