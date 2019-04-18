import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DistrictRoutingModule } from './district-routing.module';
import { DistrictComponent } from './district.component';
import { DxDataGridModule, DxSelectBoxModule, DxButtonModule, DxLookupModule } from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    DistrictRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxLookupModule,
  ],
  declarations: [DistrictComponent]
})
export class DistrictModule { }
