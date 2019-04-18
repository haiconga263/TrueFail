import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FCMasterComponent } from './fc-master/fc-master.component';
import { FCDetailComponent } from './fc-detail/fc-detail.component';



const routes: Routes = [{
  path: '',
  redirectTo: 'list',
  pathMatch: 'full',
},
{
  path: '',
  children: [
      { path: 'list', component: FCMasterComponent, data: { title: 'Nhận hàng - Collection' } },
      { path: 'detail', component: FCDetailComponent, data: { title: 'Hàng trong kho' } }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrderRoutingModule { }
