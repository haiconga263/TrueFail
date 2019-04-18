import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrderTracingRoutingModule } from './order-tracing-routing.module';
import { OrderTracingComponent } from './order-tracing.component';
import { 
  DxDataGridModule, 
  DxButtonModule, 
  DxSelectBoxModule, 
  DxTextBoxModule, 
  DxValidatorModule,
  DxCheckBoxModule,
  DxNumberBoxModule,
  DxLookupModule
} from 'devextreme-angular';
import { OrderService } from '../order/order.service';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    OrderTracingRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxTextBoxModule,
    DxValidatorModule,
    DxCheckBoxModule,
    DxNumberBoxModule,
    DxLookupModule
  ],
  declarations: [OrderTracingComponent],
  providers: [OrderService]
})
export class OrderTracingModule { }
