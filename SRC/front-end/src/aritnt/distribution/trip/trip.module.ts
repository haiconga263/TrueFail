import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TripRoutingModule } from './trip-routing.module';
import { TripComponent } from './trip.component';
import { TripService } from './trip.service';
import { RouterService } from '../router/router.service';
import { EmployeeService } from '../common/services/employee.service';
import { UserService } from '../common/services/user.service'
import { VehicleService } from '../common/services/vehicle.service';
import { LocationService } from '../common/services/location.service';
import { DistributionService } from '../common/services/distribution.service';
import { DistributionEmployeeService } from '../common/services/distribution-employee.service';
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
  declarations: [TripComponent],
  providers: [TripService, RouterService, EmployeeService, UserService, VehicleService, LocationService, DistributionService, DistributionEmployeeService]
})
export class TripModule { }
