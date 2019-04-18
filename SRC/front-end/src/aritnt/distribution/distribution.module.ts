import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreLayoutModule } from 'src/core/layout/core-layout.module';
import { LoadingPageModule, MaterialBarModule } from 'angular-loading-page';
import { LayoutModule } from 'angular-admin-lte';
import { AgmCoreModule } from '@agm/core';

import { DistributionRoutingModule } from './distribution-routing.module';
import { DistributionComponent } from './distribution.component';
import { adminLteConf } from 'src/aritnt/distribution/admin-lte.conf';
import { AppConfigModule } from 'src/core/basecommon/app-config.module';

@NgModule({
  imports: [
    AppConfigModule,
    CommonModule,
    CoreLayoutModule,
    DistributionRoutingModule,
    LayoutModule.forRoot(adminLteConf),
    LoadingPageModule, MaterialBarModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyD8-FUbLs-VXi4cRZfRO3dYQHXtUUUpoBc'
    })
  ],
  declarations: [
    DistributionComponent,
   ],
  bootstrap: [DistributionComponent],
})
export class DistributionModule { }
