import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GLNDatagridComponent } from './gln-datagrid.component';

const routes: Routes = [{
  path: '',
  component: GLNDatagridComponent,
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GLNDatagridRoutingModule { }
