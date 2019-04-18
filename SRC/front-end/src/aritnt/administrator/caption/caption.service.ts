import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';

export class Caption {
  id: number = 0;
  name: string = '';
  type: number = 0;
  defaultCaption: string = '';

  languages: CaptionLanguage[] = [];
}

export class CaptionLanguage {
  id: number = 0;
  languageId: number = 0;
  captionId: number = 0;
  caption: string = '';
}


@Injectable()
export class CaptionService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlCaptions: string = '/api/language/gets/caption';
  private urlCaption: string = '/api/language/get/caption';
  private urlUpdateCaption: string = '/api/language/update/caption';

  gets(): Observable<ResultModel<Caption[]>> {
    return this.http.get<ResultModel<Caption[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlCaptions}`, this.authenticSvc.getHttpHeader());
  }

  get(id: number): Observable<ResultModel<Caption>> {
    return this.http.get<ResultModel<Caption>>(`${AppConsts.remoteServiceBaseUrl}${this.urlCaption}?CaptionId=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(caption: Caption) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateCaption}`, { caption }, this.authenticSvc.getHttpHeader());
  }

  getTypes()
  {
    return[
      {
        id: 1,
        name: 'Message'
      },
      {
        id: 2,
        name: 'Label'
      },
      {
        id: 3,
        name: 'Column'
      }
    ];
  }
}
