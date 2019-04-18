import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageForbiddenComponent } from 'src/core/layout/page-forbidden/page-forbidden.component';
import { PageNotFoundComponent } from 'src/core/layout/page-not-found/page-not-found.component';
import { AuthenticationComponent } from 'src/core/Authentication/authentication.component';

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
      loadChildren: 'src/aritnt/distribution/dashboard/dashboard.module#DashboardModule', data: { title: 'Dashboard' },
    },
    {
      path: 'map',
      loadChildren: 'src/aritnt/distribution/map/map.module#MapModule', data: { title: 'Map' },
    },
    {
      path: 'router',
      loadChildren: 'src/aritnt/distribution/router/router.module#RouterModule', data: { title: 'Router' },
    },
    {
      path: 'trips',
      loadChildren: 'src/aritnt/distribution/trip/trip.module#TripModule', data: { title: 'Trip' },
    },
    {
      path: 'reports',
      loadChildren: 'src/aritnt/distribution/report/report.module#ReportModule', data: { title: 'Report' },
    }
  ]
},
{ path: 'forbidden', component: PageForbiddenComponent, data: { title: '403 Forbidden' } },
{ path: 'authenticate', component: AuthenticationComponent, data: { title: 'authentication' }, },
{ path: 'not-found', component: PageNotFoundComponent, data: { title: '404 Not found' } },
{ path: '**', redirectTo: 'not-found' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: false, onSameUrlNavigation: 'reload' })],
  exports: [RouterModule]
})
export class DistributionRoutingModule { }
