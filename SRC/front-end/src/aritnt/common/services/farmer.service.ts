import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { Address, Contact } from 'src/aritnt/common/services/geo.service';

export class Farmer {
  id: number = 0;
  code: string = '';
  name: string = '';
  userId: number = 0;
  addressId: number = 0;
  contactId: number = 0;
  imageURL: string = '';
  isUsed: boolean = true;

  isCompany: boolean = false;
  taxCode: string = '';

  //mapping
  imageData: string = '';
  address: Address = new Address();
  contact: Contact = new Contact();
}


@Injectable()
export class FarmerService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlFarmers: string = '/api/Farmer/gets';
  private urlFarmer: string = '/api/Farmer/get';
  private urlAddFarmer: string = '/api/Farmer/add';
  private urlUpdateFarmer: string = '/api/Farmer/update';
  private urlDeleteFarmer: string = '/api/Farmer/delete';

  gets(): Observable<ResultModel<Farmer[]>> {
    return this.http.get<ResultModel<Farmer[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlFarmers}`, this.authenticSvc.getHttpHeader());
  }
  get(id: number): Observable<ResultModel<Farmer>> {
    return this.http.get<ResultModel<Farmer>>(`${AppConsts.remoteServiceBaseUrl}${this.urlFarmer}?farmerId=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(farmer: Farmer): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateFarmer}`, { farmer }, this.authenticSvc.getHttpHeader());
  }
  add(farmer: Farmer): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddFarmer}`, { farmer }, this.authenticSvc.getHttpHeader());
  }

  delete(farmerId: number): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteFarmer}`, { farmerId }, this.authenticSvc.getHttpHeader());
  }

  getGenders() {
    return [
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
