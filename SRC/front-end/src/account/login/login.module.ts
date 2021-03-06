import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login.component';
import { FormsModule } from '@angular/forms';
import { DeviceDetectorModule } from 'ngx-device-detector';

@NgModule({
  imports: [
    CommonModule,
    LoginRoutingModule,
    FormsModule,
    DeviceDetectorModule.forRoot()
  ],
  declarations: [LoginComponent]
})
export class LoginModule { }
