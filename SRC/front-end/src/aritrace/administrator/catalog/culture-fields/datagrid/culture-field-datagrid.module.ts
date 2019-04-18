import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DxDataGridModule, DxSelectBoxModule, DxButtonModule } from 'devextreme-angular';
import { CultureFieldDatagridComponent } from './culture-field-datagrid.component';
import { CultureFieldDatagridRoutingModule } from './culture-field-datagrid-routing.module';

@NgModule({
  imports: [
    CommonModule,
    CultureFieldDatagridRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule
  ],
  declarations: [CultureFieldDatagridComponent]
})
export class CultureFieldDatagridModule { }
