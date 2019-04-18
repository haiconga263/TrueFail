import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService, Session } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { saveAs } from 'file-saver/dist/FileSaver';

@Injectable()
export class AbivinService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  public urlAbivinVehicleTypeDownload = '/api/vehicle/types/download';
  public urlAbivinVehicleDownload = '/api/vehicle/download';
  public urlAbivinProductDownload = '/api/product/download';

  getVehicleType(): Observable<ResultModel<any[]>> {
    return this.http.get<ResultModel<any[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAbivinVehicleTypeDownload}`, this.authenticSvc.getHttpHeader());
  }
  
  downloadVehicleType(): void {
    this.http.get(`${AppConsts.remoteServiceBaseUrl}${this.urlAbivinVehicleTypeDownload}`,
      {
        headers: this.authenticSvc.getHttpHeader().headers,
        observe: "body",
        params: null,
        reportProgress: null,
        withCredentials: null,
        responseType: 'arraybuffer'
      }
    ).subscribe(response => this.downLoadFile("VehicleType.xlsx", response, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
  }

  downloadVehicle(): void {
    this.http.get(`${AppConsts.remoteServiceBaseUrl}${this.urlAbivinVehicleDownload}`,
      {
        headers: this.authenticSvc.getHttpHeader().headers,
        observe: "body",
        params: null,
        reportProgress: null,
        withCredentials: null,
        responseType: 'arraybuffer'
      }
    ).subscribe(response => this.downLoadFile("Vehicle.xlsx", response, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
  }

  downloadProduct(): void {
    this.http.get(`${AppConsts.remoteServiceBaseUrl}${this.urlAbivinProductDownload}`,
      {
        headers: this.authenticSvc.getHttpHeader().headers,
        observe: "body",
        params: null,
        reportProgress: null,
        withCredentials: null,
        responseType: 'arraybuffer'
      }
    ).subscribe(response => this.downLoadFile("Product.xlsx", response, "text/plain"));
  }

  downLoadFile(fileName: string, data: any, type: string) {
    //var blob = new Blob([data], { type: type });
    var blob = new Blob([data]);
    saveAs(blob, fileName);
    //saveAs(data, fileName);
  }
}
