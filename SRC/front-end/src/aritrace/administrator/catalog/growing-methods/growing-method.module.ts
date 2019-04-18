import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GrowingMethodRoutingModule } from './growing-method-routing.module';
import { GrowingMethodComponent } from './growing-method.component';

@NgModule({
  imports: [
    CommonModule,
    GrowingMethodRoutingModule,
  ],
  declarations: [GrowingMethodComponent]
})
export class GrowingMethodModule { }
