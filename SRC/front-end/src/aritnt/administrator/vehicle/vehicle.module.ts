import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VehicleRoutingModule } from './vehicle-routing.module';
import { VehicleComponent } from './master/vehicle.component';
import { VehicleDetailComponent } from './detail/vehicle-detail.component';
import { 
  DxDataGridModule, 
  DxButtonModule, 
  DxSelectBoxModule, 
  DxTextBoxModule, 
  DxValidatorModule,
  DxCheckBoxModule,
  DxNumberBoxModule,
  DxMenuModule,
  DxDateBoxModule
} from 'devextreme-angular';
import { VehicleService } from './vehicle.service';
import { FormsModule } from '@angular/forms';
import { AbivinService } from '../common/services/abivin.service';
import { DistributionService } from '../distribution/distribution.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    VehicleRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxTextBoxModule,
    DxValidatorModule,
    DxCheckBoxModule,
    DxNumberBoxModule,
    DxMenuModule,
    DxDateBoxModule
  ],
  declarations: [VehicleComponent, VehicleDetailComponent],
  providers: [VehicleService, AbivinService, DistributionService]
})
export class VehicleModule { }
