import { NgModule } from '@angular/core';
import { CommonModule } from 'node_modules/@angular/common';

import { PesticideRoutingModule } from './pesticide-routing.module';
import { PesticideComponent } from './master/pesticide.component';
import { PesticideDetailComponent } from './detail/pesticide-detail.component';
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
import { PesticideService } from '../../common/services/pesticide.service';
import { PesticideCategoryService } from '../../common/services/pesticide-category.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    PesticideRoutingModule,
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
  declarations: [PesticideComponent, PesticideDetailComponent],
  providers: [PesticideService, PesticideCategoryService]
})
export class PesticideModule { }
