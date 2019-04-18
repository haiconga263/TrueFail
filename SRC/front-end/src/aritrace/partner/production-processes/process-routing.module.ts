import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProcessComponent } from './process.component';

const routes: Routes = [{
  path: '',
  component: ProcessComponent,
  children: [
    { path: '', redirectTo: 'datagrid', pathMatch: 'full' },
    { path: 'datagrid', loadChildren: 'src/aritrace/partner/production-processes/datagrid/process-datagrid.module#ProcessDatagridModule', data: { title: 'Process' }, },
    { path: 'detail', loadChildren: 'src/aritrace/partner/production-processes/detail/process-detail.module#ProcessDetailModule', data: { title: 'Process Detail' }, },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProcessRoutingModule { }
