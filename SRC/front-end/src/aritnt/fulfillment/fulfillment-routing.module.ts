import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageForbiddenComponent } from 'src/core/layout/page-forbidden/page-forbidden.component';
import { PageNotFoundComponent } from 'src/core/layout/page-not-found/page-not-found.component';
import { AuthenticationComponent } from 'src/core/Authentication/authentication.component';

const routes: Routes = [{
  path: '',
  data: { title: 'Nơi đóng gói' },
  children: [
    {
      path: '',
      redirectTo: 'dashboard',
      pathMatch: 'full'
    },
    {
      path: 'dashboard',
      loadChildren: 'src/aritnt/fulfillment/dashboard/dashboard.module#DashboardModule', data: { title: 'Dashboard' },
    },      
    {
      path: 'ful-collection',
      loadChildren: 'src/aritnt/fulfillment/fulfillment-collection/fc.module#FCModule', 
      data: { title: 'Nhận hàng' },
    },
    {
      path: 'ful-pack',
      loadChildren: 'src/aritnt/fulfillment/pack/pack.module#PackModule', 
      data: { title: 'Đóng gói' },
    }
    // // { path: 'shipping', component: ShippingComponent, data: { title: 'Xuất kho' } },
    // {
    //   path: 'shipping',
    //   loadChildren: 'src/aritnt/fulfillment/shipping/shipping.module#ShippingModule', 
    //   data: { title: 'Xuất kho' },
    // }
    // {
    //   path: 'reports',
    //   loadChildren: 'src/aritnt/fulfillment/report/report.module#ReportModule', data: { title: 'Lấy hàng' },
    // },
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
export class FulfillmentRoutingModule { }
