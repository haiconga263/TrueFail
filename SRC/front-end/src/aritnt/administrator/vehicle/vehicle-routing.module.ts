import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { VehicleComponent } from './master/vehicle.component';
import { VehicleDetailComponent } from './detail/vehicle-detail.component';

const routes: Routes = [{
  path: '',
  redirectTo: 'list',
  pathMatch: 'full',
},
{
  path: '',
  children: [
      { path: 'list', component: VehicleComponent },
      { path: 'detail', component: VehicleDetailComponent }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VehicleRoutingModule { }
