import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageForbiddenComponent } from 'src/core/layout/page-forbidden/page-forbidden.component';
import { PageNotFoundComponent } from 'src/core/layout/page-not-found/page-not-found.component';
import { AuthenticationComponent } from 'src/core/Authentication/authentication.component';

const routes: Routes =
  [
    {
      path: '',
      data: { title: 'Partner' },
      children: [
        { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
        { path: 'dashboard', loadChildren: 'src/aritrace/partner/dashboard/dashboard.module#DashboardModule', data: { title: 'Dashboard' }, },
        { path: 'processes', loadChildren: 'src/aritrace/partner/production-processes/process.module#ProcessModule', data: { title: 'Production Process' }, },
        { path: 'productions', loadChildren: 'src/aritrace/partner/productions/production.module#ProductionModule', data: { title: 'Productions' }, },
        { path: 'accounts', loadChildren: 'src/aritrace/partner/accounts/account.module#AccountModule', data: { title: 'Accounts' }, },
        { path: 'materials', loadChildren: 'src/aritrace/partner/materials/material.module#MaterialModule', data: { title: 'Materials' }, },
      ]
    },
    { path: 'forbidden', component: PageForbiddenComponent, data: { title: '403 Forbidden' } },
    { path: 'not-found', component: PageNotFoundComponent, data: { title: '404 Not found' } },
    { path: 'authenticate', component: AuthenticationComponent, data: { title: 'authentication' }, },
    { path: '**', component: PageNotFoundComponent, data: { title: '404 Not found' } },
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: false, onSameUrlNavigation: 'reload' })],
  exports: [RouterModule]
})
export class PartnerRoutingModule { } 
