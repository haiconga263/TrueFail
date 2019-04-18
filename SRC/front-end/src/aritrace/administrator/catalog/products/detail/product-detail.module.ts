import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductDetailRoutingModule } from './product-detail-routing.module';
import { ProductDetailComponent } from './product-detail.component';
import { DxSelectBoxModule, DxTextAreaModule, DxDateBoxModule, DxFormModule, DxRadioGroupModule, DxTemplateModule, DxButtonModule, DxButtonGroupModule, DxColorBoxModule, DxTabPanelModule, DxTextBoxModule } from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    ProductDetailRoutingModule,
    DxSelectBoxModule,
    DxTextAreaModule,
    DxDateBoxModule,
    DxFormModule,
    DxRadioGroupModule,
    DxTemplateModule,
    DxButtonModule,
    DxButtonGroupModule,
    DxTabPanelModule,
    DxTextBoxModule,
    DxTextAreaModule,
  ],
  declarations: [ProductDetailComponent]
})
export class ProductDetailModule { }
