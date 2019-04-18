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
        { path: 'dashboard', loadChildren: 'src/aritrace/administrator/dashboard/dashboard.module#DashboardModule', data: { title: 'Dashboard' }, },
        { path: 'companies', loadChildren: 'src/aritrace/administrator/companies/company.module#CompanyModule', data: { title: 'Companies' }, },

        // *** catalog
        { path: 'products', loadChildren: 'src/aritrace/administrator/catalog/products/product.module#ProductModule', data: { title: 'Products' }, },
        { path: 'categories', loadChildren: 'src/aritrace/administrator/catalog/categories/category.module#CategoryModule', data: { title: 'Categories' }, },
        { path: 'company-types', loadChildren: 'src/aritrace/administrator/catalog/company-types/company-type.module#CompanyTypeModule', data: { title: 'Company Types' }, },
        { path: 'growing-methods', loadChildren: 'src/aritrace/administrator/catalog/growing-methods/growing-method.module#GrowingMethodModule', data: { title: 'Growing Methods' }, },
        { path: 'culture-fields', loadChildren: 'src/aritrace/administrator/catalog/culture-fields/culture-field.module#CultureFieldModule', data: { title: 'Culture Fields' }, },

        // *** users
        { path: 'accounts', loadChildren: 'src/aritrace/administrator/users/accounts/account.module#AccountModule', data: { title: 'Accounts' }, },
        { path: 'roles', loadChildren: 'src/aritrace/administrator/users/roles/role.module#RoleModule', data: { title: 'Roles' }, },

        // *** configuration
        // Lists
        { path: 'uoms', loadChildren: 'src/aritrace/administrator/configuration/lists/uoms/uom.module#UOMModule', data: { title: 'Unit Of Measurement' }, },

        // Regional setting
        { path: 'countries', loadChildren: 'src/aritrace/administrator/configuration/regional-settings/countries/country.module#CountryModule', data: { title: 'Countries' }, },
        { path: 'provinces', loadChildren: 'src/aritrace/administrator/configuration/regional-settings/provinces/province.module#ProvinceModule', data: { title: 'Provinces' }, },
        { path: 'regions', loadChildren: 'src/aritrace/administrator/configuration/regional-settings/regions/region.module#RegionModule', data: { title: 'Regions' }, },
        { path: 'districts', loadChildren: 'src/aritrace/administrator/configuration/regional-settings/districts/district.module#DistrictModule', data: { title: 'Districts' }, },
        { path: 'wards', loadChildren: 'src/aritrace/administrator/configuration/regional-settings/wards/ward.module#WardModule', data: { title: 'Wards' }, },

        // Language setting
        { path: 'languages', loadChildren: 'src/aritrace/administrator/configuration/language-settings/languages/language.module#LanguageModule', data: { title: 'Languages' }, },
        { path: 'captions', loadChildren: 'src/aritrace/administrator/configuration/language-settings/captions/caption.module#CaptionModule', data: { title: 'Captions' }, },

        { path: 'settings', loadChildren: 'src/aritrace/administrator/configuration/settings/setting.module#SettingModule', data: { title: 'Settings' }, },
        { path: 'glns', loadChildren: 'src/aritrace/administrator/gln/gln.module#GLNModule', data: { title: 'Global Location Number' }, },
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
export class AdministratorRoutingModule { } 
