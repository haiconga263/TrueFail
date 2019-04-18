import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TripRoutingModule } from './shipping-routing.module';
import { ShippingComponent } from './shipping.component';
import { ShippingService } from './shipping.service';
import { EmployeeService } from '../common/services/employee.service';
import { UserService } from '../common/services/user.service'
import { VehicleService } from '../common/services/vehicle.service';
import { ReceivingService } from '../receiving/receiving.service';
import { FulfillmentService } from '../common/services/fulfillment.service';
import { InventoryService } from '../inventory/inventory.service';
import { ProductService } from '../common/services/product.service';
import { UoMService } from '../common/services/uom.service';
import { 
  DxDataGridModule,
  DxButtonModule,
  DxSelectBoxModule
} from 'devextreme-angular';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    TripRoutingModule,
    FormsModule
  ],
  declarations: [ShippingComponent],
  providers: [
    ShippingService, 
    EmployeeService, 
    UserService, 
    VehicleService, 
    ReceivingService, 
    FulfillmentService, 
    InventoryService, 
    ProductService,
    UoMService
  ]
})
export class ShippingModule { }
