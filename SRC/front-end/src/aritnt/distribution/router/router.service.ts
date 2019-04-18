import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { Country, Province } from '../common/services/geo.service';

export class Router {
  id: number = 0;
  name: string = '';
  description: string = '';
  currentLongitude: number = 0;
  currentLatitude: number = 0;
  radius: number = 0;
  distributionId: number = 0;
  countryId: number = 0;
  provinceId: number = 0;
  isUsed: boolean = false;

  //mapping
  country: Country = new Country();
  province: Province = new Province();
}

@Injectable()
export class RouterService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlRouters: string = '/api/router/gets';
  private urlAddRouter: string = '/api/router/add';
  private urlUpdateRouter: string = '/api/router/update';
  private urlDeleteRouter: string = '/api/router/delete';

  gets(distributionId: number): Observable<ResultModel<Router[]>> {
    return this.http.get<ResultModel<Router[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlRouters}?distributionId=${distributionId}`, this.authenticSvc.getHttpHeader());
  }

  update(router: Router) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateRouter}`, { router }, this.authenticSvc.getHttpHeader());
  }
  add(router: Router) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddRouter}`, { router }, this.authenticSvc.getHttpHeader());
  }

  delete(routerId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteRouter}`, { routerId }, this.authenticSvc.getHttpHeader());
  }
}
