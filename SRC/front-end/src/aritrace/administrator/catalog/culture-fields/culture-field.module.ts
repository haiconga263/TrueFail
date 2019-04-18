import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CultureFieldRoutingModule } from './culture-field-routing.module';
import { CultureFieldComponent } from './culture-field.component';

@NgModule({
  imports: [
    CommonModule,
    CultureFieldRoutingModule,
  ],
  declarations: [CultureFieldComponent]
})
export class CultureFieldModule { }
