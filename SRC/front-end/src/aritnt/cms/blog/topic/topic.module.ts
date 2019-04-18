import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TopicRoutingModule } from './topic-routing.module';
import { TopicComponent } from './master/topic.component';
import { TopicDetailComponent } from './detail/topic-detail.component';
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
import { TopicService } from './topic.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    TopicRoutingModule,
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
    DxDateBoxModule
  ],
  declarations: [TopicComponent, TopicDetailComponent],
  providers: [TopicService]
})
export class TopicModule { }
