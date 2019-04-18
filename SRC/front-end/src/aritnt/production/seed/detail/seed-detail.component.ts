import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from 'src/aritnt/production/app-url';
import { ResultModel } from 'src/core/models/http.model';
import { Language } from 'src/core/common/language.service';
import { ParamUrlKeys } from 'src/core/constant/AppConsts';
import { Seed, SeedService } from 'src/aritnt/common/services/seed.service';
import { Injector, Component } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { ProductService, Product } from 'src/aritnt/common/services/product.service';

@Component({
  selector: 'seed-detail',
  templateUrl: './seed-detail.component.html',
  styleUrls: ['./seed-detail.component.css']
})
export class SeedDetailComponent extends AppBaseComponent {
  private id: number = 0;
  private seed: Seed = new Seed();
  private products: Product[] = [];

  private isNameValid: boolean = false;
  private isProductValid: boolean = false;

  constructor(
    injector: Injector,
    private seedService: SeedService,
    private productService: ProductService,
    private activatedRoute: ActivatedRoute
  ) {
    super(injector);
    this.activatedRoute.queryParams.subscribe((params: Params) => {
      this.id = params["id"];
    });
    this.Init();
  }

  async Init() {
    var productRs = await this.productService.getsOnly().toPromise();
    if (productRs.result == ResultCode.Success) {
      this.products = productRs.data;
    }

    if (this.id != null && this.id > 0) {
      await this.loadDatasource();
    }
  }

  private async loadDatasource() {
    var rs = await this.seedService.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.seed = rs.data;
    }
  }

  private return() {
    this.router.navigate([appUrl.seedList]);
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    if (!this.checkValid()) {
      return;
    }

    if (this.id != null && this.id > 0) {
      this.seedService.update(this.seed).subscribe((result: ResultModel<any>) => {
        if (result.result == ResultCode.Success) {
          //alert
          this.showSuccess(this.lang.instant('Common.UpdateSuccess'));
          this.return();
        }
        else {
          //alert
          this.showError(result.errorMessage);
        }
      });
    }
    else {
      this.seedService.add(this.seed).subscribe((result: ResultModel<any>) => {
        if (result.result == ResultCode.Success) {
          //alert
          this.showSuccess(this.lang.instant('Common.AddSuccess'));
          this.return();
        }
        else {
          //alert
          this.showError(result.errorMessage);
        }
      });
    }
  }

  private checkValid(): boolean {
    return this.isNameValid;
  }

  // *** Product
  _gridSelectedRowKeys: number[] = [];
  gridBox_displayExpr(item: any) {
    return item && (item.code + " - " + item.defaultName);
  }

  get gridProductId(): number {
    return this.seed.productId;
  }

  set gridProductId(value: number) {
    this._gridSelectedRowKeys = value && [value] || [];
    this.seed.productId = value;
  }

  get gridSelectedProducts(): number[] {
    return this._gridSelectedRowKeys;
  }

  set gridSelectedProducts(value: number[]) {
    this.seed.productId = value.length && value[0] || null;
    this._gridSelectedRowKeys = value;
  }

  //

  ngOnInit() {
  }
}
