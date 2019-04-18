import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { Queue } from 'src/core/algorithms/datastructures/queue.structure';
import { appUrl } from '../../production/app-url';

export class Fertilizer {
  id: number = 0;
  code: string = '';
  name: string = '';
  description: string = '';
  category: number;
  isUsed: boolean = true;

  public constructor(init?: Partial<Fertilizer>) {
    Object.assign(this, init);
  }
}

@Injectable()
export class FertilizerService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlFertilizerCategories: string = `${appUrl.apiFertilizer}/gets`;
  private urlAllFertilizerCategories: string = `${appUrl.apiFertilizer}/gets/all`;
  private urlfertilizerById: string = `${appUrl.apiFertilizer}/get`;
  private urlAddfertilizer: string = `${appUrl.apiFertilizer}/insert`;
  private urlUpdatefertilizer: string = `${appUrl.apiFertilizer}/update`;
  private urlDeletefertilizer: string = `${appUrl.apiFertilizer}/delete`;

  gets(): Observable<ResultModel<Fertilizer[]>> {
    return this.http.get<ResultModel<Fertilizer[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlFertilizerCategories}`, this.authenticSvc.getHttpHeader());
  }

  getsAll(): Observable<ResultModel<Fertilizer[]>> {
    return this.http.get<ResultModel<Fertilizer[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAllFertilizerCategories}`, this.authenticSvc.getHttpHeader());
  }

  get(id: number): Observable<ResultModel<Fertilizer>> {
    return this.http.get<ResultModel<Fertilizer>>(`${AppConsts.remoteServiceBaseUrl}${this.urlfertilizerById}?id=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(fertilizer: Fertilizer): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdatefertilizer}`, { fertilizer }, this.authenticSvc.getHttpHeader());
  }

  add(fertilizer: Fertilizer): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddfertilizer}`, { fertilizer }, this.authenticSvc.getHttpHeader());
  }

  delete(fertilizerId: number): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeletefertilizer}`, { id: fertilizerId }, this.authenticSvc.getHttpHeader());
  }
}
