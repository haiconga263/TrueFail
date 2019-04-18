import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from "./dashboard.component";
import { DashBoardReportService } from './dashboard.service'
import { 
  DxDataGridModule,
  DxButtonModule,
  DxDateBoxModule,
  DxSelectBoxModule
} from 'devextreme-angular';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    DashboardRoutingModule,
    FormsModule,
    DxSelectBoxModule
  ],
  declarations: [DashboardComponent],
  providers: [DashBoardReportService]
})
export class DashboardModule { }
