import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageForbiddenComponent } from 'src/core/layout/page-forbidden/page-forbidden.component';
import { PageNotFoundComponent } from 'src/core/layout/page-not-found/page-not-found.component';
import { AuthenticationComponent } from 'src/core/Authentication/authentication.component';

const routes: Routes =
  [
    {
      path: '',
      data: { title: 'Administrator' },
      children: [
        { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
        { path: 'dashboard', loadChildren: 'src/aritnt/administrator/dashboard/dashboard.module#DashboardModule', data: { title: 'Dashboard' }, },
        { path: 'user', loadChildren: 'src/aritnt/administrator/user/user.module#UserModule', data: { title: 'User management' }, },
        { path: 'product', loadChildren: 'src/aritnt/administrator/product/product.module#ProductModule', data: { title: 'Product management' }, },
        { path: 'category', loadChildren: 'src/aritnt/administrator/category/category.module#CategoryModule', data: { title: 'Category management' }, },
        { path: 'employee', loadChildren: 'src/aritnt/administrator/employee/employee.module#EmployeeModule', data: { title: 'Employee management' }, },
        { path: 'farmer', loadChildren: 'src/aritnt/administrator/farmer/farmer.module#FarmerModule', data: { title: 'Farmer management' }, },
        { path: 'retailer', loadChildren: 'src/aritnt/administrator/retailer/retailer.module#RetailerModule', data: { title: 'Retailer management' }, },
        { path: 'geo', loadChildren: 'src/aritnt/administrator/geographical/geo.module#GeoModule', data: { title: 'Geographical management' }, },
        { path: 'collection', loadChildren: 'src/aritnt/administrator/collection/collection.module#CollectionModule', data: { title: 'Collection management' }, },
        { path: 'fulfillment', loadChildren: 'src/aritnt/administrator/fulfillment/fulfillment.module#FulfillmentModule', data: { title: 'Fulfillment management' }, },
        { path: 'distribution', loadChildren: 'src/aritnt/administrator/distribution/distribution.module#DistributionModule', data: { title: 'Distribution management' }, },
        { path: 'caption', loadChildren: 'src/aritnt/administrator/caption/caption.module#CaptionModule', data: { title: 'Language management' }, },
        { path: 'vehicle', loadChildren: 'src/aritnt/administrator/vehicle/vehicle.module#VehicleModule', data: { title: 'Vehicle management' }, }
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
export class AdministratorRoutingModule { } 
