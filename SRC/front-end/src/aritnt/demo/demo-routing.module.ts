import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DemoComponent } from './demo.component';
import { PageForbiddenComponent } from 'src/core/layout/page-forbidden/page-forbidden.component';
import { PageNotFoundComponent } from 'src/core/layout/page-not-found/page-not-found.component';

const routes: Routes = [{
  path: '',
  data: { title: 'Demo' },
  children: [
    {
      path: '',
      redirectTo: 'dashboard',
      pathMatch: 'full'
    },
    {
      path: 'dashboard',
      loadChildren: 'src/aritnt/demo/dashboard/dashboard.module#DashboardModule',
      data: { title: 'Dashboard' },
    }
  ]
},
{ path: 'forbidden', component: PageForbiddenComponent, data: { title: '403 Forbidden' } },
{ path: 'not-found', component: PageNotFoundComponent, data: { title: '404 Not found' } },
{ path: '**', redirectTo: 'not-found' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: false, onSameUrlNavigation: 'reload' })],
  exports: [RouterModule]
})
export class DemoRoutingModule { }
