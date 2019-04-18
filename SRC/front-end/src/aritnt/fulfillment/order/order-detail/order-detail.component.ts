import { Component, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { appUrl } from 'src/aritnt/administrator/app-url';
// import { RetailerOrderDetailService, RetailerOrderDetail } from '../retailer-order-temp.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { ActivatedRoute, Params } from '@angular/router';
import { FulfillmentCollection, FulfillmentService, Team, FulfillmentFrOrder, FulfillmentFrOrderItem, Status } from '../../fulfillment.service';
import { Product, ProductService } from 'src/aritnt/administrator/product/product.service';
import { UoM } from 'src/aritnt/administrator/common/services/uom.service';
import { UoMService } from 'src/aritnt/administrator/uom/uom.service';

@Component({
  selector: 'order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.css']
})
export class OrderDetailComponent extends AppBaseComponent {
  private params: Params;
  private type: string = 'add';
  private id: number = 0;
  private fulfillment: FulfillmentCollection = new FulfillmentCollection();
  fcModel: FulfillmentCollection[] = [];
  fulfillmentFrs: FulfillmentFrOrder[] =[];
  fulfillmentFr: FulfillmentFrOrder = new FulfillmentFrOrder();
  fulfillmentFrItem: FulfillmentFrOrderItem = new FulfillmentFrOrderItem();
  selectedRows: FulfillmentCollection = new FulfillmentCollection();
  private products: Product[] = [];
  private uoms: UoM[] = [];
  private teams: Team[] = [];
  private statusS: Status[] = [];

  private sta: Status = new Status();

  constructor(
    injector: Injector,
    private activatedRoute: ActivatedRoute,
    private fufilmentSvc: FulfillmentService,
    private productSvc: ProductService,
    private uomSvc: UoMService,
  ) {
    super(injector);
    
    this.activatedRoute.queryParams.subscribe((params: Params) => {
      debugger
      this.params = params;
      console.log('param' + this.params.id);
      if (this.params['type'] == 'update') {
        this.type = 'update';
        this.id = this.params["id"];
        console.log('param' + this.params["id"]);
      }
    });
    
    this.Init();
  }

  async Init() {
    await this.loadByCollectionSource(this.id);


    this.productSvc.getsForOrder().subscribe((result) => {
      if (result.result == ResultCode.Success) {
        this.products = result.data;
      }
    });

    this.uomSvc.gets().subscribe((result) => {
      if (result.result == ResultCode.Success) {
        this.uoms = result.data;
      }
    });
    this.fufilmentSvc.getFulfillmentStatus().subscribe((result) => {
      if (result.result == ResultCode.Success) {
        this.statusS = result.data;
      }
    });
    this.fufilmentSvc.getTeams().subscribe((result) => {
      if (result.result == ResultCode.Success) {
        this.teams = result.data;
      }
    });

    if (this.params['type'] == 'update') {
      await this.loadDatasource();
    }  
  }

  private async loadDatasource() {    
  }

  private return() {
    console.log('return');
    this.router.navigate([appUrl.productList]);
  }

  private refresh() {
    this.loadDatasource();
  }
  private onSelectionChanged(event: any){
    if(event.selectedRowsData != null && event.selectedRowsData.length == 1){  
      this.selectedRows = event.selectedRowsData[0]; 
      console.log(this.selectedRows.items[0].productId);
    }    
  }
  private save() {
    console.log('save');

    if (this.type == "update") {
      
    }    
  }

  private loadByCollectionSource(id: number) {
    this.fufilmentSvc.getFulfillmentCollectionByFcId(id).subscribe(result => {      
      if (result.result == ResultCode.Success) {
        this.fcModel = result.data;
      }
      this.fcModel.forEach(element => {
        this.fulfillmentFr.id = element.id;
        this.fulfillmentFr.code = element.code;   
        this.fulfillmentFr.fulfillmentName = element.fulfillment.name;
        this.fulfillmentFr.deliveryDate = element.deliveryDate;    
        this.fulfillmentFr.status = new Status();
        element.items.forEach(elements => {
          this.fulfillmentFrItem.id = elements.id;
          this.fulfillmentFrItem.productId = elements.productId;
          this.fulfillmentFrItem.deliveriedQuantity = elements.shippedQuantity;
          this.fulfillmentFrItem.uomId = elements.uoMId;
          this.fulfillmentFrItem.traceCode = elements.traceCode;
          this.fulfillmentFrItem.adapQuantity = 0;
          
          this.fulfillmentFr.items.push(this.fulfillmentFrItem);
        });
        this.fulfillmentFrs.push(this.fulfillmentFr);
      });     
      console.log( this.fulfillmentFrs);
    });
  }
  ngOnInit() {
  }
}