import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductionRoutingModule } from './production-routing.module';
import { ProductionComponent } from './production.component';

@NgModule({
  imports: [
    CommonModule,
    ProductionRoutingModule
  ],
  declarations: [ProductionComponent]
})
export class ProductionModule { }
