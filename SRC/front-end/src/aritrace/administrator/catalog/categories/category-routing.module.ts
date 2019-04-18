import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CategoryComponent } from './category.component';

const routes: Routes = [{
  path: '',
  component: CategoryComponent,
  children: [
    { path: '', redirectTo: 'datagrid', pathMatch: 'full' },
    { path: 'datagrid', loadChildren: 'src/aritrace/administrator/catalog/categories/datagrid/category-datagrid.module#CategoryDatagridModule', data: { title: 'Categories' }, },
    { path: 'detail', loadChildren: 'src/aritrace/administrator/catalog/categories/detail/category-detail.module#CategoryDetailModule', data: { title: 'Category Detail' }, },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategoryRoutingModule { }
