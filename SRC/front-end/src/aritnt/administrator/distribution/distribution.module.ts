import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DistributionRoutingModule } from './distribution-routing.module';
import { DistributionComponent } from './master/distribution.component';
import { DistributionDetailComponent } from './detail/distribution-detail.component';
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
import { DistributionService } from './distribution.service';
import { DistributionEmployeeService } from './distribution-employee.service';
import { EmployeeService } from '../employee/employee.service';
import { UserService } from '../user/user.service';
import { FormsModule } from '@angular/forms';
import { GeoService } from '../geographical/geo.service';
import { AgmCoreModule } from '@agm/core';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    DistributionRoutingModule,
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
  declarations: [DistributionComponent, DistributionDetailComponent],
  providers: [DistributionService, EmployeeService, GeoService, DistributionEmployeeService, UserService]
})
export class DistributionModule { }
