import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CollectionComponent } from './master/collection.component';
import { CollectionDetailComponent } from './detail/collection-detail.component';

const routes: Routes = [{
  path: '',
  redirectTo: 'list',
  pathMatch: 'full',
},
{
  path: '',
  children: [
      { path: 'list', component: CollectionComponent },
      { path: 'detail', component: CollectionDetailComponent }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CollectionRoutingModule { }
