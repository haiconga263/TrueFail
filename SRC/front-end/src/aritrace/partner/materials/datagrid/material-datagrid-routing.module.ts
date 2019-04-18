import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MaterialDatagridComponent } from './material-datagrid.component';

const routes: Routes = [{
  path: '',
  component: MaterialDatagridComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MaterialDatagridRoutingModule { }
