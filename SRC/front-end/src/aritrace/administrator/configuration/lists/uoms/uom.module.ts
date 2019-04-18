import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UOMRoutingModule } from './uom-routing.module';
import { UOMComponent } from './uom.component';
import { DxDataGridModule, DxSelectBoxModule, DxButtonModule } from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    UOMRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule
  ],
  declarations: [UOMComponent]
})
export class UOMModule { }
