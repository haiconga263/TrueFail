import { NgModule } from '@angular/core';
import { CommonModule } from 'node_modules/@angular/common';

import { FertilizerCategoryRoutingModule } from './fertilizer-category-routing.module';
import { FertilizerCategoryComponent } from './master/fertilizer-category.component';
import { FertilizerCategoryDetailComponent } from './detail/fertilizer-category-detail.component';
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
  DxTreeListModule
} from 'devextreme-angular';
import { FormsModule } from '@angular/forms';
import { FertilizerCategoryService } from '../../common/services/fertilizer-category.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    FertilizerCategoryRoutingModule,
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
  ],
  declarations: [FertilizerCategoryComponent, FertilizerCategoryDetailComponent],
  providers: [FertilizerCategoryService]
})
export class FertilizerCategoryModule { }
