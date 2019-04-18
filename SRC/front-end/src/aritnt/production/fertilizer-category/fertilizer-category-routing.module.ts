import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FertilizerCategoryComponent } from './master/fertilizer-category.component';
import { FertilizerCategoryDetailComponent } from './detail/fertilizer-category-detail.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full',
  },
  {
    path: '',
    children: [
      { path: 'list', component: FertilizerCategoryComponent, data: { title: 'Fertilizer Category List' }, },
      { path: 'detail', component: FertilizerCategoryDetailComponent, data: { title: 'Fertilizer Category Detail' }, }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FertilizerCategoryRoutingModule { }
