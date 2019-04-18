import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PesticideComponent } from './master/pesticide.component';
import { PesticideDetailComponent } from './detail/pesticide-detail.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full',
  },
  {
    path: '',
    children: [
      { path: 'list', component: PesticideComponent, data: { title: 'Pesticide List' }, },
      { path: 'detail', component: PesticideDetailComponent, data: { title: 'Pesticide Detail' }, }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PesticideRoutingModule { }
