import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MaterialHistoryComponent } from './material-history.component';

const routes: Routes = [{
  path: '',
  component: MaterialHistoryComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MaterialHistoryRoutingModule { }
