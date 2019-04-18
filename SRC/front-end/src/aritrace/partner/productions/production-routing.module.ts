import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductionComponent } from './production.component';

const routes: Routes = [{
  path: '',
  component: ProductionComponent,
  children: [
    { path: '', redirectTo: 'datagrid', pathMatch: 'full' },
    { path: 'datagrid', loadChildren: 'src/aritrace/partner/productions/datagrid/production-datagrid.module#ProductionDatagridModule', data: { title: 'Production' }, },
    { path: 'detail', loadChildren: 'src/aritrace/partner/productions/detail/production-detail.module#ProductionDetailModule', data: { title: 'Production Detail' }, },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductionRoutingModule { }
