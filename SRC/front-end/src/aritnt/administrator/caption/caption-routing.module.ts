import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CaptionComponent } from './master/caption.component';
import { CaptionDetailComponent } from './detail/caption-detail.component';

const routes: Routes = [{
  path: '',
  redirectTo: 'list',
  pathMatch: 'full',
},
{
  path: '',
  children: [
      { path: 'list', component: CaptionComponent, data: { title: 'Caption' } },
      { path: 'detail', component: CaptionDetailComponent, data: { title: 'Caption Detail' } }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CaptionRoutingModule { }
