import { NgModule, APP_INITIALIZER, Injector } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CMSRoutingModule } from './cms-routing.module';
import { CMSComponent } from './cms.component';
import { AuthenticationComponent } from 'src/core/Authentication/authentication.component'
import { CoreLayoutModule } from 'src/core/layout/core-layout.module';
import { LoadingPageModule, MaterialBarModule } from 'angular-loading-page';
import { LayoutModule } from 'angular-admin-lte';
import { adminLteConf } from 'src/aritnt/cms/admin-lte.conf';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoaderFactory } from 'src/core/common/language.service';
import { HttpClient } from '@angular/common/http';
import { PageForbiddenComponent } from 'src/core/layout/page-forbidden/page-forbidden.component';
import { PageNotFoundComponent } from 'src/core/layout/page-not-found/page-not-found.component';
import { CookieService } from 'ngx-cookie-service';
import { HttpLoaderFactory } from 'src/core/common/config.service';
import { AgmCoreModule } from '@agm/core';

@NgModule({
  imports: [
    BrowserModule,
    HttpClientModule,
    CommonModule,
    CMSRoutingModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: TranslateHttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    CoreLayoutModule,
    LayoutModule.forRoot(adminLteConf),
    LoadingPageModule, MaterialBarModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyD8-FUbLs-VXi4cRZfRO3dYQHXtUUUpoBc'
    })
  ],
  exports: [
    TranslateModule
  ],
  declarations: [
    CMSComponent,
    AuthenticationComponent,
    PageNotFoundComponent,
    PageForbiddenComponent],
  providers: [
    CookieService,
    {
      provide: APP_INITIALIZER,
      useFactory: HttpLoaderFactory,
      deps: [Injector],
      multi: true
    }
  ],
  bootstrap: [CMSComponent],
})
export class CMSModule { }
