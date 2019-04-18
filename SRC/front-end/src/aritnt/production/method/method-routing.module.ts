import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MethodComponent } from './master/method.component';
import { MethodDetailComponent } from './detail/method-detail.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full',
  },
  {
    path: '',
    children: [
      { path: 'list', component: MethodComponent, data: { title: 'Method List' }, },
      { path: 'detail', component: MethodDetailComponent, data: { title: 'Method Detail' }, }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MethodRoutingModule { }
