import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { Queue } from 'src/core/algorithms/datastructures/queue.structure';
import { appUrl } from '../../production/app-url';

export class Plot {
  id: number = 0;
  code: string = '';
  description: string = '';
  area: number;
  longitude: number;
  latitude: number;
  isGlassHouse: boolean;
  farmerId: number;

  public constructor(init?: Partial<Plot>) {
    Object.assign(this, init);
  }
}

@Injectable()
export class PlotService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlPlotCategories: string = `${appUrl.apiPlot}/gets`;
  private urlAllPlotCategories: string = `${appUrl.apiPlot}/gets/all`;
  private urlplotById: string = `${appUrl.apiPlot}/get`;
  private urlAddplot: string = `${appUrl.apiPlot}/insert`;
  private urlUpdateplot: string = `${appUrl.apiPlot}/update`;
  private urlDeleteplot: string = `${appUrl.apiPlot}/delete`;

  gets(): Observable<ResultModel<Plot[]>> {
    return this.http.get<ResultModel<Plot[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlPlotCategories}`, this.authenticSvc.getHttpHeader());
  }

  get(id: number): Observable<ResultModel<Plot>> {
    return this.http.get<ResultModel<Plot>>(`${AppConsts.remoteServiceBaseUrl}${this.urlplotById}?id=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(plot: Plot): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateplot}`, { plot }, this.authenticSvc.getHttpHeader());
  }

  add(plot: Plot): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddplot}`, { plot }, this.authenticSvc.getHttpHeader());
  }

  delete(plotId: number): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteplot}`, { id: plotId }, this.authenticSvc.getHttpHeader());
  }
}
