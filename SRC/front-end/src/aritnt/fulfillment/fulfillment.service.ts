import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { Product } from 'src/aritnt/administrator/product/product.service';
import { UoM } from 'src/aritnt/administrator/common/services/uom.service';
import { TempFarmerPlanningItem } from 'src/aritnt/administrator/farmer/farmer-planning.service';
import { Address, Contact } from 'src/aritnt/administrator/geographical/geo.service';



export class FulfillmentCollection {
  id: number = 0;
  code: string = '';
  collectionId: number = 0;
  fulfillmentId: number = 0;
  deliveryDate: Date = new Date(Date.now());
  statusId: number = 0;
  statusFulCols: Status [] = [];
  fulfillment: Fulfillment = new Fulfillment();
  collection: Collection = new Collection();
  items: FulfillmentCollectionItem[] = [];
}

export class FulfillmentCollectionItem {
  id: number = 0;

  traceCode: string = '';
  productId: number = 0;
  uoMId: number = 0;
  shippedQuantity: number = 0;
  deliveriedQuantity: number = 0;

  //mapping
  product: Product = new Product();
  uom: UoM = new UoM();
}
export class FulfillmentFrOrder{
  id: number = 0;
  code: string  = '';
  teamId: number = 0;
  statusId: number = 0;
  fulfillmentName: string= '';
  deliveryDate: Date = new Date(Date.now());
  //mapping
  team: Team = new Team();
  status: Status = new Status();
  items: FulfillmentFrOrderItem[] = [];
}

export class FulfillmentFrOrderItem{
  id: number = 0;
  fulfillmentFrId = 0;
  productId: number = 0;
  uomId: number = 0;
  adapQuantity: number = 0;
  deliveriedQuantity: number = 0;
  traceCode: string='';
  //mapping
  product: Product = new Product();
  uom: UoM = new UoM();
}

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

  //mapping
  imageData: string = '';
  address: Address = new Address();
  contact: Contact = new Contact();
}

export class Status{
  id: number = 0;
  name: string ='';
  description: string = '';

  public constructor(init?: Partial<Status>) {
    Object.assign(this, init);
  }
}
export class Team{
  id: number = 0;
  name: string = '';
}

export class Pack{
  id: number = 0;
  code: string = '';
  name: string = '';
  retailerId: number = 0;
  retailerBuyingCalanderId: number = 0;
  statusId: number = 0;
  buyingDate: Date = new Date(Date.now());
  billTo: number = 0;
  shipTo: number = 0;
  totalAmount: number = 0;
  fulfillmentIdTo:number = 0;
  fulfillmentName: string ='';
  //mapping
  items: PackItem[] = [];
}
export class PackItem{
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
export class FulfillmentService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlFulfillmentCollection: string = '/api/Fulfillment/get/odercollection';
  private urlGetCollection: string = '/api/Fulfillment/get/collection';
  private urlGetFulfillmentCollectionById: string = '/api/Fulfillment/get/FulfillmentById';
  private urlGetFulfillment: string = '/api/Fulfillment/gets';
  private urlAddFulfillment: string = '/api/Fulfillment/add/fulfillmentCollection';
  private urlGetFulfillmentByFcId: string = '/api/Fulfillment/get/fulfillmentCollectionByFcId'
  private urlGetStatus: string = '/api/Fulfillment/get/fulfillmentCollectionStatus';
  private urlGetTeam: string = '/api/FulfillmentFR/get/team';
  private urlGetRetailerPack: string ='/api/FulfillmentFR/get/retailerorderforpack';
  private urlGetRetailerById: string ='/api/FulfillmentFR/get/retailerorderbyid';
  private urlGetFcProduct: string ='/api/Fulfillment/get/getfcproduct';


  getFulfillmentCollection(): Observable<ResultModel<FulfillmentCollection[]>> {
    return this.http.post<ResultModel<FulfillmentCollection[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlFulfillmentCollection}`, this.authenticSvc.getHttpHeader());
  }

  getFulfillmentCollectionById(collectionId: number): Observable<ResultModel<FulfillmentCollection[]>> {
    return this.http.post<ResultModel<FulfillmentCollection[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlGetFulfillmentCollectionById}?id=${collectionId}`, this.authenticSvc.getHttpHeader());
  }
  
  getCollection(): Observable<ResultModel<Collection[]>> {
    return this.http.post<ResultModel<Collection[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlGetCollection}`, this.authenticSvc.getHttpHeader());
  }

  getFulfillment(): Observable<ResultModel<Fulfillment[]>> {
    return this.http.get<ResultModel<Fulfillment[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlGetFulfillment}`, this.authenticSvc.getHttpHeader());
  }

  getFulfillmentCollectionByFcId(id: number): Observable<ResultModel<FulfillmentCollection[]>> {
    return this.http.post<ResultModel<FulfillmentCollection[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlGetFulfillmentByFcId}?id=${id}`, this.authenticSvc.getHttpHeader());
  }

  add(fc: FulfillmentCollection): Observable<ResultModel<any>> {
    debugger
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddFulfillment}`, { FulfillmentCollection: fc }, this.authenticSvc.getHttpHeader());
  }

  getFulfillmentStatus(): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlGetStatus}`, this.authenticSvc.getHttpHeader());
  }

  getTeams(): Observable<ResultModel<any>> {
    return this.http.get<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlGetTeam}`, this.authenticSvc.getHttpHeader());
  }
  
  getRetailerOrder(fulfillmentId: number): Observable<ResultModel<Pack[]>> {
    return this.http.get<ResultModel<Pack[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlGetRetailerPack}?id=${fulfillmentId}`, this.authenticSvc.getHttpHeader());
  }

  getRetailerOrderById(Id: number): Observable<ResultModel<Pack[]>> {
    return this.http.get<ResultModel<Pack[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlGetRetailerById}?id=${Id}`, this.authenticSvc.getHttpHeader());
  }

  getFCProduct(): Observable<ResultModel<any>> {
    return this.http.get<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlGetFcProduct}`, this.authenticSvc.getHttpHeader());
  }
}
