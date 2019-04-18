import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GLNDetailComponent } from './gln-detail.component';

const routes: Routes = [{
  path: '',
  component: GLNDetailComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GLNDetailRoutingModule { }
