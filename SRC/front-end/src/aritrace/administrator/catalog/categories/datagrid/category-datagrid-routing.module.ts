import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CategoryDatagridComponent } from './category-datagrid.component';

const routes: Routes = [{
  path: '',
  component: CategoryDatagridComponent,
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategoryDatagridRoutingModule { }
