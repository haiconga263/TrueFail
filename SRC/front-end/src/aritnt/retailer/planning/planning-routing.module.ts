import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PlanningComponent } from './master/planning.component';
import { PlanningDetailComponent } from './detail/planning-detail.component';

const routes: Routes = [{
  path: '',
  redirectTo: 'list',
  pathMatch: 'full',
},
{
  path: '',
  children: [
      { path: 'list', component: PlanningComponent, data: { title: 'Kế hoạch đặt hàng' } },
      { path: 'detail', component: PlanningDetailComponent, data: { title: 'Kế hoạch đặt hàng' } },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PlanningRoutingModule { }
