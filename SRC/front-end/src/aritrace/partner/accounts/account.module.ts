import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountRoutingModule } from './account-routing.module';
import { AccountComponent } from './account.component';
import { DxDataGridModule, DxSelectBoxModule, DxButtonModule, DxTextBoxModule, DxAutocompleteModule } from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    AccountRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxAutocompleteModule,
    DxTextBoxModule
  ],
  declarations: [AccountComponent]
})
export class AccountModule { }
