import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageForbiddenComponent } from 'src/core/layout/page-forbidden/page-forbidden.component';
import { PageNotFoundComponent } from 'src/core/layout/page-not-found/page-not-found.component';
import { AuthenticationComponent } from 'src/core/Authentication/authentication.component';

const routes: Routes = [{
  path: '',
  data: { title: 'CMS' },
  children: [
    {
      path: '',
      redirectTo: 'dashboard',
      pathMatch: 'full'
    },
    {
      path: 'dashboard',
      loadChildren: 'src/aritnt/cms/dashboard/dashboard.module#DashboardModule', data: { title: 'Dashboard' },
    },
    {
      path: 'page',
      loadChildren: 'src/aritnt/cms/page/page.module#PageModule', data: { title: 'Page' },
    },
    {
      path: 'topic',
      loadChildren: 'src/aritnt/cms/blog/topic/topic.module#TopicModule', data: { title: 'Topic' },
    },
    {
      path: 'post',
      loadChildren: 'src/aritnt/cms/blog/post/post.module#PostModule', data: { title: 'Post' },
    },
    {
      path: 'faq',
      loadChildren: 'src/aritnt/cms/faq/faq.module#FaqModule', data: { title: 'Faq' },
    },
    {
      path: 'image',
      loadChildren: 'src/aritnt/cms/image/image.module#ImageModule', data: { title: 'Image' },
    },
    {
      path: 'contact',
      loadChildren: 'src/aritnt/cms/contact/contact.module#ContactModule', data: { title: 'Contact' },
    },
  ]
},
{ path: 'forbidden', component: PageForbiddenComponent, data: { title: '403 Forbidden' } },
{ path: 'authenticate', component: AuthenticationComponent, data: { title: 'authentication' }, },
{ path: 'not-found', component: PageNotFoundComponent, data: { title: '404 Not found' } },
{ path: '**', redirectTo: 'not-found' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: false, onSameUrlNavigation: 'reload' })],
  exports: [RouterModule]
})
export class CMSRoutingModule { }
