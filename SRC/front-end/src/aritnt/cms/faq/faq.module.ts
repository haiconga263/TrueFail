import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FaqRoutingModule } from './faq-routing.module';
import { FaqComponent } from './master/faq.component';
import { FaqDetailComponent } from './detail/faq-detail.component';
import { 
  DxDataGridModule, 
  DxButtonModule, 
  DxSelectBoxModule, 
  DxTextBoxModule, 
  DxValidatorModule,
  DxCheckBoxModule,
  DxNumberBoxModule,
  DxLookupModule,
  DxDropDownBoxModule,
  DxTreeViewModule,
  DxMenuModule,
  DxTextAreaModule
} from 'devextreme-angular';
import { FaqService } from './faq.service';
import { FormsModule } from '@angular/forms';
import { ExcelService } from '../../../core/services/excel.service'

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    FaqRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxTextBoxModule,
    DxValidatorModule,
    DxCheckBoxModule,
    DxNumberBoxModule,
    DxLookupModule,
    DxDropDownBoxModule,
    DxTreeViewModule,
    DxMenuModule,
    DxTextAreaModule
  ],
  declarations: [FaqComponent, FaqDetailComponent],
  providers: [FaqService, ExcelService]
})
export class FaqModule { }
