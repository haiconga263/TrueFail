import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DxDataGridModule, DxSelectBoxModule, DxButtonModule } from 'devextreme-angular';
import { CompanyTypeDatagridComponent } from './company-type-datagrid.component';
import { CompanyTypeDatagridRoutingModule } from './company-type-datagrid-routing.module';

@NgModule({
  imports: [
    CommonModule,
    CompanyTypeDatagridRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule
  ],
  declarations: [CompanyTypeDatagridComponent]
})
export class CompanyTypeDatagridModule { }
