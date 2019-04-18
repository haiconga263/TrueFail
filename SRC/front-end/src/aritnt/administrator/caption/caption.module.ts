import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CaptionRoutingModule } from './caption-routing.module';
import { CaptionComponent } from './master/caption.component';
import { CaptionDetailComponent } from './detail/caption-detail.component';

import { 
  DxDataGridModule, 
  DxButtonModule, 
  DxSelectBoxModule, 
  DxTextBoxModule, 
  DxValidatorModule,
} from 'devextreme-angular';
import { CaptionService } from './caption.service';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    CaptionRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxTextBoxModule,
    DxValidatorModule
  ],
  declarations: [
    CaptionComponent,
    CaptionDetailComponent
  ],
  providers: [
    CaptionService
  ]
})
export class CaptionModule { }
