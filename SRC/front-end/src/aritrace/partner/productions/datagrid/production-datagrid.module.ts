import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductionDatagridRoutingModule } from './production-datagrid-routing.module';
import { ProductionDatagridComponent } from './production-datagrid.component';
import { DxButtonModule, DxSelectBoxModule, DxDataGridModule } from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    ProductionDatagridRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule
  ],
  declarations: [ProductionDatagridComponent]
})
export class ProductionDatagridModule { }
