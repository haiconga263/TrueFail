import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReceivingComponent } from './receiving.component';

const routes: Routes = [{
  path: '',
  component: ReceivingComponent,
  data: { title: 'Lấy hàng' }
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SupervisionRoutingModule { }
