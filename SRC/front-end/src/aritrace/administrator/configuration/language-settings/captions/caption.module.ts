import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CaptionRoutingModule } from './caption-routing.module';
import { CaptionComponent } from './caption.component';

@NgModule({
  imports: [
    CommonModule,
    CaptionRoutingModule
  ],
  declarations: [CaptionComponent]
})
export class CaptionModule { }
