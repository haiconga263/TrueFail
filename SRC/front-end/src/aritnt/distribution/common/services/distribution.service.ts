import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { Address, Contact } from '../services/geo.service';
import { Trip } from '../../trip/trip.service';

export class Distribution {
  id: number = 0;
  code: string = '';
  name: string = '';
  description: string = '';
  managerId: number = null;
  addressId: number = 0;
  contactId: number = 0;
  imageURL: string = '';
  isUsed: boolean = true;
  radius: number = 0;

  //mapping
  imageData: string = '';
  address: Address = new Address();
  contact: Contact = new Contact();

  trips: Trip[] = null;
}


@Injectable()
export class DistributionService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlDistributions: string = '/api/Distribution/gets';
  private urlDistributionsByOwner: string = '/api/Distribution/gets/by-owner';
  private urlDistribution: string = '/api/Distribution/get';
  private urlAddDistribution: string = '/api/Distribution/add';
  private urlUpdateDistribution: string = '/api/Distribution/update';
  private urlDeleteDistribution: string = '/api/Distribution/delete';

  getByOwners(): Observable<ResultModel<Distribution[]>> {
    return this.http.get<ResultModel<Distribution[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDistributionsByOwner}`, this.authenticSvc.getHttpHeader());
  }
  get(id: number): Observable<ResultModel<Distribution>> {
    return this.http.get<ResultModel<Distribution>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDistribution}?id=${id}`, this.authenticSvc.getHttpHeader());
  }
}
