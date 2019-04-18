import { NgModule } from '@angular/core';
import { CommonModule } from 'node_modules/@angular/common';

import { PesticideCategoryRoutingModule } from './pesticide-category-routing.module';
import { PesticideCategoryComponent } from './master/pesticide-category.component';
import { PesticideCategoryDetailComponent } from './detail/pesticide-category-detail.component';
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
import { PesticideCategoryService } from '../../common/services/pesticide-category.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    PesticideCategoryRoutingModule,
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
  declarations: [PesticideCategoryComponent, PesticideCategoryDetailComponent],
  providers: [PesticideCategoryService]
})
export class PesticideCategoryModule { }
