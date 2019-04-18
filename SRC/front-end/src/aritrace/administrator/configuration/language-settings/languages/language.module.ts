import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LanguageRoutingModule } from './language-routing.module';
import { LanguageComponent } from './language.component';
import { DxDataGridModule, DxSelectBoxModule, DxButtonModule, DxLookupModule } from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    LanguageRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxLookupModule,
  ],
  declarations: [LanguageComponent]
})
export class LanguageModule { }
