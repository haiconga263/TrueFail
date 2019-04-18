import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { Queue } from 'src/core/algorithms/datastructures/queue.structure';
import { appUrl } from '../../production/app-url';

export enum CultivationStatus {
  NotStarted = 0,
  InProgress = 1,
  Completed = 2
}

export const CultivationStatusArray: any[] = [{ id: 0, name: 'NotStarted' }, { id: 1, name: 'InProgress' }, { id: 2, name: 'Completed' }];

export class Cultivation {
  id: number = 0;
  code: string = '';
  name: string = '';
  description: string = '';
  seedId: number;
  methodId: number;
  plotId: number;
  seedingDate: Date;
  status: CultivationStatus;
  expectedHarvestDate: Date;
  longitude: number;
  latitude: number;

  public constructor(init?: Partial<Cultivation>) {
    Object.assign(this, init);
  }
}

@Injectable()
export class CultivationService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlCultivationCategories: string = `${appUrl.apiCultivation}/gets`;
  private urlAllCultivationCategories: string = `${appUrl.apiCultivation}/gets/all`;
  private urlcultivationById: string = `${appUrl.apiCultivation}/get`;
  private urlAddcultivation: string = `${appUrl.apiCultivation}/insert`;
  private urlUpdatecultivation: string = `${appUrl.apiCultivation}/update`;
  private urlDeletecultivation: string = `${appUrl.apiCultivation}/delete`;

  gets(): Observable<ResultModel<Cultivation[]>> {
    return this.http.get<ResultModel<Cultivation[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlCultivationCategories}`, this.authenticSvc.getHttpHeader());
  }

  getsAll(): Observable<ResultModel<Cultivation[]>> {
    return this.http.get<ResultModel<Cultivation[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAllCultivationCategories}`, this.authenticSvc.getHttpHeader());
  }

  get(id: number): Observable<ResultModel<Cultivation>> {
    return this.http.get<ResultModel<Cultivation>>(`${AppConsts.remoteServiceBaseUrl}${this.urlcultivationById}?id=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(cultivation: Cultivation): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdatecultivation}`, { cultivation }, this.authenticSvc.getHttpHeader());
  }

  add(cultivation: Cultivation): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddcultivation}`, { cultivation }, this.authenticSvc.getHttpHeader());
  }

  delete(cultivationId: number): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeletecultivation}`, { id: cultivationId }, this.authenticSvc.getHttpHeader());
  }
}
