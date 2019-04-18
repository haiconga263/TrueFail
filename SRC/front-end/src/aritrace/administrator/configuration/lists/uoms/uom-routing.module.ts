import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UOMComponent } from './uom.component';

const routes: Routes = [{
  path: '',
  component: UOMComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UOMRoutingModule { }
