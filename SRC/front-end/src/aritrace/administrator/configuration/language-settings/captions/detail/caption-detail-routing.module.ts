import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CaptionDetailComponent } from './caption-detail.component';

const routes: Routes = [{
  path: '',
  component: CaptionDetailComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CaptionDetailRoutingModule { }
