import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CaptionDetailRoutingModule } from './caption-detail-routing.module';
import { CaptionDetailComponent } from './caption-detail.component';
import { DxTextBoxModule, DxTabPanelModule, DxTabsModule, DxSelectBoxModule, DxTextAreaModule, DxDateBoxModule, DxFormModule, DxRadioGroupModule, DxTemplateModule, DxButtonModule, DxButtonGroupModule, DxColorBoxModule } from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    CaptionDetailRoutingModule,
    DxSelectBoxModule,
    DxTextAreaModule,
    DxDateBoxModule,
    DxFormModule,
    DxRadioGroupModule,
    DxTemplateModule,
    DxButtonModule,
    DxButtonGroupModule,
    DxTabsModule,
    DxTabPanelModule,
    DxTextBoxModule
  ],
  declarations: [CaptionDetailComponent]
})
export class CaptionDetailModule { }
