import { Component, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { FarmerOrderService, FarmerOrder, FarmerOrderItem, FarmerOrderStatuses } from 'src/aritnt/administrator/farmer/farmer-order.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from '../../../app-url';
import { ActivatedRoute, Params } from '@angular/router';
import { ResultModel } from 'src/core/models/http.model';
import { Product, ProductService } from 'src/aritnt/administrator/product/product.service';
import { UoM, UoMService } from 'src/aritnt/administrator/uom/uom.service';
import { FarmerPlanningService, FarmerPlanning } from 'src/aritnt/administrator/farmer/farmer-planning.service';
import { CollectionService, Collection } from 'src/aritnt/administrator/collection/collection.service';
import { FarmerService, Farmer } from '../../farmer.service';
import { AppConsts } from 'src/core/constant/AppConsts';

@Component({
  selector: 'order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.css']
})
export class OrderDetailComponent extends AppBaseComponent {
  private type: string = 'add';
  private id: number = 0;
  private planningId: number = 0;
  private returnUrl: string = '';

  private createType: number = 0;
  private types: any[] = [];

  private order: FarmerOrder = new FarmerOrder();
  private planning: FarmerPlanning = null;
  private products: Product[] = [];
  private uoms: UoM[] = [];
  private unCompletedPlannings: FarmerPlanning[] = null;
  private collections: Collection[] = [];
  private farmers: Farmer[] = [];

  private isNameValid: boolean = false;
  private isBuyingDateValid: boolean = true;
  private isShipValid: boolean = false;

  private now: Date = new Date(Date.now());
  private numberPattern = AppConsts.numberPattern;

  constructor(
    injector: Injector,
    private orderSvc: FarmerOrderService,
    private plnSvc: FarmerPlanningService,
    private farmerSvc: FarmerService,
    private proSvc: ProductService,
    private uomSvc: UoMService,
    private collectionSvc: CollectionService,
    private activatedRoute: ActivatedRoute
  ) {
    super(injector);
    this.activatedRoute.queryParams.subscribe((params: Params) => {
      if (params['type'] == 'update' || params['type'] == 'infor') {
        this.type = params['type'];
        this.id = params["id"];
      }
      else {
        this.type = 'add';
        if (params["planningId"] != null)
          this.planningId = params["planningId"];
      }
      this.returnUrl = params["returnUrl"] == null ? appUrl.farmerOrder : params["returnUrl"];
      console.log(this.type);
    });

    this.types = this.orderSvc.getCreateTypes();
    this.Init();
  }

  async Init() {
    let proRs = await this.proSvc.getsForOrder().toPromise();
    if (proRs.result == ResultCode.Success) {
      this.products = proRs.data;
    }

    let uomRs = await this.uomSvc.gets().toPromise();
    if (uomRs.result == ResultCode.Success) {
      this.uoms = uomRs.data;
    }

    let locaRs = await this.collectionSvc.gets().toPromise();
    if (locaRs.result == ResultCode.Success) {
      this.collections = locaRs.data;
    }

    let farmerRs = await this.farmerSvc.gets().toPromise();
    if (farmerRs.result == ResultCode.Success) {
      this.farmers = farmerRs.data;
    }

    if (this.type == 'update' || this.type == 'infor') {
      await this.loadDatasource();
    }
    else if (this.type == 'add' && this.planningId != 0) {
      this.createType = 1;
      var rs = await this.plnSvc.get(this.planningId).toPromise();
      if (rs.result == ResultCode.Success) {
        if (rs.data != null) {
          this.unCompletedPlannings = [];
          this.unCompletedPlannings.push(rs.data);
          this.planning = rs.data;
          this.planningChange(null);
        }
        else {
          this.showError("Order.Farmer.Planning.NotExisted");
          this.return();
        }
      }
      else {
        this.showError("Order.Farmer.Planning.NotExisted");
        this.return();
      }

      if(this.farmers.find(f => f.id == this.planning.farmerId) == null) {
        this.showError("Farmer.NotExisted");
        this.return();
      }
    }
  }

