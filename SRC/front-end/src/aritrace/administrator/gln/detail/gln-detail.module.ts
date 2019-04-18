import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GLNDetailRoutingModule } from './gln-detail-routing.module';
import { GLNDetailComponent } from './gln-detail.component';
import { DxSelectBoxModule, DxTextAreaModule, DxDateBoxModule, DxFormModule, DxRadioGroupModule, DxTemplateModule, DxButtonModule, DxButtonGroupModule, DxColorBoxModule, DxTabPanelModule, DxTextBoxModule } from 'devextreme-angular';
import { CaptionEmbedComponent } from 'src/aritrace/administrator/configuration/language-settings/captions/embed/caption-embed.component';

@NgModule({
  imports: [
    CommonModule,
    GLNDetailRoutingModule,
    DxSelectBoxModule,
    DxTextAreaModule,
    DxDateBoxModule,
    DxFormModule,
    DxRadioGroupModule,
    DxTemplateModule,
    DxButtonModule,
    DxButtonGroupModule,
    DxTabPanelModule,
    DxTextBoxModule
  ],
  declarations: [GLNDetailComponent, CaptionEmbedComponent]
})
export class GLNDetailModule { }
