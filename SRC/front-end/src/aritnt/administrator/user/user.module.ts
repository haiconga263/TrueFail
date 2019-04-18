import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './master/user.component';
import { UserDetailComponent } from './detail/user-detail.component';
import { DxDataGridModule, DxButtonModule, DxTextBoxModule, DxValidatorModule, DxCheckBoxModule } from 'devextreme-angular';
import { FormsModule } from '@angular/forms';
import { UserService } from './user.service';
import { SweetAlert2Module } from '@toverux/ngx-sweetalert2';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    UserRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxTextBoxModule,
    DxValidatorModule,
    DxCheckBoxModule,
    SweetAlert2Module
  ],
  declarations: [UserComponent, UserDetailComponent],
  providers: [UserService]
})
export class UserModule { }
