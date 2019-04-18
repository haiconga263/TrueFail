import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReportRoutingModule } from './report-routing.module';
import { ReportService } from './report.service';
import { ReceivingService } from '../receiving/receiving.service'
import { EmployeeService } from "../common/services/employee.service";
import { ShippingService } from '../shipping/shipping.service';
import { 
  DxDataGridModule,
  DxButtonModule,
  DxDateBoxModule,
  DxSelectBoxModule
} from 'devextreme-angular';
import { FormsModule } from '@angular/forms';

import { EmployeeHistoryComponent } from './employee-history/employee-history.component';
import { OrderHistoryComponent } from './order-history/order-history.component';
import { InventoryHistoryComponent } from './inventory-history/inventory-history.component';
import { ShippingHistoryComponent } from './shipping-history/shipping-history.component';
import { ProductService } from '../common/services/product.service';
import { UoMService } from '../common/services/uom.service';
import { VehicleService } from '../common/services/vehicle.service';
import { FulfillmentService } from '../common/services/fulfillment.service';

@NgModule({
  imports: [
    CommonModule,
    DxDataGridModule,
    DxButtonModule,
    DxDateBoxModule,
    ReportRoutingModule,
    FormsModule,
    DxSelectBoxModule
  ],
  declarations: [
    EmployeeHistoryComponent,
    OrderHistoryComponent,
    InventoryHistoryComponent,
    ShippingHistoryComponent
  ],
  providers: [ShippingService, ReportService, EmployeeService, ProductService, UoMService, ReceivingService, VehicleService, FulfillmentService]
})
export class ReportModule { }
