import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProcessDetailComponent } from './process-detail.component';

const routes: Routes = [{
  path: '',
  component: ProcessDetailComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProcessDetailRoutingModule { }
