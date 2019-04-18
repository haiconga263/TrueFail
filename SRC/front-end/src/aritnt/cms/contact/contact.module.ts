import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ContactRoutingModule } from './contact-routing.module';
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
  DxTextAreaModule,
  DxDateBoxModule,
  
} from 'devextreme-angular';
import { FormsModule } from '@angular/forms';
import { ExcelService } from '../../../core/services/excel.service'
import { ContactComponent } from './master/contact.component';
import { ContactDetailComponent } from './detail/contact-detail.component';
import { ContactService } from './contact.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ContactRoutingModule,
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
    DxTextAreaModule,
    DxDateBoxModule,
  ],
  declarations: [ContactComponent, ContactDetailComponent],
  providers: [ContactService, ExcelService]
})
export class ContactModule { }
