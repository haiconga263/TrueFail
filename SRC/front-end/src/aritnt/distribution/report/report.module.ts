import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReportRoutingModule } from './report-routing.module';
import { ReportService } from './report.service';
import { 
  DxDataGridModule,
  DxButtonModule,
  DxDateBoxModule,
  DxSelectBoxModule
} from 'devextreme-angular';
import { FormsModule } from '@angular/forms';

import { TripHistoryComponent } from './trip-history/trip-history.component';
import { OrderHistoryComponent } from './order-history/order-history.component';
import { DistributionService } from '../common/services/distribution.service';

@NgModule({
  imports: [
    CommonModule,
    DxDataGridModule,
    DxButtonModule,
    DxDateBoxModule,
    ReportRoutingModule,
    FormsModule,
    DxSelectBoxModule
  ],
  declarations: [
    TripHistoryComponent,
    OrderHistoryComponent
  ],
  providers: [ReportService, DistributionService]
})
export class ReportModule { }
