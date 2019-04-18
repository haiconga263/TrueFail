import { NgModule } from '@angular/core';
import { CommonModule } from 'node_modules/@angular/common';

import { CultivationRoutingModule } from './cultivation-routing.module';
import { CultivationComponent } from './master/cultivation.component';
import { CultivationDetailComponent } from './detail/cultivation-detail.component';
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
  DxTextAreaModule,
  DxDateBoxModule
} from 'devextreme-angular';
import { FormsModule } from '@angular/forms';
import { CultivationService } from 'src/aritnt/common/services/cultivation.service';
import { ProductService } from 'src/aritnt/common/services/product.service';
import { PlotService } from 'src/aritnt/common/services/plot.service';
import { SeedService } from 'src/aritnt/common/services/seed.service';
import { FarmerService } from 'src/aritnt/common/services/farmer.service';
import { MethodService } from 'src/aritnt/common/services/method.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    CultivationRoutingModule,
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
    DxDateBoxModule
  ],
  declarations: [CultivationComponent, CultivationDetailComponent],
  providers: [CultivationService, SeedService, PlotService, FarmerService, ProductService, MethodService]
})
export class CultivationModule { }
