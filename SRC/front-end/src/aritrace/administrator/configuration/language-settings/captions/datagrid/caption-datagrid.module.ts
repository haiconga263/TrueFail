import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CaptionDatagridRoutingModule } from './caption-datagrid-routing.module';
import { CaptionDatagridComponent } from './caption-datagrid.component';
import { DxDataGridModule, DxSelectBoxModule, DxButtonModule, DxLookupModule } from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    CaptionDatagridRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxLookupModule,
  ],
  declarations: [CaptionDatagridComponent]
})
export class CaptionDatagridModule { }
