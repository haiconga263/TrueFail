import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService, Session } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';

export class Topic {
  id: number = 0;
  topicName: string = '';
  topicUrl: string = '';
  sortTopic: number = 1;
  isUsed: boolean = false;
  isDeleted : boolean = false;
  topicLanguages : TopicLanguage[] = [];
  createdBy: number;
  createdDate: Date;
  modifiedBy: number;
  modifiedDate: Date;
  isFooter: boolean = false;
}

export class TopicLanguage {
  id: number = 0;
  topicId: number = 0;
  languageId: number = 0;
  topicUrl: string = '';
  topicName: string = '';
}
@Injectable()
export class TopicService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlTopics: string = '/api/TopicHomepage/gets';
  private urlTopicsOnlyWithLang: string = '/api/TopicHomepage/gets/only/withlang';
  private urlTopicsForOrder: string = '/api/TopicHomepage/gets/fororder';
  private urlTopicFull: string = '/api/TopicHomepage/get';
  private urlTopicsFull: string = '/api/TopicHomepage/gets/full';

  private urlTopicsOnly: string = '/api/TopicHomepage/get/all-topic';
  private urlTopic: string = '/api/TopicHomepage/get/topic-detail';
  private urlAddTopic: string = '/api/TopicHomepage/add';
  private urlUpdateTopic: string = '/api/TopicHomepage/update';
  private urlDeleteTopic: string = '/api/TopicHomepage/delete';

  getsOnly(): Observable<ResultModel<Topic[]>> {
    return this.http.get<ResultModel<Topic[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlTopicsOnly}`, this.authenticSvc.getHttpHeader());
  }

  get(id: number): Observable<ResultModel<Topic>> {
    return this.http.get<ResultModel<Topic>>(`${AppConsts.remoteServiceBaseUrl}${this.urlTopic}?topicId=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(topic: Topic) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateTopic}`, { topic: topic }, this.authenticSvc.getHttpHeader());
  }
  add(topic: Topic) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddTopic}`, { topic }, this.authenticSvc.getHttpHeader());
  }

  delete(topicId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteTopic}`, { topicId }, this.authenticSvc.getHttpHeader());
  }
}
