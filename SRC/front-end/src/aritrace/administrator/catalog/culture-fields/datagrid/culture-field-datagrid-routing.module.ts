import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CultureFieldDatagridComponent } from './culture-field-datagrid.component';

const routes: Routes = [{
  path: '',
  component: CultureFieldDatagridComponent,
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CultureFieldDatagridRoutingModule { }
