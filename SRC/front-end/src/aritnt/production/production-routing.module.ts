import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageForbiddenComponent } from 'src/core/layout/page-forbidden/page-forbidden.component';
import { PageNotFoundComponent } from 'src/core/layout/page-not-found/page-not-found.component';
import { AuthenticationComponent } from 'src/core/Authentication/authentication.component';

const routes: Routes = [{
  path: '',
  data: { title: 'Production' },
  children: [
    {
      path: '',
      redirectTo: 'dashboard',
      pathMatch: 'full'
    },
    {
      path: 'dashboard',
      loadChildren: 'src/aritnt/production/dashboard/dashboard.module#DashboardModule', data: { title: 'Dashboard' },
    },
    { path: 'cultivation', loadChildren: 'src/aritnt/production/cultivation/cultivation.module#CultivationModule', data: { title: 'Cultivation management' }, },
    { path: 'seed', loadChildren: 'src/aritnt/production/seed/seed.module#SeedModule', data: { title: 'Seed management' }, },
    { path: 'method', loadChildren: 'src/aritnt/production/method/method.module#MethodModule', data: { title: 'Cultivation Method management' }, },
    { path: 'plot', loadChildren: 'src/aritnt/production/plot/plot.module#PlotModule', data: { title: 'Plot management' }, },
    { path: 'fertilizer', loadChildren: 'src/aritnt/production/fertilizer/fertilizer.module#FertilizerModule', data: { title: 'Fertilizer management' }, },
    { path: 'fertilizer/category', loadChildren: 'src/aritnt/production/fertilizer-category/fertilizer-category.module#FertilizerCategoryModule', data: { title: 'Fertilizer Category management' }, },
    { path: 'pesticide', loadChildren: 'src/aritnt/production/pesticide/pesticide.module#PesticideModule', data: { title: 'Pesticide management' }, },
    { path: 'pesticide/category', loadChildren: 'src/aritnt/production/pesticide-category/pesticide-category.module#PesticideCategoryModule', data: { title: 'Pesticide Category management' }, },
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
export class ProductionRoutingModule { }
