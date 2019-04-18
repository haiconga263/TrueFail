import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FulfillmentRoutingModule } from './fulfillment-routing.module';
import { FulfillmentComponent } from './master/fulfillment.component';
import { FulfillmentDetailComponent } from './detail/fulfillment-detail.component';
import { 
  DxDataGridModule, 
  DxButtonModule, 
  DxSelectBoxModule, 
  DxTextBoxModule, 
  DxValidatorModule,
  DxCheckBoxModule,
  DxDateBoxModule,
  DxNumberBoxModule,
  DxPopupModule
} from 'devextreme-angular';
import { FulfillmentService } from './fulfillment.service';
import { EmployeeService } from '../employee/employee.service';
import { FormsModule } from '@angular/forms';
import { GeoService } from '../geographical/geo.service';
import { AgmCoreModule } from '@agm/core';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    FulfillmentRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxTextBoxModule,
    DxValidatorModule,
    DxCheckBoxModule,
    DxDateBoxModule,
    DxNumberBoxModule,
    AgmCoreModule,
    DxPopupModule
  ],
  declarations: [FulfillmentComponent, FulfillmentDetailComponent],
  providers: [FulfillmentService, EmployeeService, GeoService]
})
export class FulfillmentModule { }
