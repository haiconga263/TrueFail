
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GLNRoutingModule } from './gln-routing.module';
import { GLNComponent } from './gln.component';

@NgModule({
  imports: [
    CommonModule,
    GLNRoutingModule,
  ],
  declarations: [GLNComponent]
})
export class GLNModule { }
