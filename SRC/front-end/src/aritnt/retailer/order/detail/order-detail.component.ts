import { Component, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { OrderService, Order, OrderItem } from '../order.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from '../../app-url';
import { ActivatedRoute, Params } from '@angular/router';
import { ResultModel } from 'src/core/models/http.model';
import { Product, ProductService } from '../../common/services/product.service';
import { UoM, UoMService } from 'src/aritnt/retailer/common/services/uom.service';
import { PlanningService, Planning } from '../../planning/planning.service';
import { LocationService, RetailerLocation } from '../../location/location.service';
import { AppConsts } from 'src/core/constant/AppConsts';

@Component({
  selector: 'order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.css']
})
export class OrderDetailComponent extends AppBaseComponent {
  private params: Params;
  private type: string = 'add';
  private id: number = 0;

  private createType: number = 0;
  private types: any[] = [];

  private order: Order = new Order();
  private planning: Planning = null;
  private products: Product[] = [];
  private uoms: UoM[] = [];
  private unCompletedPlannings: Planning[] = null;
  private locations: RetailerLocation[] = [];

  private isNameValid: boolean = false;
  private isBuyingDateValid: boolean = true;
  private isShipValid: boolean = false;
  private isBillValid: boolean = false;

  private now: Date = new Date(Date.now());
  private numberPattern = AppConsts.numberPattern;

  constructor(
    injector: Injector,
    private orderSvc: OrderService,
    private plnSvc: PlanningService,
    private proSvc: ProductService,
    private uomSvc: UoMService,
    private locationSvc: LocationService,
    private activatedRoute: ActivatedRoute
  ) {
    super(injector);
    this.activatedRoute.queryParams.subscribe((params: Params) => {
      this.params = params;
      if (this.params['type'] == 'update') {
        this.type = 'update';
        this.id = this.params["id"];
      }
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

    let locaRs = await this.locationSvc.gets(this.authenticSvc.getSession().id).toPromise();
    if (locaRs.result == ResultCode.Success) {
      this.locations = locaRs.data;
    }

    if (this.params['type'] == 'update') {
      await this.loadDatasource();
    }
  }

  private async loadDatasource() {

    var rs = await this.orderSvc.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.order = rs.data;
      this.order.items.forEach(item => {
        item.totalAmount = item.price * item.orderedQuantity
      });
    }
  }

  private return() {
    console.log('return');
    this.router.navigate([appUrl.orderList]);
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
      this.orderSvc.update(this.order).subscribe((result: ResultModel<any>) => {
        if(result.result == ResultCode.Success)
        {
          //alert
          this.showSuccess(this.lang.instant('Common.UpdateSuccess'));
          this.return();
        }
        else
        {
          //alert
          this.showError(this.lang.instant('Common.UpdateFail'));
        }
      });
    }
    else {
      if(this.createType == 1 && this.planning != null)
      {
        this.order.retailerBuyingCalendarId = this.planning.id;
      }
      this.orderSvc.add(this.order).subscribe((result: ResultModel<any>) => {
        if(result.result == ResultCode.Success)
        {
          //alert
          this.showSuccess(this.lang.instant('Common.AddSuccess'));
          this.return();
        }
        else
        {
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
    if(item != null)
    {
      let product = this.products.find(p => p.id == item.productId);
      let price = product.prices.find(p => p.uoMId == item.uoMId);
      item.price = price.sellingPrice;
      item.totalAmount = item.price * item.orderedQuantity;
    }
  }

  private updatedItem(event: any) {
    console.log("updated");
    console.log(event);

    if(event.key.__KEY__ != null)
    {
      let item = this.order.items.find(i => i.__KEY__ == event.key.__KEY__);
      if(item != null)
      {
        let product = this.products.find(p => p.id == item.productId);
        let price = product.prices.find(p => p.uoMId == item.uoMId);
        item.price = price.sellingPrice;
        item.totalAmount = item.price * item.orderedQuantity;
      }
    }
    else if(event.key.id != null)
    {
      let item = this.order.items.find(i => i.id == event.key.id);
      if(item != null)
      {
        let product = this.products.find(p => p.id == item.productId);
        let price = product.prices.find(p => p.uoMId == item.uoMId);
        item.price = price.sellingPrice;
        item.totalAmount = item.price * item.orderedQuantity;
      }
    }
  }

  private planningChange(event) {
    console.log(event);
    this.order.buyingDate = this.planning.buyingDate;
    this.order.items = [];
    this.planning.items.forEach(item => {
      let orderItem = new OrderItem();
      orderItem.productId = item.productId;
      orderItem.uoMId = item.uoMId;
      orderItem.orderedQuantity = item.quantity;

      let product = this.products.find(p => p.id == item.productId);
      if(product != null)
      {
        let price = product.prices.find(p => p.uoMId == item.uoMId);
        if(price != null)
        {
          orderItem.price = price.sellingPrice;
          orderItem.totalAmount = orderItem.price * orderItem.orderedQuantity;
          this.order.items.push(orderItem);
        }
      }
    });
  };

  private async unCompletedPlanningsFocusIn(event: any) {
    if(this.unCompletedPlannings == null)
    {
      var rs = await this.plnSvc.getsUncompletedByUser().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.unCompletedPlannings = rs.data;
      }
    }
  }

  private checkValid(): boolean {
    return this.isBuyingDateValid && this.isNameValid && this.isBillValid && this.isShipValid && this.order.items.length > 0;
  }


  ngOnInit() {
  }
}
