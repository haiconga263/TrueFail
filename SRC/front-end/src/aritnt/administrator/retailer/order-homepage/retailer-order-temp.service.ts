import { ResultModel } from "src/core/models/http.model";
import { AuthenticService } from "src/core/Authentication/authentic.service";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AppConsts } from "src/core/constant/AppConsts";
import { HttpClient } from '@angular/common/http';

export class RetailerOrderTemp {
    id: number = 0;
    name: string = '';
    companyName: string = '';
    email: string = '';
    phoneNumber: number = 0;
    buyDate: Date  = new Date(Date.now());
    address: string = '';
    note: string = '';
    shippingAddress: string = '';
    shippingDate: Date = new Date(Date.now());
  
    //mapping
    //items: RetailerOrderItem[] = [];
}
@Injectable()
export class RetailerOrderTempService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlGetAll: string = '/api/RetailerOrderHomepage/gets/retailer-ordertemp';  

  private urlGetDetail: string = '/api/RetailerOrderHomepage/get/retailer-ordertemp-byid"';

  gets(retailerId: number = 0): Observable<ResultModel<RetailerOrderTemp[]>> {
    let param = '';
    return this.http.get<ResultModel<RetailerOrderTemp[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlGetAll}` + param, this.authenticSvc.getHttpHeader());
  }

  get(id: number): Observable<ResultModel<RetailerOrderTemp>> {
    return this.http.get<ResultModel<RetailerOrderTemp>>(`${AppConsts.remoteServiceBaseUrl}${this.urlGetDetail}?orderId=${id}`, this.authenticSvc.getHttpHeader());
  }

  
}