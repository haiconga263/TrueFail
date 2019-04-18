import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';

export class Fulfillment {
  id: number = 0;
  code: string = '';
  name: string = '';
  description: string = '';
  managerId: number = null;
  addressId: number = 0;
  contactId: number = 0;
  imageURL: string = '';
  isUsed: boolean = true;
}


@Injectable()
export class FulfillmentService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlFulfillments: string = '/api/Fulfillment/gets/actived';

  gets(): Observable<ResultModel<Fulfillment[]>> {
    return this.http.get<ResultModel<Fulfillment[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlFulfillments}`, this.authenticSvc.getHttpHeader());
  }
}
