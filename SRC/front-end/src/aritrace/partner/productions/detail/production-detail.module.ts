import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductionDetailRoutingModule } from './production-detail-routing.module';
import { ProductionDetailComponent } from './production-detail.component';
import { DxNumberBoxModule, DxValidatorModule, DxValidationSummaryModule, DxSelectBoxModule, DxTextAreaModule, DxDateBoxModule, DxFormModule, DxRadioGroupModule, DxTemplateModule, DxButtonModule, DxButtonGroupModule, DxColorBoxModule, DxTextBoxModule, DxBoxModule, DxDropDownBoxModule, DxDataGridModule } from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    ProductionDetailRoutingModule,
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
    DxSelectBoxModule,
    DxDataGridModule
  ],
  declarations: [ProductionDetailComponent]
})
export class ProductionDetailModule { }
