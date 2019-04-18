import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CompanyComponent } from './company.component';

const routes: Routes = [{
  path: '',
  component: CompanyComponent,
  children: [
    { path: '', redirectTo: 'datagrid', pathMatch: 'full' },
    { path: 'datagrid', loadChildren: 'src/aritrace/administrator/companies/datagrid/company-datagrid.module#CompanyDatagridModule', data: { title: 'Company' }, },
    { path: 'detail', loadChildren: 'src/aritrace/administrator/companies/detail/company-detail.module#CompanyDetailModule', data: { title: 'Company Detail' }, },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CompanyRoutingModule { }
