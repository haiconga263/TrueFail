import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { Address, Contact } from '../geographical/geo.service';

export class Collection {
  id: number = 0;
  code: string = '';
  name: string = '';
  description: string = '';
  managerId: number = null;
  addressId: number = 0;
  contactId: number = 0;
  imageURL: string = '';
  isUsed: boolean = true;

  //mapping
  imageData: string = '';
  address: Address = new Address();
  contact: Contact = new Contact();
}


@Injectable()
export class CollectionService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlCollections: string = '/api/Collection/gets';
  private urlCollection: string = '/api/Collection/get';
  private urlAddCollection: string = '/api/Collection/add';
  private urlUpdateCollection: string = '/api/Collection/update';
  private urlDeleteCollection: string = '/api/Collection/delete';

  gets(): Observable<ResultModel<Collection[]>> {
    return this.http.get<ResultModel<Collection[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlCollections}`, this.authenticSvc.getHttpHeader());
  }
  get(id: number): Observable<ResultModel<Collection>> {
    return this.http.get<ResultModel<Collection>>(`${AppConsts.remoteServiceBaseUrl}${this.urlCollection}?id=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(collection: Collection) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateCollection}`, { collection }, this.authenticSvc.getHttpHeader());
  }
  add(collection: Collection) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddCollection}`, { collection }, this.authenticSvc.getHttpHeader());
  }

  delete(collectionId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteCollection}`, { collectionId }, this.authenticSvc.getHttpHeader());
  }

  getGenders()
  {
    return[
      {
        id: 'M',
        name: 'Male'
      },
      {
        id: 'F',
        name: 'Female'
      },
      {
        id: 'O',
        name: 'Other'
      }
    ];
  }
}
