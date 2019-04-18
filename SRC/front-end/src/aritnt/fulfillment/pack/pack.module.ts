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
  DxNumberBoxModule,
  DxDropDownBoxModule,
  DxListModule,
  DxTagBoxModule,
} from 'devextreme-angular';
import { FormsModule } from '@angular/forms';
import { OrderRoutingModule } from './pack-routing.module';
import { PackMasterComponent } from './pack-master/pack-master.component';
import { PackDetailComponent } from './pack-detail/pack-detail.component';
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
    DxDateBoxModule,
    DxNumberBoxModule,
    DxDropDownBoxModule,
    DxListModule,
    DxTagBoxModule
  ],
  declarations: [
    PackMasterComponent,
    PackDetailComponent
  ],
  providers: [
    InventoryService,
    FulfillmentService,
    ProductService,
    UoMService
  ]
})
export class PackModule { }
