import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductionDetailComponent } from './production-detail.component';

const routes: Routes = [{
  path: '',
  component: ProductionDetailComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductionDetailRoutingModule { }
