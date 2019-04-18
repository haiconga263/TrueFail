import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GLNComponent } from './gln.component';

const routes: Routes = [{
  path: '',
  component: GLNComponent,
  children: [
    { path: '', redirectTo: 'datagrid', pathMatch: 'full' },
    { path: 'datagrid', loadChildren: 'src/aritrace/administrator/gln/datagrid/gln-datagrid.module#GLNDatagridModule', data: { title: 'Data Global Location Number' }, },
    { path: 'detail', loadChildren: 'src/aritrace/administrator/gln/detail/gln-detail.module#GLNDetailModule', data: { title: 'Global Location Number Detail' }, },

  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GLNRoutingModule { }
