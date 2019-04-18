import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrderHistoryRoutingModule } from './order-history-routing.module'
import { OrderHistoryComponent } from './master/order-history.component';
import { OrderHistoryDetailComponent } from './detail/order-history-detail.component';

import { 
  DxDataGridModule, 
  DxButtonModule, 
  DxSelectBoxModule, 
  DxTextBoxModule, 
  DxValidatorModule,
  DxDateBoxModule
} from 'devextreme-angular';
import { OrderService } from '../order/order.service';
import { ProductService } from '../common/services/product.service';
import { UoMService } from '../common/services/uom.service';
import { LocationService } from '../location/location.service';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    OrderHistoryRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxTextBoxModule,
    DxValidatorModule,
    DxDateBoxModule
  ],
  declarations: [
    OrderHistoryComponent,
    OrderHistoryDetailComponent
  ],
  providers: [
    OrderService,
    ProductService,
    UoMService,
    LocationService
  ]
})
export class OrderHistoryModule { }
