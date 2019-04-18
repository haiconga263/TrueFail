import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductRoutingModule } from './product-routing.module';
import { ProductComponent } from './master/product.component';
import { ProductDetailComponent } from './detail/product-detail.component';
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
  DxTreeViewModule,
  DxMenuModule
} from 'devextreme-angular';
import { ProductService } from './product.service';
import { UoMService } from '../common/services/uom.service';
import { FormsModule } from '@angular/forms';
import { CategoryService } from '../category/category.service';
import { AbivinService } from '../common/services/abivin.service';
import { ExcelService } from '../../../core/services/excel.service'

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ProductRoutingModule,
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
    DxMenuModule
  ],
  declarations: [ProductComponent, ProductDetailComponent],
  providers: [ProductService, UoMService, CategoryService, AbivinService, ExcelService]
})
export class ProductModule { }
