import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RegionRoutingModule } from './region-routing.module';
import { RegionComponent } from './region.component';
import { DxDataGridModule, DxSelectBoxModule, DxButtonModule, DxLookupModule } from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    RegionRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxLookupModule,
  ],
  declarations: [RegionComponent]
})
export class RegionModule { }
