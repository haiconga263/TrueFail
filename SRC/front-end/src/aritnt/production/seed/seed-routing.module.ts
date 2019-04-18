import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SeedComponent } from './master/seed.component';
import { SeedDetailComponent } from './detail/seed-detail.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full',
  },
  {
    path: '',
    children: [
      { path: 'list', component: SeedComponent, data: { title: 'Seed List' }, },
      { path: 'detail', component: SeedDetailComponent, data: { title: 'Seed Detail' }, }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SeedRoutingModule { }
