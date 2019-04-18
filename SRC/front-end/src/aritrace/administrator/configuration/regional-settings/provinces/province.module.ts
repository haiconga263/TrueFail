import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProvinceRoutingModule } from './province-routing.module';
import { ProvinceComponent } from './province.component';
import { DxDataGridModule, DxSelectBoxModule, DxButtonModule, DxLookupModule } from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    ProvinceRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxLookupModule,
  ],
  declarations: [ProvinceComponent]
})
export class ProvinceModule { }
