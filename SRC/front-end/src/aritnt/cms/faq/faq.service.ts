import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService, Session } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';

export class Faq {
  id: number = 0;
  question: string = '';
  answer: string = '';
  isDeleted : boolean = false;
  faqLanguages : FaqLanguage[] = [];
  createdBy: number;
  createdDate: Date;
  modifiedBy: number;
  modifiedDate: Date;
  isUsed : boolean = false;
}

export class FaqLanguage {
  id: number = 0;
  faqId: number = 0;
  languageId: number = 0;
  question: string = '';
  answer: string = '';
}
@Injectable()
export class FaqService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlFaqs: string = '/api/FaqHomepage/gets';
  private urlFaqsOnly: string = '/api/FaqHomepage/get/all-faq';
  private urlFaqsOnlyWithLang: string = '/api/FaqHomepage/gets/only/withlang';
  private urlFaqsForOrder: string = '/api/FaqHomepage/gets/fororder';
  private urlFaqFull: string = '/api/FaqHomepage/get';
  private urlFaqsFull: string = '/api/FaqHomepage/gets/full';


  private urlFaq: string = '/api/FaqHomepage/get/faq-detail';
  private urlAddFaq: string = '/api/FaqHomepage/add';
  private urlUpdateFaq: string = '/api/FaqHomepage/update';
  private urlDeleteFaq: string = '/api/FaqHomepage/delete';

  getsOnly(): Observable<ResultModel<Faq[]>> {
    return this.http.get<ResultModel<Faq[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlFaqsOnly}`, this.authenticSvc.getHttpHeader());
  }

  get(id: number): Observable<ResultModel<Faq>> {
    return this.http.get<ResultModel<Faq>>(`${AppConsts.remoteServiceBaseUrl}${this.urlFaq}?faqId=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(faq: Faq) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateFaq}`, { faq: faq }, this.authenticSvc.getHttpHeader());
  }
  add(faq: Faq) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddFaq}`, { faq }, this.authenticSvc.getHttpHeader());
  }

  delete(faqId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteFaq}`, { faqId }, this.authenticSvc.getHttpHeader());
  }
}
