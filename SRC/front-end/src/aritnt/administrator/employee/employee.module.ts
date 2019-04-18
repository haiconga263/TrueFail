import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmployeeRoutingModule } from './employee-routing.module';
import { EmployeeComponent } from './master/employee.component';
import { EmployeeDetailComponent } from './detail/employee-detail.component';
import { 
  DxDataGridModule, 
  DxButtonModule, 
  DxSelectBoxModule, 
  DxTextBoxModule, 
  DxValidatorModule,
  DxCheckBoxModule,
  DxDateBoxModule
} from 'devextreme-angular';
import { EmployeeService } from './employee.service';
import { UserService } from '../user/user.service';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    EmployeeRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxTextBoxModule,
    DxValidatorModule,
    DxCheckBoxModule,
    DxDateBoxModule
  ],
  declarations: [EmployeeComponent, EmployeeDetailComponent],
  providers: [EmployeeService, UserService]
})
export class EmployeeModule { }
