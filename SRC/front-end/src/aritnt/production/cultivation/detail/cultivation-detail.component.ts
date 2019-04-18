import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from 'src/aritnt/production/app-url';
import { ResultModel } from 'src/core/models/http.model';
import { Cultivation, CultivationService, CultivationStatusArray } from 'src/aritnt/common/services/cultivation.service';
import { Injector, Component } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Seed, SeedService } from 'src/aritnt/common/services/seed.service';
import { Plot, PlotService } from 'src/aritnt/common/services/plot.service';
import { Farmer } from 'src/aritnt/administrator/farmer/farmer.service';
import { FarmerService } from 'src/aritnt/common/services/farmer.service';
import { Product, ProductService } from 'src/aritnt/common/services/product.service';
import { MethodService, Method } from 'src/aritnt/common/services/method.service';
import { FuncHelper } from 'src/core/helpers/function-helper';

@Component({
  selector: 'cultivation-detail',
  templateUrl: './cultivation-detail.component.html',
  styleUrls: ['./cultivation-detail.component.css']
})
export class CultivationDetailComponent extends AppBaseComponent {
  private id: number = 0;
  private cultivation: Cultivation = new Cultivation();
  private seeds: Seed[] = [];
  private plots: Plot[] = [];
  private farmers: Farmer[] = [];
  private products: Product[] = [];
  private methods: Method[] = [];
  private cultivationStatuss: any[] = CultivationStatusArray;
  private isSeedValid: boolean = false;
  private isPlotValid: boolean = false;

  constructor(
    injector: Injector,
    private cultivationService: CultivationService,
    private farmerService: FarmerService,
    private seedService: SeedService,
    private plotService: PlotService,
    private productService: ProductService,
    private methodService: MethodService,
    private activatedRoute: ActivatedRoute
  ) {
    super(injector);
    this.activatedRoute.queryParams.subscribe((params: Params) => {
      this.id = params["id"];
    });
    this.Init();
  }

  async Init() {
    var seedRs = await this.seedService.gets().toPromise();
    if (seedRs.result == ResultCode.Success) {
      this.seeds = seedRs.data;
    }

    var farmerRs = await this.farmerService.gets().toPromise();
    if (farmerRs.result == ResultCode.Success) {
      this.farmers = farmerRs.data;
    }

    var plotRs = await this.plotService.gets().toPromise();
    if (plotRs.result == ResultCode.Success) {
      this.plots = plotRs.data;
    }

    var productRs = await this.productService.getsOnly().toPromise();
    if (productRs.result == ResultCode.Success) {
      this.products = productRs.data;
    }

    var methodRs = await this.methodService.gets().toPromise();
    if (methodRs.result == ResultCode.Success) {
      this.methods = methodRs.data;
      this.methods.unshift(new Method({ id: null, name: '- none -' }));
    }

    if (this.id != null && this.id > 0) {
      await this.loadDatasource();
    }
  }

  private async loadDatasource() {
    var rs = await this.cultivationService.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.cultivation = rs.data;
    }
  }

  private return() {
    this.router.navigate([appUrl.cultivationList]);
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    if (!this.checkValid()) {
      return;
    }

    if (this.id != null && this.id > 0) {
      this.cultivationService.update(this.cultivation).subscribe((result: ResultModel<any>) => {
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
      this.cultivationService.add(this.cultivation).subscribe((result: ResultModel<any>) => {
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
    return this.isPlotValid && this.isSeedValid;
  }

  // *** Seed
  _seedSelectedRowKeys: number[] = [];
  seed_displayExpr(item: any) {
    return item && (item.code);
  }

  get gridSeedId(): number {
    return this.cultivation.seedId;
  }

  set gridSeedId(value: number) {
    this._seedSelectedRowKeys = value && [value] || [];
    this.cultivation.seedId = value;
  }

  get gridSelectedSeeds(): number[] {
    return this._seedSelectedRowKeys;
  }

  set gridSelectedSeeds(value: number[]) {
    this.cultivation.seedId = value.length && value[0] || null;
    this._seedSelectedRowKeys = value;
  }

  // ***

  // *** Plot
  _plotSelectedRowKeys: number[] = [];
  plot_displayExpr(item: any) {
    return item && (item.code);
  }

  get gridPlotId(): number {
    return this.cultivation.plotId;
  }

  set gridPlotId(value: number) {
    this._plotSelectedRowKeys = value && [value] || [];
    this.cultivation.plotId = value;
  }

  get gridSelectedPlots(): number[] {
    return this._plotSelectedRowKeys;
  }

  set gridSelectedPlots(value: number[]) {
    this.cultivation.plotId = value.length && value[0] || null;
    this._plotSelectedRowKeys = value;
  }

  // ***

  ngOnInit() {
  }
}
