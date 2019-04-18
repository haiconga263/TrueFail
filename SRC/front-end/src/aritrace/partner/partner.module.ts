import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreLayoutModule } from 'src/core/layout/core-layout.module';
import { LoadingPageModule, MaterialBarModule } from 'angular-loading-page';
import { LayoutModule } from 'angular-admin-lte';

import { PartnerRoutingModule } from './partner-routing.module';
import { PartnerComponent } from './partner.component';
import { adminLteConf } from 'src/aritrace/partner/admin-lte.conf';
import { AppConfigModule } from 'src/core/basecommon/app-config.module';

@NgModule({
  imports: [
    AppConfigModule,
    CommonModule,
    CoreLayoutModule,
    PartnerRoutingModule,
    LayoutModule.forRoot(adminLteConf),
    LoadingPageModule,
    MaterialBarModule,
  ],
  declarations: [
    PartnerComponent
  ],
  bootstrap: [PartnerComponent],
})
export class PartnerModule { }
