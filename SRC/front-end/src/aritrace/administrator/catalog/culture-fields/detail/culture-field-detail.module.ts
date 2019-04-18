import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CultureFieldDetailRoutingModule } from './culture-field-detail-routing.module';
import { CultureFieldDetailComponent } from './culture-field-detail.component';
import { DxSelectBoxModule, DxTextAreaModule, DxDateBoxModule, DxFormModule, DxRadioGroupModule, DxTemplateModule, DxButtonModule, DxButtonGroupModule, DxColorBoxModule, DxTabPanelModule, DxTextBoxModule } from 'devextreme-angular';
import { CaptionEmbedComponent } from 'src/aritrace/administrator/configuration/language-settings/captions/embed/caption-embed.component';
import { CaptionEmbedModule } from 'src/aritrace/administrator/configuration/language-settings/captions/embed/caption-embed.module';
import { BoxModule } from 'angular-admin-lte';

@NgModule({
  imports: [
    CommonModule,
    CultureFieldDetailRoutingModule,
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
    CaptionEmbedModule,
    BoxModule
  ],
  declarations: [CultureFieldDetailComponent]
})
export class CultureFieldDetailModule { }
