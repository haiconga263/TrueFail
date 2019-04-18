import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService, Session } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';

export class UoM {
  id: number = 0;
  code: string = '';
  name: string = '';
  description: string = '';
}


@Injectable()
export class UoMService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlUoMs: string = '/api/UoM/gets';

  gets(): Observable<ResultModel<UoM[]>> {
    return this.http.get<ResultModel<UoM[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUoMs}`, this.authenticSvc.getHttpHeader());
  }
}
