import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GrowingMethodDetailComponent } from './growing-method-detail.component';

const routes: Routes = [{
  path: '',
  component: GrowingMethodDetailComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GrowingMethodDetailRoutingModule { }
