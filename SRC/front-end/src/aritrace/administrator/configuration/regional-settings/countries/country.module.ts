import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CountryRoutingModule } from './country-routing.module';
import { CountryComponent } from './country.component';
import { DxDataGridModule, DxSelectBoxModule, DxButtonModule } from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    CountryRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule
  ],
  declarations: [CountryComponent]
})
export class CountryModule { }
