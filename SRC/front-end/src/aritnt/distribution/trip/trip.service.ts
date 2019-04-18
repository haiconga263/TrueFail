import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { Vehicle } from '../common/services/vehicle.service';
import { Employee } from '../common/services/employee.service';
import { Product } from '../common/services/product.service';
import { UoM } from '../common/services/uom.service';
import { RetailerLocation } from '../common/services/location.service';

export class Trip {
  id: number = 0;
  code: string = '';
  routerId: number = null;
  distributionId: number = null;
  statusId: number = null;
  deliveryManId: number = null;
  driverId: number = null;
  vehicleId: number = null;
  currentLongitude: number = null;
  currentLatitude: number = null;
  deliveryDate: Date;

  //mapping
  deliveryMan: Employee = new Employee();
  driver: Employee = new Employee();
  vehicle: Vehicle = new Vehicle();

  orders: Order[] = null;
}

export class TripStatus {
  id: number = 0;
  name: string = '';
  description: string = '';
  flagColor: string = '';
}

export class Order {
  id: number = 0;
  name: string = '';
  retailerId: number = 0;
  retailerBuyingCalanderId: number = 0;
  statusId: number = 0;
  buyingDate: Date = new Date(Date.now());
  billTo: number = 0;
  shipTo: number = 0;
  totalAmount: number = 0;
  tripId: number = 0;

  //mapping
  items: OrderItem[] = [];

  ship: RetailerLocation = new RetailerLocation();
}

export class OrderItem {
  id: number = 0;
  retailerOrderId: number = 0;
  productId: number = 0;
  statusId: number = 0;
  price: number = 0;
  orderedQuantity: number = 0;
  adapQuantity: number = 0;
  deliveriedQuantity: number = 0;
  uoMId: number = 0;

  //mapping
  product: Product = new Product();
  uom: UoM = new UoM();
}

@Injectable()
export class TripService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlTrip: string = '/api/trip/get';
  private urlTrips: string = '/api/trip/gets';
  private urlOrders: string = '/api/trip/gets/order';
  private urlAddTrip: string = '/api/trip/add';
  private urlUpdateTrip: string = '/api/trip/update';
  private urlDeleteTrip: string = '/api/trip/delete';

  private urlTripStatueses: string = '/api/trip/gets/status';
  private urlUpdateStatusTrip: string = '/api/trip/update/status';

  private urlUpdateOrderTrip: string = '/api/retailerorder/update/trip';

  get(tripId: number): Observable<ResultModel<Trip>> {
    return this.http.get<ResultModel<Trip>>(`${AppConsts.remoteServiceBaseUrl}${this.urlTrip}?tripId=${tripId}`, this.authenticSvc.getHttpHeader());
  }

  gets(distributionId: number): Observable<ResultModel<Trip[]>> {
    return this.http.get<ResultModel<Trip[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlTrips}?distributionId=${distributionId}`, this.authenticSvc.getHttpHeader());
  }

  getOrders(distributionId: number, tripId: number): Observable<ResultModel<Order[]>> {
    return this.http.get<ResultModel<Order[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlOrders}?distributionId=${distributionId}&tripId=${tripId}`, this.authenticSvc.getHttpHeader());
  }

  getStatuses() : Observable<ResultModel<TripStatus[]>> {
    return this.http.get<ResultModel<TripStatus[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlTripStatueses}`, this.authenticSvc.getHttpHeader());
  }

  update(trip: Trip) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateTrip}`, { trip }, this.authenticSvc.getHttpHeader());
  }

  moveOrder(orderId: number, tripId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateOrderTrip}`, { orderId, tripId }, this.authenticSvc.getHttpHeader());
  }

  changeStatus(tripId: number, statusId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateStatusTrip}`, {tripId, statusId}, this.authenticSvc.getHttpHeader());
  }

  add(trip: Trip) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddTrip}`, { trip }, this.authenticSvc.getHttpHeader());
  }

  delete(tripId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteTrip}`, { tripId }, this.authenticSvc.getHttpHeader());
  }
}
