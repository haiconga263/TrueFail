import { Component, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { appUrl } from 'src/aritnt/administrator/app-url';
// import { RetailerOrderDetailService, RetailerOrderDetail } from '../retailer-order-temp.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { ActivatedRoute, Params } from '@angular/router';
import { FulfillmentCollection, FulfillmentService, Team, FulfillmentFrOrder, FulfillmentFrOrderItem, Status, Pack, Fulfillment } from '../../fulfillment.service';
import { Product, ProductService } from 'src/aritnt/administrator/product/product.service';
import { UoM } from 'src/aritnt/administrator/common/services/uom.service';
import { UoMService } from 'src/aritnt/administrator/uom/uom.service';
import { AppConsts } from 'src/core/constant/AppConsts';

@Component({
  selector: 'pack-detail',
  templateUrl: './pack-detail.component.html',
  styleUrls: ['./pack-detail.component.css']
})
export class PackDetailComponent extends AppBaseComponent {
  private params: Params;
  private type: string = 'add';
  private id: number = 0;

  fcModel: FulfillmentCollection[] = [];
  fulfillmentFrs: FulfillmentFrOrder[] = [];
  fulfillmentFr: FulfillmentFrOrder = new FulfillmentFrOrder();
  fulfillmentFrItem: FulfillmentFrOrderItem = new FulfillmentFrOrderItem();
  selectedRows: FulfillmentCollection = new FulfillmentCollection();
  private products: Product[] = [];
  private uoms: UoM[] = [];
  private teams: Team[] = [];
  private statusS: Status[] = [];
  private packs: Pack[] = [];
  private sta: Status = new Status();
  private roId: number = 0;
  private fulfillments: Fulfillment[] = [];
  private fulfillment: Fulfillment = new Fulfillment();
  private numberPattern = AppConsts.numberPattern;
  _selectedProduct: Product[] = [];
    isDropDownBoxOpened: boolean = false;

  uomSelectedId: number = 0;

  constructor(
    injector: Injector,
    private activatedRoute: ActivatedRoute,
    private fufilmentSvc: FulfillmentService,
    private productSvc: ProductService,
    private uomSvc: UoMService,
  ) {
    super(injector);

    this.activatedRoute.queryParams.subscribe((params: Params) => {
      this.params = params;
      if (this.params['type'] == 'update') {
        this.type = 'update';
        this.id = this.params["id"]
        this.roId = this.params["id"]
      }
      this.roId = this.params["id"];
    });

    this.fufilmentSvc.getFulfillment().subscribe(rs => {
      if (rs.result == ResultCode.Success) {
        this.fulfillments = rs.data;
        this.fulfillment = this.fulfillments[0];
      }
    });


    this.loadDatasource();
  }

  loadDatasource(callback: () => void = null) {
    this.fufilmentSvc.getRetailerOrderById(this.roId).subscribe((result) => {
      if (result.result == ResultCode.Success) {
        this.packs = result.data;
      }
      console.log(this.packs);
      if (FuncHelper.isFunction(callback))
        callback();

    });
    this.fufilmentSvc.getFCProduct().subscribe((result) => {
      if (result.result == ResultCode.Success) {
        this.products = result.data;
      }
      // this.selectedProduct = this.products;
      console.log(this.products);
    });

    this.uomSvc.gets().subscribe((result) => {
      if (result.result == ResultCode.Success) {
        this.uoms = result.data;
      }
    });

    this.fufilmentSvc.getFulfillment().subscribe((result) => {
      if (result.result == ResultCode.Success) {
        this.fulfillments = result.data;
      }

      if (FuncHelper.isFunction(callback))
        callback();
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
  }

  private return() {
    console.log('return');
    this.router.navigate([appUrl.productList]);
  }

  private refresh() {
    this.loadDatasource();
  }
  private onSelectionChanged(event: any) {
    if (event.selectedRowsData != null && event.selectedRowsData.length == 1) {
      this.selectedRows = event.selectedRowsData[0];
      console.log(this.selectedRows.items[0].productId);
    }
  }
  private save() {
    console.log('save');

    if (this.type == "update") {

    }
  }
  // changeDropDownBoxValue(event: any) {

  //   this._selectedProduct = event.addedItems;
  //   this.isDropDownBoxOpened = false;

  //   console.log(this._selectedProduct);

  // }

  display_Prt(value: Product) {
    return value.defaultName;
  }

  get selectedProduct(): Product[] {
    return this._selectedProduct;
  }

  set selectedProduct(value: Product[]) {
    this._selectedProduct = value || [];
    console.log(this._selectedProduct);
  }

  ngOnInit() {
  }
}