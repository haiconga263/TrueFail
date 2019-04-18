import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MaterialRoutingModule } from './material-routing.module';
import { MaterialComponent } from './material.component';

@NgModule({
  imports: [
    CommonModule,
    MaterialRoutingModule
  ],
  declarations: [MaterialComponent]
})
export class MaterialModule { }
