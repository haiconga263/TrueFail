import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CompanyDatagridRoutingModule } from './company-datagrid-routing.module';
import { CompanyDatagridComponent } from './company-datagrid.component';
import { DxButtonModule, DxSelectBoxModule, DxDataGridModule } from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    CompanyDatagridRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule
  ],
  declarations: [CompanyDatagridComponent]
})
export class CompanyDatagridModule { }
