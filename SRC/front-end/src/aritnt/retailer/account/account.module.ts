import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountRoutingModule } from './account-routing.module';
import { AccountComponent } from './master/account.component';
import { InforUpdateComponent } from './infor-update/infor-update.component';
import { AccountService } from './account.service';
import { GeoService } from '../common/services/geo.service';
import { AgmCoreModule } from '@agm/core';
import { 
  DxButtonModule,
  DxTextBoxModule,
  DxValidatorModule,
  DxCheckBoxModule,
  DxSelectBoxModule,
  DxNumberBoxModule,
  DxPopupModule
} from 'devextreme-angular';
import { FormsModule } from '@angular/forms';
@NgModule({
  imports: [
    CommonModule,
    AccountRoutingModule,
    DxButtonModule,
    DxTextBoxModule,
    FormsModule,
    DxValidatorModule,
    DxCheckBoxModule,
    DxSelectBoxModule,
    DxNumberBoxModule,
    AgmCoreModule,
    DxPopupModule
  ],
  declarations: [AccountComponent, InforUpdateComponent],
  providers: [AccountService, GeoService]
})
export class AccountModule { }
