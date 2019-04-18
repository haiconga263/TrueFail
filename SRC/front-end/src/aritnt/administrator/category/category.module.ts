import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoryRoutingModule } from './category-routing.module';
import { CategoryComponent } from './master/category.component';
import { CategoryDetailComponent } from './detail/category-detail.component';
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
  DxTreeViewModule,
  DxMenuModule,
  DxTreeListModule
} from 'devextreme-angular';
import { UoMService } from '../common/services/uom.service';
import { FormsModule } from '@angular/forms';
import { CategoryService } from '../category/category.service';
import { AbivinService } from '../common/services/abivin.service';
import { ExcelService } from '../../../core/services/excel.service'

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    CategoryRoutingModule,
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
    DxTreeListModule
  ],
  declarations: [CategoryComponent, CategoryDetailComponent],
  providers: [CategoryService, UoMService, CategoryService, AbivinService, ExcelService]
})
export class CategoryModule { }
