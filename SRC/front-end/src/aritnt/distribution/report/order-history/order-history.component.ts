import { Component, Injector, ViewChild } from "@angular/core";
import { AppBaseComponent } from "src/core/basecommon/app-base.component";
import { ReportService } from "../report.service";
import { ResultCode } from "src/core/constant/AppEnums";
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from "src/core/helpers/function-helper";
import { DxDataGridComponent } from "devextreme-angular";
import { Distribution, DistributionService } from "../../common/services/distribution.service";

@Component({
  selector: 'order-history',
  templateUrl: './order-history.component.html',
  styleUrls: ['./order-history.component.css']
})
export class OrderHistoryComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource: ArrayStore;
  selectedRows: number[];

  private distributions: Distribution[] = [];
  private chooseDistribution: Distribution;
  private from: Date = null;
  private to: Date = null;

  constructor(
    injector: Injector,
    private rptSvc: ReportService,
    private disSvc: DistributionService
  ) {
    super(injector);

    this.from = new Date(Date.now());
    this.to = new Date(Date.now());
    
    this.loadDistributionSource();
  }

  loadDistributionSource() {
    this.disSvc.getByOwners().subscribe(disRs => {
      if (disRs.result == ResultCode.Success) {
        this.distributions = disRs.data;
      }
    });
  }

  loadDatasource(callback: () => void = null) {
    if(this.chooseDistribution == null) {
      this.showError("Distribution.Report.RequiredDistribution");
      return;
    }
    this.rptSvc.getOrderHistorys(this.chooseDistribution.id, this.from, this.to).subscribe((result) => {
      if (result.result == ResultCode.Success) {

        this.dataSource = new ArrayStore({
          key: "id",
          data: result.data
        });

        if (FuncHelper.isFunction(callback))
          callback();
      }
    });
  }

  private search()
  {
    console.log(this.from);
    console.log(this.to);

    this.loadDatasource(() => {
      this.dataGrid.instance.refresh();
    });
  }
}
