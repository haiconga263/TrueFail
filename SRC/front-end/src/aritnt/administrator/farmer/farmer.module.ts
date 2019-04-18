import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FarmerRoutingModule } from './farmer-routing.module';
import { FarmerComponent } from './farmer/master/farmer.component';
import { FarmerDetailComponent } from './farmer/detail/farmer-detail.component';
import { PlanningComponent } from './planning/master/planning.component';
import { PlanningHistoryComponent } from './planning/master-history/planning-history.component';
import { OrderComponent } from './order/master/order.component';
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
  DxDateBoxModule,
  DxRadioGroupModule
} from 'devextreme-angular';
import { FarmerService } from './farmer.service';
import { FarmerPlanningService } from './farmer-planning.service';
import { FarmerOrderService } from './farmer-order.service';
import { GeoService } from '../geographical/geo.service';
import { UserService } from '../user/user.service';
import { ProductService } from '../product/product.service';
import { UoMService } from '../uom/uom.service';
import { CollectionService } from '../collection/collection.service';
import { OrderDetailComponent } from './order/detail/order-detail.component';

import { FormsModule } from '@angular/forms';
import { AgmCoreModule } from '@agm/core';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    FarmerRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxTextBoxModule,
    DxValidatorModule,
    DxCheckBoxModule,
    DxNumberBoxModule,
    DxPopupModule,
    DxDateBoxModule,
    DxRadioGroupModule,
    AgmCoreModule
  ],
  declarations: [FarmerComponent, FarmerDetailComponent, PlanningComponent, PlanningHistoryComponent, OrderComponent, OrderHistoryComponent, OrderDetailComponent],
  providers: [FarmerService, FarmerPlanningService, FarmerOrderService, GeoService, UserService, ProductService, UoMService, CollectionService]
})
export class FarmerModule { }
