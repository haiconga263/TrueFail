import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductionDatagridComponent } from './production-datagrid.component';

const routes: Routes = [{
  path: '',
  component: ProductionDatagridComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductionDatagridRoutingModule { }
