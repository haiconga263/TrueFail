import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CompanyDetailRoutingModule } from './company-detail-routing.module';
import { CompanyDetailComponent } from './company-detail.component';
import { DxSelectBoxModule, DxTextAreaModule, DxDateBoxModule, DxFormModule, DxRadioGroupModule, DxTemplateModule, DxButtonModule, DxButtonGroupModule, DxColorBoxModule } from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    CompanyDetailRoutingModule,
    DxSelectBoxModule,
    DxTextAreaModule,
    DxDateBoxModule,
    DxFormModule,
    DxRadioGroupModule,
    DxTemplateModule,
    DxButtonModule,
    DxButtonGroupModule
  ],
  declarations: [CompanyDetailComponent]
})
export class CompanyDetailModule { }
