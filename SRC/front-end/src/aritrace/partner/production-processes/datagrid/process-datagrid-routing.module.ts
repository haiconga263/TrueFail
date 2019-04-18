import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProcessDatagridComponent } from './process-datagrid.component';

const routes: Routes = [{
  path: '',
  component: ProcessDatagridComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProcessDatagridRoutingModule { }
