import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductDatagridRoutingModule } from './product-datagrid-routing.module';
import { ProductDatagridComponent } from './product-datagrid.component';
import { DxButtonModule, DxSelectBoxModule, DxDataGridModule } from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    ProductDatagridRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule
  ],
  declarations: [ProductDatagridComponent]
})
export class ProductDatagridModule { }
