import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PageRoutingModule } from './page-routing.module';
import { PageComponent } from './master/page.component';
import { PageDetailComponent } from './detail/page-detail.component';
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
  DxHtmlEditorModule
} from 'devextreme-angular';
import { PageService } from './page.service';
import { FormsModule } from '@angular/forms';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    PageRoutingModule,
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
    DxHtmlEditorModule,
    NgMultiSelectDropDownModule
  ],
  declarations: [PageComponent, PageDetailComponent],
  providers: [PageService]
})
export class PageModule { }
