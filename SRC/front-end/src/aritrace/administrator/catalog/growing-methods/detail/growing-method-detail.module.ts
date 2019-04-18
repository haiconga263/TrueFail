import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GrowingMethodDetailRoutingModule } from './growing-method-detail-routing.module';
import { GrowingMethodDetailComponent } from './growing-method-detail.component';
import { DxSelectBoxModule, DxTextAreaModule, DxDateBoxModule, DxFormModule, DxRadioGroupModule, DxTemplateModule, DxButtonModule, DxButtonGroupModule, DxColorBoxModule, DxTabPanelModule, DxTextBoxModule } from 'devextreme-angular';
import { CaptionEmbedComponent } from 'src/aritrace/administrator/configuration/language-settings/captions/embed/caption-embed.component';
import { CaptionEmbedModule } from 'src/aritrace/administrator/configuration/language-settings/captions/embed/caption-embed.module';

@NgModule({
  imports: [
    CommonModule,
    GrowingMethodDetailRoutingModule,
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
    CaptionEmbedModule
  ],
  declarations: [GrowingMethodDetailComponent]
})
export class GrowingMethodDetailModule { }
