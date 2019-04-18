import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CultureFieldDetailComponent } from './culture-field-detail.component';

const routes: Routes = [{
  path: '',
  component: CultureFieldDetailComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CultureFieldDetailRoutingModule { }
