import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProcessDatagridRoutingModule } from './process-datagrid-routing.module';
import { ProcessDatagridComponent } from './process-datagrid.component';
import { DxButtonModule, DxSelectBoxModule, DxDataGridModule } from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    ProcessDatagridRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule
  ],
  declarations: [ProcessDatagridComponent]
})
export class ProcessDatagridModule { }
