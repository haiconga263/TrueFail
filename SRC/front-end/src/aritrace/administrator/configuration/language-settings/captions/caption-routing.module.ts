import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CaptionComponent } from './caption.component';

const routes: Routes = [{
  path: '',
  component: CaptionComponent,
  children: [
    { path: '', redirectTo: 'datagrid', pathMatch: 'full' },
    { path: 'datagrid', loadChildren: './datagrid/caption-datagrid.module#CaptionDatagridModule', data: { title: 'Captions' }, },
    { path: 'detail', loadChildren: './detail/caption-detail.module#CaptionDetailModule', data: { title: 'Caption Detail' }, },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CaptionRoutingModule { }
