import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PlanningRoutingModule } from './planning-routing.module'
import { PlanningComponent } from './master/planning.component';
import { PlanningDetailComponent } from './detail/planning-detail.component';

import { 
  DxDataGridModule, 
  DxButtonModule, 
  DxSelectBoxModule, 
  DxTextBoxModule, 
  DxValidatorModule,
  DxDateBoxModule
} from 'devextreme-angular';
import { PlanningService } from './planning.service';
import { ProductService } from '../common/services/product.service';
import { UoMService } from '../common/services/uom.service';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    PlanningRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxTextBoxModule,
    DxValidatorModule,
    DxDateBoxModule
  ],
  declarations: [
    PlanningComponent,
    PlanningDetailComponent
  ],
  providers: [
    PlanningService,
    ProductService,
    UoMService
  ]
})
export class PlanningModule { }
