import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageDetailComponent } from './detail/page-detail.component';
import { PageComponent } from './master/page.component';

const routes: Routes = [{
  path: '',
  redirectTo: 'list',
  pathMatch: 'full',
},
{
  path: '',
  children: [
      { path: 'list', component: PageComponent },
      { path: 'detail', component: PageDetailComponent }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PageRoutingModule { }
