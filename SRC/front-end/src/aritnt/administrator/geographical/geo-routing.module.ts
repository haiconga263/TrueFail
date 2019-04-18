import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CountryComponent } from './country/country.component';
import { RegionComponent } from './region/region.component';
import { ProvinceComponent } from './province/province.component';
import { DistrictComponent } from './district/district.component';
import { WardComponent } from './ward/ward.component';

const routes: Routes = [{
  path: '',
  redirectTo: 'country',
  pathMatch: 'full',
},
{
  path: '',
  children: [
      { path: 'country', component: CountryComponent, data: { title: 'Country' } },
      { path: 'region', component: RegionComponent, data: { title: 'Region' } },
      { path: 'province', component: ProvinceComponent, data: { title: 'Province' } },
      { path: 'district', component: DistrictComponent, data: { title: 'District' } },
      { path: 'ward', component: WardComponent, data: { title: 'Ward' } }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GeoRoutingModule { }
