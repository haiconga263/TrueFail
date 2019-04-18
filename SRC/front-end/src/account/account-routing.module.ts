import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LogoutComponent } from './logout/logout.component';
import { PageForbiddenComponent } from 'src/core/layout/page-forbidden/page-forbidden.component';
import { PageNotFoundComponent } from 'src/core/layout/page-not-found/page-not-found.component';

const routes: Routes =
  [
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    { path: 'login', loadChildren: './login/login.module#LoginModule', data: { title: 'Login', customLayout: true } },
    { path: 'register', loadChildren: './register/register.module#RegisterModule', data: { title: 'register', customLayout: true }, },
    { path: 'logout', loadChildren: './logout/logout.module#LogoutModule', data: { title: 'logout', customLayout: true }, },
    { path: 'forbidden', component: PageForbiddenComponent, data: { title: '403 Forbidden' } },
    { path: 'not-found', component: PageNotFoundComponent, data: { title: '404 Not found' } },
    { path: '**', redirectTo: 'not-found' },
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: false, onSameUrlNavigation: 'reload' })],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
