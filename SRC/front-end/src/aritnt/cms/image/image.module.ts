import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ImageRoutingModule } from './image-routing.module';
import { ImageComponent } from './master/image.component';
import { ImageDetailComponent } from './detail/image-detail.component';
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
  DxFileUploaderModule,
  DxTreeListModule
} from 'devextreme-angular';
import { ImageService } from './image.service';
import { FormsModule } from '@angular/forms';
import { ExcelService } from '../../../core/services/excel.service'

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ImageRoutingModule,
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
    DxFileUploaderModule,
    DxTreeListModule
  ],
  declarations: [ImageComponent, ImageDetailComponent],
  providers: [ImageService, ExcelService]
})
export class ImageModule { }
