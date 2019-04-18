import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MaterialDetailRoutingModule } from './material-detail-routing.module';
import { MaterialDetailComponent } from './material-detail.component';
import { DxSelectBoxModule, DxTextAreaModule, DxDateBoxModule, DxFormModule, DxRadioGroupModule, DxTemplateModule, DxButtonModule, DxButtonGroupModule, DxColorBoxModule, DxTabPanelModule, DxTextBoxModule } from 'devextreme-angular';
import { BoxModule } from 'angular-admin-lte';
import { MaterialHistoryModule } from '../histories/material-history.module';

@NgModule({
  imports: [
    CommonModule,
    MaterialDetailRoutingModule,
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
    BoxModule,
    MaterialHistoryModule
  ],
  declarations: [MaterialDetailComponent]
})
export class MaterialDetailModule { }
