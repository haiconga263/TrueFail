import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrderRoutingModule } from './order-routing.module'
import { OrderComponent } from './master/order.component';
import { OrderDetailComponent } from './detail/order-detail.component';

import { 
  DxDataGridModule, 
  DxButtonModule, 
  DxSelectBoxModule, 
  DxTextBoxModule, 
  DxValidatorModule,
  DxDateBoxModule,
  DxRadioGroupModule
} from 'devextreme-angular';
import { OrderService } from './order.service';
import { ProductService } from '../common/services/product.service';
import { PlanningService } from '../planning/planning.service';
import { UoMService } from '../common/services/uom.service';
import { LocationService } from '../location/location.service';
import { FormsModule } from '@angular/forms';

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
    DxDateBoxModule,
    DxRadioGroupModule
  ],
  declarations: [
    OrderComponent,
    OrderDetailComponent
  ],
  providers: [
    OrderService,
    ProductService,
    UoMService,
    PlanningService,
    LocationService
  ]
})
export class OrderModule { }