  private async loadDatasource() {

    var rs = await this.orderSvc.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      if(this.type == "update" && rs.data.statusId != FarmerOrderStatuses.BeginOrder) {
        this.showError("Order.WrongStep");
        this.return();
      }

      this.order = rs.data;
      this.order.items.forEach(item => {
        item.totalAmount = item.price * item.orderedQuantity
      });
    }
  }

  private return() {
    console.log('return');
    this.router.navigate([this.returnUrl]);
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    console.log('save');
    console.log(this.order);
    if (!this.checkValid()) {
      return;
    }

    if (this.type == "update") {
      this.order.collectionId = this.order.shipTo;
      this.orderSvc.update(this.order).subscribe((result: ResultModel<any>) => {
        if (result.result == ResultCode.Success) {
          //alert
          this.showSuccess(this.lang.instant('Common.UpdateSuccess'));
          this.return();
        }
        else {
          //alert
          this.showError(this.lang.instant('Common.UpdateFail'));
        }
      });
    }
    else if(this.type == "add") {
      if (this.createType == 1 && this.planning != null) {
        this.order.farmerBuyingCalendarId = this.planning.id;
      }
      this.order.collectionId = this.order.shipTo;
      this.orderSvc.add(this.order).subscribe((result: ResultModel<any>) => {
        if (result.result == ResultCode.Success) {
          //alert
          this.showSuccess(this.lang.instant('Common.AddSuccess'));
          this.return();
        }
        else {
          //alert
          this.showError(this.lang.instant('Common.AddFail'));
        }
      });
    }
  }

  private insertingItem(event: any) {
    console.log(event);

    // check product is exsited in items
    let product = this.order.items.find(i => i.productId == event.data.productId);
    if (product != null) {
      this.showError("Order.Order.Item.ExistedProduct");
      event.cancel = true;
      return;
    }

    let checkingProduct = this.products.find(p => p.id == event.data.productId);
    if (checkingProduct == null) {
      this.showError("Product.NotExsited");
      event.cancel = true;
      return;
    }
    else {
      let checkingProductPrice = checkingProduct.prices.find(p => p.uoMId == event.data.uoMId);
      if (checkingProductPrice == null) {
        this.showError("Product.Price.NotExsited");
        event.cancel = true;
        return;
      }
    }

  }

  private updatingItem(event: any) {
    console.log(event);
    console.log(this.order.items);
    // check product is exsited in items
    if (event.newData.productId != null) {
      let product = this.order.items.find(i => i.productId == event.newData.productId && i.id != event.key.__KEY__);
      if (product != null) {
        this.showError("Order.Order.Item.ExistedProduct");
        event.cancel = true;
        return;
      }
    }

    if (event.newData.uoMId != null) {
      let checkingProduct = this.products.find(p => p.id == event.newData.productId || p.id == event.oldData.productId);
      if (checkingProduct == null) {
        this.showError("Product.NotExsited");
        event.cancel = true;
        return;
      }
      else {
        let checkingProductPrice = checkingProduct.prices.find(p => p.uoMId == event.newData.uoMId);
        if (checkingProductPrice == null) {
          this.showError("Product.Price.NotExsited");
          event.cancel = true;
          return;
        }
      }
    }
  }

  private insertedItem(event: any) {
    console.log("inserted");
    console.log(event);
    console.log(this.order.items);

    let item = this.order.items.find(i => i.__KEY__ == event.key.__KEY__);
    if (item != null) {
      let product = this.products.find(p => p.id == item.productId);
      let price = product.prices.find(p => p.uoMId == item.uoMId);
      item.price = price.sellingPrice;
      item.totalAmount = item.price * item.orderedQuantity;
      event.component.refresh(true);
    }
  }

  private updatedItem(event: any) {
    console.log("updated");
    console.log(event);

    if (event.key.__KEY__ != null) {
      let item = this.order.items.find(i => i.__KEY__ == event.key.__KEY__);
      if (item != null) {
        let product = this.products.find(p => p.id == item.productId);
        let price = product.prices.find(p => p.uoMId == item.uoMId);
        item.price = price.sellingPrice;
        item.totalAmount = item.price * item.orderedQuantity;
        event.component.refresh(true);
      }
    }
    else if (event.key.id != null) {
      let item = this.order.items.find(i => i.id == event.key.id);
      if (item != null) {
        let product = this.products.find(p => p.id == item.productId);
        let price = product.prices.find(p => p.uoMId == item.uoMId);
        item.price = price.sellingPrice;
        item.totalAmount = item.price * item.orderedQuantity;
        event.component.refresh(true);
      }
    }
  }

  private planningChange(event) {
    console.log(event);
    this.order.buyingDate = this.planning.buyingDate;
    this.order.items = [];
    this.order.farmerId = this.planning.farmerId;
    this.planning.items.forEach(item => {
      let orderItem = new FarmerOrderItem();
      orderItem.productId = item.productId;
      orderItem.uoMId = item.uoMId;
      orderItem.orderedQuantity = item.quantity;

      let product = this.products.find(p => p.id == item.productId);
      if (product != null) {
        let price = product.prices.find(p => p.uoMId == item.uoMId);
        if (price != null) {
          orderItem.price = price.sellingPrice;
          orderItem.totalAmount = orderItem.price * orderItem.orderedQuantity;
          this.order.items.push(orderItem);
        }
      }
    });
  };

  private async unCompletedPlanningsFocusIn(event: any) {
    if (this.unCompletedPlannings == null) {
      var rs = await this.plnSvc.getsUncompleted().toPromise();
      if (rs.result == ResultCode.Success) {
        this.unCompletedPlannings = rs.data;
      }
    }
  }

  private checkValid(): boolean {
    return this.isBuyingDateValid && this.isNameValid && this.isShipValid && this.order.items.length > 0;
  }


  ngOnInit() {
  }
}
