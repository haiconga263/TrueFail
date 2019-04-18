import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageForbiddenComponent } from 'src/core/layout/page-forbidden/page-forbidden.component';
import { PageNotFoundComponent } from 'src/core/layout/page-not-found/page-not-found.component';
import { AuthenticationComponent } from 'src/core/Authentication/authentication.component';

const routes: Routes =
  [
    {
      path: '',
      data: { title: 'Retailer' },
      children: [
        { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
        { path: 'dashboard', loadChildren: 'src/aritnt/retailer/dashboard/dashboard.module#DashboardModule', data: { title: 'Dashboard' }, },
        { path: 'account', loadChildren: 'src/aritnt/retailer/account/account.module#AccountModule', data: { title: 'Quản lý tài khoản' }, },
        { path: 'location', loadChildren: 'src/aritnt/retailer/location/location.module#LocationModule', data: { title: 'Sổ địa chỉ' }, },
        { path: 'price', loadChildren: 'src/aritnt/retailer/price/price.module#PriceModule', data: { title: 'Bảng báo giá' }, },
        { path: 'planning', loadChildren: 'src/aritnt/retailer/planning/planning.module#PlanningModule', data: { title: 'Kế hoạch đặt hàng' }, },
        { path: 'order', loadChildren: 'src/aritnt/retailer/order/order.module#OrderModule', data: { title: 'Đơn mua hàng' }, },
        { path: 'order-tracing', loadChildren: 'src/aritnt/retailer/order-tracing/order-tracing.module#OrderTracingModule', data: { title: 'Theo dõi đơn hàng' }, },
        { path: 'order-history', loadChildren: 'src/aritnt/retailer/order-history/order-history.module#OrderHistoryModule', data: { title: 'Lịch sử mua hàng' }, },
      ]
    },
    { path: 'forbidden', component: PageForbiddenComponent, data: { title: '403 Forbidden' } },
    { path: 'not-found', component: PageNotFoundComponent, data: { title: '404 Not found' } },
    { path: 'authenticate', component: AuthenticationComponent, data: { title: 'authentication' }, },
    { path: '**', redirectTo: 'not-found' },
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: false, onSameUrlNavigation: 'reload' })],
  exports: [RouterModule]
})
export class RetailerRoutingModule { } 
