import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductComponent } from './product.component';

const routes: Routes = [{
  path: '',
  component: ProductComponent,
  children: [
    { path: '', redirectTo: 'datagrid', pathMatch: 'full' },
    { path: 'datagrid', loadChildren: 'src/aritrace/administrator/catalog/products/datagrid/product-datagrid.module#ProductDatagridModule', data: { title: 'Product' }, },
    { path: 'detail', loadChildren: 'src/aritrace/administrator/catalog/products/detail/product-detail.module#ProductDetailModule', data: { title: 'Product Detail' }, },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductRoutingModule { }
