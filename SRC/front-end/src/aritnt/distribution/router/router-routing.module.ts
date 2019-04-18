import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RouterComponent } from './router.component';

const routes: Routes = [{
  path: '',
  component: RouterComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RouterRoutingModule { }
