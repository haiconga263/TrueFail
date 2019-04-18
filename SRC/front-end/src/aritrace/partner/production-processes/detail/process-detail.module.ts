import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProcessDetailRoutingModule } from './process-detail-routing.module';
import { ProcessDetailComponent } from './process-detail.component';
import {  DxNumberBoxModule, DxValidatorModule, DxValidationSummaryModule, DxSelectBoxModule, DxTextAreaModule, DxDateBoxModule, DxFormModule, DxRadioGroupModule, DxTemplateModule, DxButtonModule, DxButtonGroupModule, DxColorBoxModule, DxTextBoxModule, DxBoxModule, DxDataGridModule, DxDropDownBoxModule, DxListModule } from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    ProcessDetailRoutingModule,
    DxSelectBoxModule,
    DxTextAreaModule,
    DxDateBoxModule,
    DxFormModule,
    DxRadioGroupModule,
    DxTemplateModule,
    DxButtonModule,
    DxButtonGroupModule,
    DxTextBoxModule,
    DxBoxModule,
    DxValidatorModule,
    DxValidationSummaryModule,
    DxNumberBoxModule,
    DxDropDownBoxModule,
    DxDataGridModule,
    DxListModule
  ],
  declarations: [ProcessDetailComponent]
})
export class ProcessDetailModule { }
