import { Injectable, Injector } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { BaseComponent } from 'src/core/basecommon/base.component';

export class Vehicle {
  id: number = 0;
  orgCode: string = '';
  code: string = '';
  name: string = '';
  imageURL: string = '';
  isUsed: boolean = true;
  weight: number = 0;
  vehicleWeight: number = 0;
  zoneCount: number = 0;
  startTime: string = '00:00';
  endTime: string = '00:00';
  startLunchTime: string = '00:00';
  endLunchTime: string = '00:00';
  speed: number = 0;
  temperatureType: string = '';
  capacity: number = 0;
  typeId: number = null;

  //mapping
  type: VehicleType = new VehicleType();
  imageData: string = '';
}

export class VehicleType {
  id: number = 0;
  name: string = '';
  isUsed: boolean = true
}


@Injectable()
export class VehicleService extends BaseComponent{
  constructor(
    injector: Injector,
    private http: HttpClient,
    private authenticSvc: AuthenticService) {
      super(injector);
     }

  private urlVehicles: string = '/api/Vehicle/gets';
  private urlVehicle: string = '/api/Vehicle/get';
  private urlAddVehicle: string = '/api/Vehicle/add';
  private urlUpdateVehicle: string = '/api/Vehicle/update';
  private urlDeleteVehicle: string = '/api/Vehicle/delete';

  private urlTypes: string = '/api/vehicle/gets/type';

  gets(): Observable<ResultModel<Vehicle[]>> {
    return this.http.get<ResultModel<Vehicle[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlVehicles}`, this.authenticSvc.getHttpHeader());
  }
  get(id: number): Observable<ResultModel<Vehicle>> {
    return this.http.get<ResultModel<Vehicle>>(`${AppConsts.remoteServiceBaseUrl}${this.urlVehicle}?id=${id}`, this.authenticSvc.getHttpHeader());
  }
  update(vehicle: Vehicle) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateVehicle}`, { vehicle }, this.authenticSvc.getHttpHeader());
  }
  add(vehicle: Vehicle) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddVehicle}`, { vehicle }, this.authenticSvc.getHttpHeader());
  }

  delete(vehicleId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteVehicle}`, { vehicleId }, this.authenticSvc.getHttpHeader());
  }

  getTypes(): Observable<ResultModel<VehicleType[]>> {
    return this.http.get<ResultModel<VehicleType[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlTypes}`, this.authenticSvc.getHttpHeader());
  }

  getTemperatureTypes(){
    return [{
      code: "A",
      name: this.lang.instant("Admin.Vehicle.TemperatureType.Normal")
    },
    {
      code: "C",
      name: this.lang.instant("Admin.Vehicle.TemperatureType.Cool")
    },
    {
      code: "F",
      name: this.lang.instant("Admin.Vehicle.TemperatureType.Frozen")
    }];
  }
}
