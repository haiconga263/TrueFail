import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { 
  DxDataGridModule, 
  DxButtonModule, 
  DxSelectBoxModule, 
  DxTextBoxModule, 
  DxValidatorModule,
  DxPopupModule,
  DxDateBoxModule,
} from 'devextreme-angular';
import { FormsModule } from '@angular/forms';
import { OrderRoutingModule } from './order-routing.module';
import { OrderMasterComponent } from './order-master/order-master.component';
import { OrderDetailComponent } from './order-detail/order-detail.component';
import { InventoryService } from 'src/aritnt/administrator/common/services/inventory.service';
import { FulfillmentService } from '../fulfillment.service';
import { ProductService } from 'src/aritnt/administrator/product/product.service';
import { UoMService } from 'src/aritnt/administrator/uom/uom.service';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    OrderRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxTextBoxModule,
    DxValidatorModule,
    DxPopupModule,
    DxDateBoxModule
  ],
  declarations: [
    OrderMasterComponent,
    OrderDetailComponent
  ],
  providers: [
    InventoryService,
    FulfillmentService,
    ProductService,
    UoMService
  ]
})
export class OrderModule { }
