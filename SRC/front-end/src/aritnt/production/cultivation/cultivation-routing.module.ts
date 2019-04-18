import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CultivationComponent } from './master/cultivation.component';
import { CultivationDetailComponent } from './detail/cultivation-detail.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full',
  },
  {
    path: '',
    children: [
      { path: 'list', component: CultivationComponent, data: { title: 'Cultivation List' }, },
      { path: 'detail', component: CultivationDetailComponent, data: { title: 'Cultivation Detail' }, }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CultivationRoutingModule { }
