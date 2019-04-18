import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DxDataGridModule, DxSelectBoxModule, DxButtonModule } from 'devextreme-angular';
import { GrowingMethodDatagridComponent } from './growing-method-datagrid.component';
import { GrowingMethodDatagridRoutingModule } from './growing-method-datagrid-routing.module';

@NgModule({
  imports: [
    CommonModule,
    GrowingMethodDatagridRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule
  ],
  declarations: [GrowingMethodDatagridComponent]
})
export class GrowingMethodDatagridModule { }
