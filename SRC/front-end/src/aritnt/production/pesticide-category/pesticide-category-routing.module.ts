import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PesticideCategoryComponent } from './master/pesticide-category.component';
import { PesticideCategoryDetailComponent } from './detail/pesticide-category-detail.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full',
  },
  {
    path: '',
    children: [
      { path: 'list', component: PesticideCategoryComponent, data: { title: 'Pesticide Category List' }, },
      { path: 'detail', component: PesticideCategoryDetailComponent, data: { title: 'Pesticide Category Detail' }, }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PesticideCategoryRoutingModule { }
