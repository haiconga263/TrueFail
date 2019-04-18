import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService, Session } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';

export class Page {
  id: number = 0;
  title: string = '';
  isDeleted : boolean = false;
  isUsed : boolean = false;
  isPushing : boolean = false;
  isFooter : boolean = false;
  pageLanguages : PageLanguage[] = [];
  content: string = '';
  createdBy: number;
  createdDate: Date;
  modifiedBy: number;
  modifiedDate: Date;
}

export class PageLanguage {
  id: number = 0;
  pageId: number = 0;
  languageId: number = 0;
  title: string = '';
  content: string = '';
}
@Injectable()
export class PageService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlPagesOnly: string = '/api/PageHomepage/get/all-page';
  private urlPage: string = '/api/PageHomepage/get/page-detail';
  private urlAddPage: string = '/api/PageHomepage/add';
  private urlUpdatePage: string = '/api/PageHomepage/update';
  private urlDeletePage: string = '/api/PageHomepage/delete';
  private urlTopicsOnly: string = '/api/TopicHomepage/get/all-topic';

  getsOnly(): Observable<ResultModel<Page[]>> {
    return this.http.get<ResultModel<Page[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlPagesOnly}`, this.authenticSvc.getHttpHeader());
  }
  get(id: number): Observable<ResultModel<Page>> {
    return this.http.get<ResultModel<Page>>(`${AppConsts.remoteServiceBaseUrl}${this.urlPage}?pageId=${id}`, this.authenticSvc.getHttpHeader());
  }
  update(page: Page) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdatePage}`, { page: page }, this.authenticSvc.getHttpHeader());
  }
  add(page: Page) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddPage}`, { page }, this.authenticSvc.getHttpHeader());
  }
  delete(pageId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeletePage}`, { pageId }, this.authenticSvc.getHttpHeader());
  }
}
