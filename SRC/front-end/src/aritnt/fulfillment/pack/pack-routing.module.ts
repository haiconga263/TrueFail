import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PackMasterComponent } from './pack-master/pack-master.component';
import { PackDetailComponent } from './pack-detail/pack-detail.component';

const routes: Routes = [{
  path: '',
  redirectTo: 'list',
  pathMatch: 'full',
},
{
  path: '',
  children: [
      { path: 'list', component: PackMasterComponent, data: { title: 'Đóng gói' } },
      { path: 'detail', component: PackDetailComponent, data: { title: 'Thực hiện đóng gói' } }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrderRoutingModule { }
