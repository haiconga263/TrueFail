import { Component, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { OrderService, Order, OrderItem } from '../../order/order.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from '../../app-url';
import { ActivatedRoute, Params } from '@angular/router';
import { ResultModel } from 'src/core/models/http.model';
import { Product, ProductService } from '../../common/services/product.service';
import { UoM, UoMService } from 'src/aritnt/retailer/common/services/uom.service';
import { PlanningService, Planning } from '../../planning/planning.service';
import { LocationService, RetailerLocation } from '../../location/location.service';

@Component({
  selector: 'order-history-detail',
  templateUrl: './order-history-detail.component.html',
  styleUrls: ['./order-history-detail.component.css']
})
export class OrderHistoryDetailComponent extends AppBaseComponent {
  private params: Params;
  private id: number = 0;

  private order: Order = new Order();
  private products: Product[] = [];
  private uoms: UoM[] = [];
  private locations: RetailerLocation[] = [];

  constructor(
    injector: Injector,
    private orderSvc: OrderService,
    private proSvc: ProductService,
    private uomSvc: UoMService,
    private locationSvc: LocationService,
    private activatedRoute: ActivatedRoute
  ) {
    super(injector);
    this.activatedRoute.queryParams.subscribe((params: Params) => {
      this.params = params;
      if(this.params["id"] == null)
      {
        this.router.navigate([appUrl.orderhistoryList]);
      }
      this.id = this.params["id"];
    });
    this.Init();
  }

  async Init() {
    this.proSvc.gets().subscribe(proRs => {
      if (proRs.result == ResultCode.Success) {
        this.products = proRs.data;
      }
    });

    this.uomSvc.gets().subscribe(uomRs => {
      if (uomRs.result == ResultCode.Success) {
        this.uoms = uomRs.data;
      }
    });

    this.locationSvc.gets(this.authenticSvc.getSession().id).subscribe(locaRs => {
      if(locaRs.result == ResultCode.Success)
      {
        this.locations = locaRs.data;
      }
    });

    await this.loadDatasource();
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
    this.router.navigate([appUrl.orderhistoryList]);
  }
}
