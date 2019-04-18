import { NgModule, APP_INITIALIZER, Injector } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoaderFactory, TranslateLoaderHelper } from 'src/core/common/language.service';
import { HttpClient } from '@angular/common/http';
import { PageForbiddenComponent } from 'src/core/layout/page-forbidden/page-forbidden.component';
import { PageNotFoundComponent } from 'src/core/layout/page-not-found/page-not-found.component';
import { CookieService } from 'ngx-cookie-service';
import { HttpLoaderFactory } from 'src/core/common/config.service';
import { AuthenticationComponent } from 'src/core/Authentication/authentication.component';
import { SweetAlert2Module } from '@toverux/ngx-sweetalert2';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useClass: TranslateLoaderHelper,
        // useFactory: TranslateHttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    SweetAlert2Module.forRoot({
      buttonsStyling: false,
      customClass: 'modal-content',
      confirmButtonClass: 'btn btn-primary',
      cancelButtonClass: 'btn'
    }),
  ],
  exports: [
    TranslateModule,
  ],
  declarations: [
    PageNotFoundComponent,
    PageForbiddenComponent,
    AuthenticationComponent
  ],
  providers: [
    CookieService,
    {
      provide: APP_INITIALIZER,
      useFactory: HttpLoaderFactory,
      deps: [Injector],
      multi: true
    },
  ],
})
export class AppConfigModule { }
