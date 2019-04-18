import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DistributionComponent } from './master/distribution.component';
import { DistributionDetailComponent } from './detail/distribution-detail.component';

const routes: Routes = [{
  path: '',
  redirectTo: 'list',
  pathMatch: 'full',
},
{
  path: '',
  children: [
      { path: 'list', component: DistributionComponent },
      { path: 'detail', component: DistributionDetailComponent }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DistributionRoutingModule { }
