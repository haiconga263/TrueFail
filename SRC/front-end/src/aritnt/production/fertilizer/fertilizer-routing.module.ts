import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FertilizerComponent } from './master/fertilizer.component';
import { FertilizerDetailComponent } from './detail/fertilizer-detail.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full',
  },
  {
    path: '',
    children: [
      { path: 'list', component: FertilizerComponent, data: { title: 'Fertilizer List' }, },
      { path: 'detail', component: FertilizerDetailComponent, data: { title: 'Fertilizer Detail' }, }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FertilizerRoutingModule { }
