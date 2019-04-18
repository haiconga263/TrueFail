import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CategoryComponent } from './master/category.component';
import { CategoryDetailComponent } from './detail/category-detail.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full',
  },
  {
    path: '',
    children: [
      { path: 'list', component: CategoryComponent, data: { title: 'Category List' }, },
      { path: 'detail', component: CategoryDetailComponent, data: { title: 'Category Detail' }, }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategoryRoutingModule { }
