import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CompanyTypeRoutingModule } from './company-type-routing.module';
import { CompanyTypeComponent } from './company-type.component';

@NgModule({
  imports: [
    CommonModule,
    CompanyTypeRoutingModule,
  ],
  declarations: [CompanyTypeComponent]
})
export class CompanyTypeModule { }
