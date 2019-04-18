import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './master/user.component';
import { UserDetailComponent } from './detail/user-detail.component';

const routes: Routes = [{
  path: '',
  redirectTo: 'list',
  pathMatch: 'full',
},
{
  path: '',
  children: [
      { path: 'list', component: UserComponent },
      { path: 'detail', component: UserDetailComponent }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
