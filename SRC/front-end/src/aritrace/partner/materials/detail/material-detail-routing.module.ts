import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MaterialDetailComponent } from './material-detail.component';

const routes: Routes = [{
  path: '',
  component: MaterialDetailComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MaterialDetailRoutingModule { }
