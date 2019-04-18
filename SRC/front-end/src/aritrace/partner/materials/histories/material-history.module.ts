import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { MaterialHistoryRoutingModule } from './material-history-routing.module';
import { MaterialHistoryComponent } from './material-history.component';
import { DxCheckBoxModule, DxNumberBoxModule, DxValidatorModule, DxPopupModule, DxListModule, DxSelectBoxModule, DxTextAreaModule, DxDateBoxModule, DxFormModule, DxRadioGroupModule, DxTemplateModule, DxButtonModule, DxButtonGroupModule, DxColorBoxModule, DxTabPanelModule, DxTextBoxModule } from 'devextreme-angular';
import { BoxModule } from 'angular-admin-lte';

@NgModule({
  imports: [
    CommonModule,
    MaterialHistoryRoutingModule,
    DxSelectBoxModule,
    DxTextAreaModule,
    DxDateBoxModule,
    DxFormModule,
    DxTemplateModule,
    DxButtonModule,
    DxButtonGroupModule,
    DxTabPanelModule,
    DxTextBoxModule,
    DxTextAreaModule,
    BoxModule,
    DxListModule,
    DxPopupModule,
    DxValidatorModule,
    DxNumberBoxModule,
    DxCheckBoxModule
  ],
  providers: [DatePipe],
  exports: [MaterialHistoryComponent],
  declarations: [MaterialHistoryComponent]
})
export class MaterialHistoryModule { }
