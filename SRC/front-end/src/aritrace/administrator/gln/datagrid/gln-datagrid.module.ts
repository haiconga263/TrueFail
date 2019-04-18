import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DxDataGridModule, DxSelectBoxModule, DxButtonModule } from 'devextreme-angular';
import { GLNDatagridComponent } from './gln-datagrid.component';
import { GLNDatagridRoutingModule } from './gln-datagrid-routing.module';

@NgModule({
  imports: [
    CommonModule,
    GLNDatagridRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule
  ],
  declarations: [GLNDatagridComponent]
})
export class GLNDatagridModule { }
