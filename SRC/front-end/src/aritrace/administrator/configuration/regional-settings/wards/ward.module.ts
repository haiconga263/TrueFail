import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WardRoutingModule } from './ward-routing.module';
import { WardComponent } from './ward.component';
import { DxDataGridModule, DxSelectBoxModule, DxButtonModule, DxLookupModule } from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    WardRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxLookupModule,
  ],
  declarations: [WardComponent]
})
export class WardModule { }
