import { NgModule } from '@angular/core';
import { CommonModule } from 'node_modules/@angular/common';

import { SeedRoutingModule } from './seed-routing.module';
import { SeedComponent } from './master/seed.component';
import { SeedDetailComponent } from './detail/seed-detail.component';
import {
  DxDataGridModule,
  DxButtonModule,
  DxSelectBoxModule,
  DxTextBoxModule,
  DxValidatorModule,
  DxCheckBoxModule,
  DxNumberBoxModule,
  DxLookupModule,
  DxDropDownBoxModule,
  DxMenuModule,
  DxTreeViewModule,
  DxTreeListModule,
  DxTextAreaModule
} from 'devextreme-angular';
import { FormsModule } from '@angular/forms';
import { SeedService } from '../../common/services/seed.service';
import { ProductService } from 'src/aritnt/common/services/product.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    SeedRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxTextBoxModule,
    DxValidatorModule,
    DxCheckBoxModule,
    DxNumberBoxModule,
    DxLookupModule,
    DxDropDownBoxModule,
    DxTreeViewModule,
    DxMenuModule,
    DxTreeViewModule,
    DxTreeListModule,
    DxTextAreaModule
  ],
  declarations: [SeedComponent, SeedDetailComponent],
  providers: [SeedService, ProductService]
})
export class SeedModule { }
