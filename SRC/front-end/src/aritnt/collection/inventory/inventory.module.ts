import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './inventory-routing.module';
import { InventoryComponent } from "./inventory.component";
import { InventoryService } from './inventory.service';
import { ProductService } from '../common/services/product.service';
import { UoMService } from '../common/services/uom.service';
import { ReceivingService } from '../receiving/receiving.service';
import { 
  DxDataGridModule,
  DxSelectBoxModule,
  DxButtonModule,
  DxTextBoxModule,
  DxNumberBoxModule,
  DxPopupModule,
  DxValidatorModule
} from 'devextreme-angular';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    DashboardRoutingModule,
    FormsModule,
    DxDataGridModule,
    DxSelectBoxModule,
    DxButtonModule,
    DxTextBoxModule,
    DxNumberBoxModule,
    DxPopupModule,
    DxValidatorModule
  ],
  declarations: [InventoryComponent],
  providers: [InventoryService, ProductService, UoMService, ReceivingService]
})
export class InventoryModule { }
