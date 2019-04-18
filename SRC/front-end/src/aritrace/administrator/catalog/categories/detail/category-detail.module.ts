import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoryDetailRoutingModule } from './category-detail-routing.module';
import { CategoryDetailComponent } from './category-detail.component';
import { DxSelectBoxModule, DxTextAreaModule, DxDateBoxModule, DxFormModule, DxRadioGroupModule, DxTemplateModule, DxButtonModule, DxButtonGroupModule, DxColorBoxModule, DxTabPanelModule, DxTextBoxModule } from 'devextreme-angular';
import { CaptionEmbedComponent } from 'src/aritrace/administrator/configuration/language-settings/captions/embed/caption-embed.component';
import { CaptionEmbedModule } from 'src/aritrace/administrator/configuration/language-settings/captions/embed/caption-embed.module';

@NgModule({
  imports: [
    CommonModule,
    CategoryDetailRoutingModule,
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
  declarations: [CategoryDetailComponent]
})
export class CategoryDetailModule { }
