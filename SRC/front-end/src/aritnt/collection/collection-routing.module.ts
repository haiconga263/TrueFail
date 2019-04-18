import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageForbiddenComponent } from 'src/core/layout/page-forbidden/page-forbidden.component';
import { PageNotFoundComponent } from 'src/core/layout/page-not-found/page-not-found.component';
import { AuthenticationComponent } from 'src/core/Authentication/authentication.component';

const routes: Routes = [{
  path: '',
  data: { title: 'Nơi thu mua' },
  children: [
    {
      path: '',
      redirectTo: 'dashboard',
      pathMatch: 'full'
    },
    {
      path: 'dashboard',
      loadChildren: 'src/aritnt/collection/dashboard/dashboard.module#DashboardModule', data: { title: 'Dashboard' },
    },
    {
      path: 'receiving',
      loadChildren: 'src/aritnt/collection/receiving/receiving.module#ReceivingModule', data: { title: 'Lấy hàng' },
    },
    {
      path: 'shipping',
      loadChildren: 'src/aritnt/collection/shipping/shipping.module#ShippingModule', data: { title: 'Chuyển hàng' },
    },
    {
      path: 'inventory',
      loadChildren: 'src/aritnt/collection/inventory/inventory.module#InventoryModule', data: { title: 'Kho' },
    },
    {
      path: 'reports',
      loadChildren: 'src/aritnt/collection/report/report.module#ReportModule', data: { title: 'Báo cáo' },
    },
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
export class CollectionRoutingModule { }
