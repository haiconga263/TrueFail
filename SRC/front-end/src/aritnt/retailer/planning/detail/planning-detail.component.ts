import { Component, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { PlanningService, Planning } from '../planning.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from '../../app-url';
import { ActivatedRoute, Params } from '@angular/router';
import { ResultModel } from 'src/core/models/http.model';
import { Product, ProductService } from '../../common/services/product.service';
import { UoM, UoMService } from 'src/aritnt/retailer/common/services/uom.service';
import { ReturnStatement } from '@angular/compiler';
import { AppConsts } from 'src/core/constant/AppConsts';

@Component({
  selector: 'planning-detail',
  templateUrl: './planning-detail.component.html',
  styleUrls: ['./planning-detail.component.css']
})
export class PlanningDetailComponent extends AppBaseComponent {
  private params: Params;
  private type: string = 'add';
  private id: number = 0;

  private planning: Planning = new Planning();
  private products: Product[] = [];
  private uoms: UoM[] = [];

  private isNameValid: boolean = false;

  private now: Date = new Date(Date.now());
  private numberPattern = AppConsts.numberPattern;

  constructor(
    injector: Injector,
    private plnSvc: PlanningService,
    private proSvc: ProductService,
    private uomSvc: UoMService,
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
    this.Init();
  }

  async Init() {
    this.proSvc.getsForOrder().subscribe(proRs => {
      if (proRs.result == ResultCode.Success) {
        this.products = proRs.data;
      }
    });

    this.uomSvc.gets().subscribe(uomRs => {
      if (uomRs.result == ResultCode.Success) {
        this.uoms = uomRs.data;
      }
    });

    if (this.params['type'] == 'update') {
      await this.loadDatasource();
    }
  }

  private async loadDatasource() {

    var rs = await this.plnSvc.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.planning = rs.data;
    }
  }

  private return() {
    console.log('return');
    this.router.navigate([appUrl.planningList]);
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    console.log('save');
    console.log(this.planning);
    if (!this.checkValid()) {
      return;
    }

    if (this.type == "update") {
      this.plnSvc.update(this.planning).subscribe((result: ResultModel<any>) => {
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
      this.plnSvc.add(this.planning).subscribe((result: ResultModel<any>) => {
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
    let product = this.planning.items.find(i => i.productId == event.data.productId);
    if (product != null) {
      this.showError("Order.Planning.Item.ExistedProduct");
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
    console.log(this.planning.items);
    // check product is exsited in items
    if (event.newData.productId != null) {
      let product = this.planning.items.find(i => i.productId == event.newData.productId && i.id != event.key.__KEY__);
      if (product != null) {
        this.showError("Order.Planning.Item.ExistedProduct");
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

  private checkValid(): boolean {
    return this.isNameValid && this.planning.items.length > 0;
  }


  ngOnInit() {
  }
}
