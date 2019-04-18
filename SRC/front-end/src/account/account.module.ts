import { NgModule, APP_INITIALIZER, Injector } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HttpClient } from '@angular/common/http';

import { AccountRoutingModule } from './account-routing.module';
import { LogoutComponent } from './logout/logout.component';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoaderFactory } from 'src/core/common/language.service';
import { BrowserModule } from '@angular/platform-browser';
import { PageNotFoundComponent } from 'src/core/layout/page-not-found/page-not-found.component';
import { AccountComponent } from './account.component';
import { PageForbiddenComponent } from 'src/core/layout/page-forbidden/page-forbidden.component';
import { CookieService } from 'ngx-cookie-service';
import { HttpLoaderFactory } from 'src/core/common/config.service';
import { LoadingPageModule, MaterialBarModule } from 'angular-loading-page';

@NgModule({
  imports: [
    BrowserModule,
    HttpClientModule,
    AccountRoutingModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: TranslateHttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    LoadingPageModule,
    MaterialBarModule,
  ],
  exports: [
    TranslateModule
  ],
  declarations: [
    AccountComponent,
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
  bootstrap: [AccountComponent],
})
export class AccountModule { }
