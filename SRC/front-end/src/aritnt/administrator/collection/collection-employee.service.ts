import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';

export class CollectionEmployee {
  id: number = 0;
  collectionId: number = null;
  employeeId: number = 0;
}


@Injectable()
export class CollectionEmployeeService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlCollections: string = '/api/CollectionEmployee/gets';
  private urlAddCollection: string = '/api/CollectionEmployee/add';
  private urlUpdateCollection: string = '/api/CollectionEmployee/update';
  private urlDeleteCollection: string = '/api/CollectionEmployee/delete';

  gets(collectionId: number): Observable<ResultModel<CollectionEmployee[]>> {
    return this.http.get<ResultModel<CollectionEmployee[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlCollections}?collectionId=${collectionId}`, this.authenticSvc.getHttpHeader());
  }

  update(employee: CollectionEmployee) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateCollection}`, { employee }, this.authenticSvc.getHttpHeader());
  }
  add(employee: CollectionEmployee) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddCollection}`, { employee }, this.authenticSvc.getHttpHeader());
  }

  delete(employeeId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteCollection}`, { employeeId }, this.authenticSvc.getHttpHeader());
  }
}
