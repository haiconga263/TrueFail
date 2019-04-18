import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from '../../app-url';
import { ResultModel } from 'src/core/models/http.model';
import { Language } from 'src/core/common/language.service';
import { ParamUrlKeys } from 'src/core/constant/AppConsts';
import { Plot, PlotService } from 'src/aritnt/common/services/plot.service';
import { Injector, Component } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { FarmerService, Farmer } from 'src/aritnt/administrator/farmer/farmer.service';

@Component({
  selector: 'plot-detail',
  templateUrl: './plot-detail.component.html',
  styleUrls: ['./plot-detail.component.css']
})
export class PlotDetailComponent extends AppBaseComponent {
  private id: number = 0;
  private plot: Plot = new Plot();
  private farmers: Farmer[] = [];
  private isNameValid: boolean = false;

  constructor(
    injector: Injector,
    private plotService: PlotService,
    private farmerService: FarmerService,
    private activatedRoute: ActivatedRoute
  ) {
    super(injector);
    this.activatedRoute.queryParams.subscribe((params: Params) => {
      this.id = params["id"];
    });
    this.Init();
  }

  async Init() {
    var farmerRs = await this.farmerService.gets().toPromise();
    if (farmerRs.result < ResultCode.Success) {
      this.showError(farmerRs.errorMessage);
    }
    this.farmers = farmerRs.data;

    if (this.id != null && this.id > 0) {
      await this.loadDatasource();
    }
  }

  private async loadDatasource() {
    var rs = await this.plotService.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.plot = rs.data;
    }
  }

  private return() {
    this.router.navigate([appUrl.plotList]);
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    if (!this.checkValid()) {
      return;
    }

    if (this.id != null && this.id > 0) {
      this.plotService.update(this.plot).subscribe((result: ResultModel<any>) => {
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
      this.plotService.add(this.plot).subscribe((result: ResultModel<any>) => {
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

  // *** Farmer
  _gridSelectedRowKeys: number[] = [];
  gridBox_displayExpr(item: any) {
    return item && (item.code + " - " + item.name);
  }

  get gridFarmerId(): number {
    return this.plot.farmerId;
  }

  set gridFarmerId(value: number) {
    this._gridSelectedRowKeys = value && [value] || [];
    this.plot.farmerId = value;
  }

  get gridSelectedFarmers(): number[] {
    return this._gridSelectedRowKeys;
  }

  set gridSelectedFarmers(value: number[]) {
    this.plot.farmerId = value.length && value[0] || null;
    this._gridSelectedRowKeys = value;
  }

  //

  private checkValid(): boolean {
    return this.isNameValid;
  }

  ngOnInit() {
  }
}
