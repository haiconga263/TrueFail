import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreLayoutModule } from 'src/core/layout/core-layout.module';
import { LoadingPageModule, MaterialBarModule } from 'angular-loading-page';
import { LayoutModule } from 'angular-admin-lte';
import { AgmCoreModule } from '@agm/core';

import { AppConfigModule } from 'src/core/basecommon/app-config.module';
import { CollectionRoutingModule } from './collection-routing.module';
import { CollectionComponent } from './collection.component';
import { adminLteConf } from 'src/aritnt/collection/admin-lte.conf';

@NgModule({
  imports: [
    AppConfigModule,
    CommonModule,
    CoreLayoutModule,
    CollectionRoutingModule,
    LayoutModule.forRoot(adminLteConf),
    LoadingPageModule, MaterialBarModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyD8-FUbLs-VXi4cRZfRO3dYQHXtUUUpoBc'
    })
  ],
  declarations: [
    CollectionComponent,
  ],
  bootstrap: [CollectionComponent],
})
export class CollectionModule { }
