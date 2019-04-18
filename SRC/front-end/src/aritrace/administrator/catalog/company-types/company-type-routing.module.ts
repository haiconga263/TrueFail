import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CompanyTypeComponent } from './company-type.component';

const routes: Routes = [{
  path: '',
  component: CompanyTypeComponent,
  children: [
    { path: '', redirectTo: 'datagrid', pathMatch: 'full' },
    { path: 'datagrid', loadChildren: 'src/aritrace/administrator/catalog/company-types/datagrid/company-type-datagrid.module#CompanyTypeDatagridModule', data: { title: 'Company Types' }, },
    { path: 'detail', loadChildren: 'src/aritrace/administrator/catalog/company-types/detail/company-type-detail.module#CompanyTypeDetailModule', data: { title: 'CompanyType Detail' }, },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CompanyTypeRoutingModule { }
