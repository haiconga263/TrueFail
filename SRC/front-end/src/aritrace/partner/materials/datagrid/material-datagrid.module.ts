import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MaterialDatagridRoutingModule } from './material-datagrid-routing.module';
import { MaterialDatagridComponent } from './material-datagrid.component';
import { DxButtonModule, DxSelectBoxModule, DxDataGridModule } from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    MaterialDatagridRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule
  ],
  declarations: [MaterialDatagridComponent]
})
export class MaterialDatagridModule { }
