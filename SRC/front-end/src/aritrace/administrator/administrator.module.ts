import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreLayoutModule } from 'src/core/layout/core-layout.module';
import { LoadingPageModule, MaterialBarModule } from 'angular-loading-page';
import { LayoutModule } from 'angular-admin-lte';

import { AdministratorRoutingModule } from './administrator-routing.module';
import { AdministratorComponent } from './administrator.component';
import { adminLteConf } from 'src/aritrace/administrator/admin-lte.conf';
import { AppConfigModule } from 'src/core/basecommon/app-config.module';

@NgModule({
  imports: [
    AppConfigModule,
    CommonModule,
    CoreLayoutModule,
    AdministratorRoutingModule,
    LayoutModule.forRoot(adminLteConf),
    LoadingPageModule,
    MaterialBarModule,
  ],
  declarations: [
    AdministratorComponent
  ],
  bootstrap: [AdministratorComponent],
})
export class AdministratorModule { }
