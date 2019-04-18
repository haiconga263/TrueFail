import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CompanyDatagridComponent } from './company-datagrid.component';

const routes: Routes = [{
  path: '',
  component: CompanyDatagridComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CompanyDatagridRoutingModule { }
