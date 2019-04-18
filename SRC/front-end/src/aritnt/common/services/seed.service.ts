import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { Queue } from 'src/core/algorithms/datastructures/queue.structure';
import { appUrl } from '../../production/app-url';

export class Seed {
  id: number = 0;
  code: string = '';
  name: string = '';
  description: string = '';
  productId: number;
  isUsed: boolean = true;

  public constructor(init?: Partial<Seed>) {
    Object.assign(this, init);
  }
}

@Injectable()
export class SeedService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlSeedCategories: string = `${appUrl.apiSeed}/gets`;
  private urlAllSeedCategories: string = `${appUrl.apiSeed}/gets/all`;
  private urlseedById: string = `${appUrl.apiSeed}/get`;
  private urlAddseed: string = `${appUrl.apiSeed}/insert`;
  private urlUpdateseed: string = `${appUrl.apiSeed}/update`;
  private urlDeleteseed: string = `${appUrl.apiSeed}/delete`;

  gets(): Observable<ResultModel<Seed[]>> {
    return this.http.get<ResultModel<Seed[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlSeedCategories}`, this.authenticSvc.getHttpHeader());
  }

  getsAll(): Observable<ResultModel<Seed[]>> {
    return this.http.get<ResultModel<Seed[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAllSeedCategories}`, this.authenticSvc.getHttpHeader());
  }

  get(id: number): Observable<ResultModel<Seed>> {
    return this.http.get<ResultModel<Seed>>(`${AppConsts.remoteServiceBaseUrl}${this.urlseedById}?id=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(seed: Seed): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateseed}`, { seed }, this.authenticSvc.getHttpHeader());
  }

  add(seed: Seed): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddseed}`, { seed }, this.authenticSvc.getHttpHeader());
  }

  delete(seedId: number): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteseed}`, { id: seedId }, this.authenticSvc.getHttpHeader());
  }
}
