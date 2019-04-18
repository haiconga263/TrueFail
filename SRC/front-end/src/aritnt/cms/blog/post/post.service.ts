import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService, Session } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { Topic, TopicService } from '../topic/topic.service';

export class Post {
  id: number = 0;
  title: string = '';
  description: string = '';
  isDeleted : boolean = false;
  isUsed : boolean = false;
  postLanguages : PostLanguage[] = [];
  link : string = '';
  content: string = '';
  imageUrl: string = '';
  vote: number;
  tag: string = '';
  createdBy: number;
  createdDate: Date;
  modifiedBy: number;
  modifiedDate: Date;
  topics : Topic[] = [];
}

export class PostLanguage {
  id: number = 0;
  postId: number = 0;
  languageId: number = 0;
  title: string = '';
  content: string = '';
  description: string = '';
}
@Injectable()
export class PostService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlPostsOnly: string = '/api/PostHomepage/get/all-post';
  private urlPost: string = '/api/PostHomepage/get/post-detail';
  private urlAddPost: string = '/api/PostHomepage/add';
  private urlUpdatePost: string = '/api/PostHomepage/update';
  private urlDeletePost: string = '/api/PostHomepage/delete';
  private urlTopicsOnly: string = '/api/TopicHomepage/get/all-topic';

  getsOnly(): Observable<ResultModel<Post[]>> {
    return this.http.get<ResultModel<Post[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlPostsOnly}`, this.authenticSvc.getHttpHeader());
  }
  get(id: number): Observable<ResultModel<Post>> {
    return this.http.get<ResultModel<Post>>(`${AppConsts.remoteServiceBaseUrl}${this.urlPost}?postId=${id}`, this.authenticSvc.getHttpHeader());
  }
  update(post: Post) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdatePost}`, { post: post }, this.authenticSvc.getHttpHeader());
  }
  add(post: Post) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddPost}`, { post }, this.authenticSvc.getHttpHeader());
  }
  delete(postId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeletePost}`, { postId }, this.authenticSvc.getHttpHeader());
  }
}
