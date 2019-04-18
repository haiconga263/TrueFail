import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RetailerRoutingModule } from './retailer-routing.module';
import { RetailerComponent } from './retailer/master/retailer.component';
import { RetailerDetailComponent } from './retailer/detail/retailer-detail.component'
import { LocationDetailComponent } from './retailer/location-detail/location-detail.component';
import { PlanningComponent } from './planning/master/planning.component';
import { PlanningHistoryComponent } from './planning/master-history/planning-history.component';
import { OrderComponent } from './order/master/order.component';
import { OrderDetailComponent } from './order/detail/order-detail.component';
import { OrderHistoryComponent } from './order/master-history/order-history.component';

import { 
  DxDataGridModule, 
  DxButtonModule, 
  DxSelectBoxModule, 
  DxTextBoxModule, 
  DxValidatorModule,
  DxCheckBoxModule,
  DxNumberBoxModule,
  DxPopupModule,
  DxDateBoxModule
} from 'devextreme-angular';
import { RetailerService } from './retailer.service';
import { RetailerPlanningService } from './retailer-planning.service';
import { RetailerOrderService } from './retailer-order.service';
import { GeoService } from '../geographical/geo.service';
import { UserService } from '../user/user.service';
import { ProductService } from '../product/product.service';
import { UoMService } from 'src/aritnt/retailer/common/services/uom.service';
import { FormsModule } from '@angular/forms';
import { InventoryService } from '../common/services/inventory.service';
import { OrderTempComponent } from './order-homepage/datagrid/order-temp-datagrid.component';
import { RetailerOrderTempService } from './order-homepage/retailer-order-temp.service';
import { AgmCoreModule } from '@agm/core';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RetailerRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxTextBoxModule,
    DxValidatorModule,
    DxCheckBoxModule,
    DxNumberBoxModule,
    DxPopupModule,
    DxDateBoxModule,
    AgmCoreModule
  ],
  declarations: [
    RetailerComponent, 
    RetailerDetailComponent, 
    LocationDetailComponent, 
    PlanningComponent, 
    PlanningHistoryComponent,
    OrderComponent,
    OrderHistoryComponent,
    OrderDetailComponent,
    OrderTempComponent
  ],
  providers: [
    RetailerService, 
    GeoService, 
    UserService, 
    RetailerPlanningService, 
    RetailerOrderService,
    ProductService,
    UoMService,
    InventoryService,
    RetailerOrderTempService
  ]
})
export class RetailerModule { }
