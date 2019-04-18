import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CaptionEmbedComponent } from './caption-embed.component';

const routes: Routes = [{
  path: '',
  component: CaptionEmbedComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CaptionEmbedRoutingModule { }
