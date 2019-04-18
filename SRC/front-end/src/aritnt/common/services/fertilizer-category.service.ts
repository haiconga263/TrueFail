import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { Queue } from 'src/core/algorithms/datastructures/queue.structure';
import { appUrl } from '../../production/app-url';

export class FertilizerCategory {
  id: number = 0;
  code: string = '';
  name: string = '';
  parentId: number = null;
  isUsed: boolean = true;

  public constructor(init?: Partial<FertilizerCategory>) {
    Object.assign(this, init);
  }
}

@Injectable()
export class FertilizerCategoryService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlFertilizerCategories: string = `${appUrl.apiFertilizerCategory}/gets`;
  private urlAllFertilizerCategories: string = `${appUrl.apiFertilizerCategory}/gets/all`;
  private urlfertilizerById: string = `${appUrl.apiFertilizerCategory}/get`;
  private urlAddfertilizer: string = `${appUrl.apiFertilizerCategory}/insert`;
  private urlUpdatefertilizer: string = `${appUrl.apiFertilizerCategory}/update`;
  private urlDeletefertilizer: string = `${appUrl.apiFertilizerCategory}/delete`;

  gets(): Observable<ResultModel<FertilizerCategory[]>> {
    return this.http.get<ResultModel<FertilizerCategory[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlFertilizerCategories}`, this.authenticSvc.getHttpHeader());
  }

  getsAll(): Observable<ResultModel<FertilizerCategory[]>> {
    return this.http.get<ResultModel<FertilizerCategory[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAllFertilizerCategories}`, this.authenticSvc.getHttpHeader());
  }

  get(id: number): Observable<ResultModel<FertilizerCategory>> {
    return this.http.get<ResultModel<FertilizerCategory>>(`${AppConsts.remoteServiceBaseUrl}${this.urlfertilizerById}?id=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(fertilizerCategory: FertilizerCategory): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdatefertilizer}`, { fertilizerCategory }, this.authenticSvc.getHttpHeader());
  }

  add(fertilizerCategory: FertilizerCategory): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddfertilizer}`, { fertilizerCategory }, this.authenticSvc.getHttpHeader());
  }

  delete(fertilizerCategoryId: number): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeletefertilizer}`, { id: fertilizerCategoryId }, this.authenticSvc.getHttpHeader());
  }

  removeChildren(id: number, fertilizerCategories: FertilizerCategory[]) {
    let rs: FertilizerCategory[] = FuncHelper.clone(fertilizerCategories);
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
