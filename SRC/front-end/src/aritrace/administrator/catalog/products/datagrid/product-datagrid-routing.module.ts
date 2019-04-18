import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductDatagridComponent } from './product-datagrid.component';

const routes: Routes = [{
  path: '',
  component: ProductDatagridComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductDatagridRoutingModule { }
