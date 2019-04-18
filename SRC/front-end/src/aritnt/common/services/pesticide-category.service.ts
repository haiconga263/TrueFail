import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { Queue } from 'src/core/algorithms/datastructures/queue.structure';
import { appUrl } from '../../production/app-url';

export class PesticideCategory {
  id: number = 0;
  code: string = '';
  name: string = '';
  parentId: number = null;
  isUsed: boolean = true;

  public constructor(init?: Partial<PesticideCategory>) {
    Object.assign(this, init);
  }
}

@Injectable()
export class PesticideCategoryService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlPesticideCategories: string = `${appUrl.apiPesticideCategory}/gets`;
  private urlAllPesticideCategories: string = `${appUrl.apiPesticideCategory}/gets/all`;
  private urlpesticideById: string = `${appUrl.apiPesticideCategory}/get`;
  private urlAddpesticide: string = `${appUrl.apiPesticideCategory}/insert`;
  private urlUpdatepesticide: string = `${appUrl.apiPesticideCategory}/update`;
  private urlDeletepesticide: string = `${appUrl.apiPesticideCategory}/delete`;

  gets(): Observable<ResultModel<PesticideCategory[]>> {
    return this.http.get<ResultModel<PesticideCategory[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlPesticideCategories}`, this.authenticSvc.getHttpHeader());
  }

  getsAll(): Observable<ResultModel<PesticideCategory[]>> {
    return this.http.get<ResultModel<PesticideCategory[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAllPesticideCategories}`, this.authenticSvc.getHttpHeader());
  }

  get(id: number): Observable<ResultModel<PesticideCategory>> {
    return this.http.get<ResultModel<PesticideCategory>>(`${AppConsts.remoteServiceBaseUrl}${this.urlpesticideById}?id=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(pesticideCategory: PesticideCategory): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdatepesticide}`, { pesticideCategory }, this.authenticSvc.getHttpHeader());
  }

  add(pesticideCategory: PesticideCategory): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddpesticide}`, { pesticideCategory }, this.authenticSvc.getHttpHeader());
  }

  delete(pesticideCategoryId: number): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeletepesticide}`, { id: pesticideCategoryId }, this.authenticSvc.getHttpHeader());
  }

  removeChildren(id: number, pesticideCategories: PesticideCategory[]) {
    let rs: PesticideCategory[] = FuncHelper.clone(pesticideCategories);
    let q: Queue<number> = new Queue<number>();

    q.push(id);
    while (true) {
      let cateId = q.pop();
      if (cateId == null || cateId == 0)
        break;
      for (let i = 0; i < rs.length; i++) {
        if (rs[i].parentId == cateId || rs[i].id == cateId) {
          q.push(rs[i].id);
          rs.splice(i, 1);
          i--;
        }
      }
    }
    return rs;
  }
}
