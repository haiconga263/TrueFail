import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { WardComponent } from './ward.component';

const routes: Routes = [{
  path: '',
  component: WardComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WardRoutingModule { }
