import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AccountComponent } from './master/account.component';
import { InforUpdateComponent } from './infor-update/infor-update.component';

const routes: Routes = [{
  path: '',
  redirectTo: 'list',
  pathMatch: 'full',
},
{
  path: '',
  children: [
      { path: 'list', component: AccountComponent, data: { title: 'Quản lý tài khoản' } },
      { path: 'infor/update', component: InforUpdateComponent, data: { title: 'Chỉnh sửa thông tin' } }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
