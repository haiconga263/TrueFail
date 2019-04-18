import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService, Session } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';

export class Image {
  id: string = '';
  parentId: string = '';
  name: string = '';
  isDirectory : boolean = false;
  hasItems : boolean = false;
  size: number;
  createdDate: Date;
  modifiedDate: Date;
}

export class ImageLanguage {
  id: number = 0;
  imageId: number = 0;
  languageId: number = 0;
  question: string = '';
  answer: string = '';
}
@Injectable()
export class ImageService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlImages: string = '/api/ImageHomepage/gets';
  private urlImagesOnly: string = '/api/ImageHomepage/get/all-directories';
  private urlImagesOnlyWithLang: string = '/api/ImageHomepage/gets/only/withlang';
  private urlImagesForOrder: string = '/api/ImageHomepage/gets/fororder';
  private urlImageFull: string = '/api/ImageHomepage/get';
  private urlImagesFull: string = '/api/ImageHomepage/gets/full';


  private urlImage: string = '/api/ImageHomepage/get/image-detail';
  private urlAddImage: string = '/api/ImageHomepage/add';
  private urlUpdateImage: string = '/api/ImageHomepage/update';
  private urlDeleteImage: string = '/api/ImageHomepage/delete';
  private directories: string = '/api/CommonHomepage/get/directories';

  getsOnly(fileName : string): Observable<ResultModel<any>> {
    return this.http.get<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.directories}?parentIds=${fileName}`, this.authenticSvc.getHttpHeader());
  }

  get(id: number): Observable<ResultModel<Image>> {
    return this.http.get<ResultModel<Image>>(`${AppConsts.remoteServiceBaseUrl}${this.urlImage}?imageId=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(image: Image) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateImage}`, { image: image }, this.authenticSvc.getHttpHeader());
  }
  add(image: Image) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddImage}`, { image }, this.authenticSvc.getHttpHeader());
  }

  delete(imageId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteImage}`, { imageId }, this.authenticSvc.getHttpHeader());
  }
}
