import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SupervisionRoutingModule } from './receiving-routing.module';
import { ReceivingComponent } from './receiving.component';
import { ReceivingService } from './receiving.service';
import { 
  DxDataGridModule,
  DxButtonModule,
  DxSelectBoxModule,
  DxPopupModule,
  DxTextBoxModule,
  DxDateBoxModule
} from 'devextreme-angular';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    SupervisionRoutingModule,
    FormsModule,
    DxPopupModule,
    DxTextBoxModule,
    DxDateBoxModule
  ],
  declarations: [ReceivingComponent],
  providers: [ReceivingService]
})
export class ReceivingModule { }
