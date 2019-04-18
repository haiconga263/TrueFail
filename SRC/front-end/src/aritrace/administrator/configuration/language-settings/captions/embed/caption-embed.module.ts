import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CaptionEmbedRoutingModule } from './caption-embed-routing.module';
import { CaptionEmbedComponent } from './caption-embed.component';
import { DxTextBoxModule, DxTabPanelModule, DxTabsModule, DxSelectBoxModule, DxTextAreaModule, DxDateBoxModule, DxFormModule, DxRadioGroupModule, DxTemplateModule, DxButtonModule, DxButtonGroupModule, DxColorBoxModule } from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    CaptionEmbedRoutingModule,
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
  exports: [CaptionEmbedComponent],
  declarations: [CaptionEmbedComponent]
})
export class CaptionEmbedModule { }
