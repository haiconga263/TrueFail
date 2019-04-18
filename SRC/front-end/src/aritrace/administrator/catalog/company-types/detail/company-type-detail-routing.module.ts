import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CompanyTypeDetailComponent } from './company-type-detail.component';

const routes: Routes = [{
  path: '',
  component: CompanyTypeDetailComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CompanyTypeDetailRoutingModule { }
