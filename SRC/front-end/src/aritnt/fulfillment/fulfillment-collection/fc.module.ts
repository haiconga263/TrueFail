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
import { OrderRoutingModule } from './fc-routing.module';
import { FCMasterComponent } from './fc-master/fc-master.component';
import { FCDetailComponent } from './fc-detail/fc-detail.component';
import { InventoryService } from 'src/aritnt/administrator/common/services/inventory.service';
import { OrderService } from 'src/aritnt/retailer/order/order.service';
import {FulfillmentService } from '../fulfillment.service';
import { ProductService } from 'src/aritnt/administrator/product/product.service';
import { UoMService } from 'src/aritnt/administrator/common/services/uom.service';
import { ActivatedRoute } from '@angular/router';


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
    DxDateBoxModule,
    
  ],
  declarations: [
    FCMasterComponent,
    FCDetailComponent,
  ],
  providers: [
    InventoryService,
    OrderService,
    FulfillmentService,
    ProductService,
    UoMService
  ]
})
export class FCModule { }
