import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PostRoutingModule } from './post-routing.module';
import { PostComponent } from './master/post.component';
import { PostDetailComponent } from './detail/post-detail.component';
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
  DxHtmlEditorModule,
  DxTabPanelModule
} from 'devextreme-angular';
import { PostService } from './post.service';
import { FormsModule } from '@angular/forms';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { TopicService } from '../topic/topic.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    PostRoutingModule,
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
    NgMultiSelectDropDownModule,
    DxTabPanelModule
  ],
  declarations: [PostComponent, PostDetailComponent],
  providers: [PostService, TopicService]
})
export class PostModule { }
