import { NgModule } from '@angular/core';
import { CommonModule } from 'node_modules/@angular/common';

import { PlotRoutingModule } from './plot-routing.module';
import { PlotComponent } from './master/plot.component';
import { PlotDetailComponent } from './detail/plot-detail.component';
import {
  DxDataGridModule,
  DxButtonModule,
  DxSelectBoxModule,
  DxTextBoxModule,
  DxValidatorModule,
  DxCheckBoxModule,
  DxNumberBoxModule,
  DxLookupModule,
  DxDropDownBoxModule,
  DxMenuModule,
  DxTreeViewModule,
  DxTreeListModule,
  DxTextAreaModule
} from 'devextreme-angular';
import { FormsModule } from '@angular/forms';
import { PlotService } from '../../common/services/plot.service';
import { FarmerService } from 'src/aritnt/administrator/farmer/farmer.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    PlotRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxTextBoxModule,
    DxValidatorModule,
    DxCheckBoxModule,
    DxNumberBoxModule,
    DxLookupModule,
    DxDropDownBoxModule,
    DxTreeViewModule,
    DxMenuModule,
    DxTreeViewModule,
    DxTreeListModule,
    DxTextAreaModule,
    DxNumberBoxModule
  ],
  declarations: [PlotComponent, PlotDetailComponent],
  providers: [PlotService, FarmerService]
})
export class PlotModule { }
