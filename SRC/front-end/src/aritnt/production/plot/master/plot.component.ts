import { Component, OnInit, Injector, Input } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { Plot, PlotService } from 'src/aritnt/common/services/plot.service';
import { Farmer, FarmerService } from 'src/aritnt/administrator/farmer/farmer.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { ErrorAlert } from 'src/core/alert/alert.service';
import DataSource from 'devextreme/data/data_source';
import ArrayStore from 'devextreme/data/array_store';
import { appUrl } from '../../app-url';

@Component({
  selector: 'plot',
  templateUrl: './plot.component.html',
  styleUrls: ['./plot.component.css']
})
export class PlotComponent extends AppBaseComponent {
  @Input('plotsSelected')
  private farmers: Farmer[] = [];
  private plots: Plot[] = [];
  private plotsDS: any[] = [];

  constructor(
    injector: Injector,
    private farmerService: FarmerService,
    private plotService: PlotService,
  ) {
    super(injector);
  }

  async loadData() {
    this.plotsDS = [];
    let farmerRs = await this.farmerService.gets().toPromise();
    if (farmerRs.result < ResultCode.Success) {
      this.alertSvc.alert(new ErrorAlert({ text: farmerRs.errorMessage }));
      return;
    }

    this.farmers = farmerRs.data;

    let plotRs = await this.plotService.gets().toPromise();
    if (plotRs.result < ResultCode.Success) {
      this.alertSvc.alert(new ErrorAlert({ text: plotRs.errorMessage }));
      return;
    }
    this.plots = plotRs.data;

  }

  getPlots(farmerId: number) {
    let item = this.plotsDS.find((x) => x.key === farmerId);
    if (!item) {
      item = {
        key: farmerId,
        dataSourceInstance: new DataSource({
          store: new ArrayStore({
            data: this.plots,
            key: "id"
          }),
          filter: ["farmerId", "=", farmerId]
        })
      };
      this.plotsDS.push(item);
    }
    return item.dataSourceInstance;
  }

  navigateInsertPlot() {
    this.router.navigate([appUrl.plotDetail]);
  }

  navigatePlotDetail() {
    this.router.navigate([appUrl.plotDetail], {
      queryParams: {
        id: this._plotSelected[0]
      }
    });
  }

  deletePlot() {

  }

  private _plotSelected: number[] = [];
  get plotSelected() {
    return this._plotSelected;
  }

  set plotSelected(val) {
    this._plotSelected = val;
  }

  ngOnInit() {
    super.ngOnInit();
    this.loadData();
  }
}
