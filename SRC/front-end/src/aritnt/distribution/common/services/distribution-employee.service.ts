import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { Address, Contact } from './geo.service';

export class DistributionEmployee {
  id: number = 0;
  distributionId: number = null;
  employeeId: number = 0;
}


@Injectable()
export class DistributionEmployeeService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlDistributions: string = '/api/DistributionEmployee/gets';
  private urlAddDistribution: string = '/api/DistributionEmployee/add';
  private urlUpdateDistribution: string = '/api/DistributionEmployee/update';
  private urlDeleteDistribution: string = '/api/DistributionEmployee/delete';

  gets(distributionId: number): Observable<ResultModel<DistributionEmployee[]>> {
    return this.http.get<ResultModel<DistributionEmployee[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDistributions}?distributionId=${distributionId}`, this.authenticSvc.getHttpHeader());
  }

  update(employee: DistributionEmployee) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateDistribution}`, { employee }, this.authenticSvc.getHttpHeader());
  }
  add(employee: DistributionEmployee) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddDistribution}`, { employee }, this.authenticSvc.getHttpHeader());
  }

  delete(employeeId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteDistribution}`, { employeeId }, this.authenticSvc.getHttpHeader());
  }
}
