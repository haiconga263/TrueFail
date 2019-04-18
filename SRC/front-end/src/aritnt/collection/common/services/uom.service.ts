import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { AuthenticService } from "src/core/Authentication/authentic.service";
import { Observable } from "rxjs";
import { ResultModel } from "src/core/models/http.model";
import { AppConsts } from "src/core/constant/AppConsts";

export class UoM {
  id: number = 0;
  code: string = '';
  name: string = '';
  description: string = '';
  isUsed: boolean = false;
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