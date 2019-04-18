import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PlotComponent } from './master/plot.component';
import { PlotDetailComponent } from './detail/plot-detail.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full',
  },
  {
    path: '',
    children: [
      { path: 'list', component: PlotComponent, data: { title: 'Plot List' }, },
      { path: 'detail', component: PlotDetailComponent, data: { title: 'Plot Detail' }, }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PlotRoutingModule { }
