import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CompanyTypeDatagridComponent } from './company-type-datagrid.component';

const routes: Routes = [{
  path: '',
  component: CompanyTypeDatagridComponent,
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CompanyTypeDatagridRoutingModule { }
