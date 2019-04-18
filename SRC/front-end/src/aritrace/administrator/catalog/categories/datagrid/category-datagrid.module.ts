import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DxDataGridModule, DxSelectBoxModule, DxButtonModule } from 'devextreme-angular';
import { CategoryDatagridComponent } from './category-datagrid.component';
import { CategoryDatagridRoutingModule } from './category-datagrid-routing.module';

@NgModule({
  imports: [
    CommonModule,
    CategoryDatagridRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule
  ],
  declarations: [CategoryDatagridComponent]
})
export class CategoryDatagridModule { }
