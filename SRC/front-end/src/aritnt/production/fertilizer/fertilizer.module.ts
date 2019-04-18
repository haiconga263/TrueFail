import { NgModule } from '@angular/core';
import { CommonModule } from 'node_modules/@angular/common';

import { FertilizerRoutingModule } from './fertilizer-routing.module';
import { FertilizerComponent } from './master/fertilizer.component';
import { FertilizerDetailComponent } from './detail/fertilizer-detail.component';
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
import { FertilizerService } from '../../common/services/fertilizer.service';
import { FertilizerCategoryService } from '../../common/services/fertilizer-category.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    FertilizerRoutingModule,
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
    DxTextAreaModule
  ],
  declarations: [FertilizerComponent, FertilizerDetailComponent],
  providers: [FertilizerService, FertilizerCategoryService]
})
export class FertilizerModule { }
