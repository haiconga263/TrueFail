import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CaptionDatagridComponent } from './caption-datagrid.component';

const routes: Routes = [{
  path: '',
  component: CaptionDatagridComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CaptionDatagridRoutingModule { }
