import { NgModule } from '@angular/core';
import { CommonModule } from 'node_modules/@angular/common';

import { MethodRoutingModule } from './method-routing.module';
import { MethodComponent } from './master/method.component';
import { MethodDetailComponent } from './detail/method-detail.component';
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
import { MethodService } from '../../common/services/method.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MethodRoutingModule,
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
  declarations: [MethodComponent, MethodDetailComponent],
  providers: [MethodService]
})
export class MethodModule { }
