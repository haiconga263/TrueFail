import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CollectionRoutingModule } from './collection-routing.module';
import { CollectionComponent } from './master/collection.component';
import { CollectionDetailComponent } from './detail/collection-detail.component';
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
import { CollectionService } from './collection.service';
import { UserService } from '../user/user.service';
import { CollectionEmployeeService } from './collection-employee.service';
import { EmployeeService } from '../employee/employee.service';
import { FormsModule } from '@angular/forms';
import { GeoService } from '../geographical/geo.service';
import { AgmCoreModule } from '@agm/core';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    CollectionRoutingModule,
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
  declarations: [CollectionComponent, CollectionDetailComponent],
  providers: [CollectionService, EmployeeService, GeoService, CollectionEmployeeService, UserService]
})
export class CollectionModule { }
