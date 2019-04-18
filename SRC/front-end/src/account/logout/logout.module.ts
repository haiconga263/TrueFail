import { NgModule } from '@angular/core';

import { CommonModule } from '@angular/common';

import { FormsModule } from '@angular/forms';
import { LogoutComponent } from './logout.component';
import { LogoutRoutingModule } from './logout-routing.module';

@NgModule({
  imports: [
    CommonModule,
    LogoutRoutingModule,
    FormsModule
  ],
  declarations: [LogoutComponent]
})
export class LogoutModule { }
