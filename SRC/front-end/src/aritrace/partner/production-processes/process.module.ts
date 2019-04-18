import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProcessRoutingModule } from './process-routing.module';
import { ProcessComponent } from './process.component';

@NgModule({
  imports: [
    CommonModule,
    ProcessRoutingModule
  ],
  declarations: [ProcessComponent]
})
export class ProcessModule { }
