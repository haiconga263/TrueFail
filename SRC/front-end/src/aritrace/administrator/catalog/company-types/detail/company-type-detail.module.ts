import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CompanyTypeDetailRoutingModule } from './company-type-detail-routing.module';
import { CompanyTypeDetailComponent } from './company-type-detail.component';
import { DxSelectBoxModule, DxTextAreaModule, DxDateBoxModule, DxFormModule, DxRadioGroupModule, DxTemplateModule, DxButtonModule, DxButtonGroupModule, DxColorBoxModule, DxTabPanelModule, DxTextBoxModule } from 'devextreme-angular';
import { CaptionEmbedComponent } from 'src/aritrace/administrator/configuration/language-settings/captions/embed/caption-embed.component';
import { CaptionEmbedModule } from 'src/aritrace/administrator/configuration/language-settings/captions/embed/caption-embed.module';

@NgModule({
  imports: [
    CommonModule,
    CompanyTypeDetailRoutingModule,
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
  declarations: [CompanyTypeDetailComponent]
})
export class CompanyTypeDetailModule { }
