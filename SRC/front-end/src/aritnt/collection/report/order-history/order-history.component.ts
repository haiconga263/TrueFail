import { Component, Injector, ViewChild } from "@angular/core";
import { AppBaseComponent } from "src/core/basecommon/app-base.component";
import { ReportService } from "../report.service";
import { ResultCode } from "src/core/constant/AppEnums";
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from "src/core/helpers/function-helper";
import { DxDataGridComponent } from "devextreme-angular";
import { ReceivingService, Collection, FarmerOrderStatus, FarmerOrder } from "../../receiving/receiving.service";

@Component({
  selector: 'order-history',
  templateUrl: './order-history.component.html',
  styleUrls: ['./order-history.component.css']
})
export class OrderHistoryComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource: ArrayStore;
  selectedRows: number[];


  private from: Date = null;
  private to: Date = null;
  private collectionId: number = 0;
  private collections: Collection[] = [];
  private statuses: FarmerOrderStatus[] = [];

  constructor(
    injector: Injector,
    private rptSvc: ReportService,
    private recSvc: ReceivingService
  ) {
    super(injector);

    this.from = new Date(Date.now());
    this.to = new Date(Date.now());

    this.recSvc.getsByManager().subscribe(rs => {
      if(rs.result == ResultCode.Success) {
        this.collections = rs.data;
      }
    });

    this.recSvc.getStatuses().subscribe(rs => {
      if(rs.result == ResultCode.Success)
      {
        this.statuses = rs.data;
      }
    });
  }

  loadDatasource(callback: () => void = null) {
    if(this.collectionId == null || this.collectionId == 0) {
      this.showError("Common.SearchConditionError");
    }
    this.rptSvc.getHistories(this.from, this.to, this.collectionId).subscribe((result) => {
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

  private async print() {
    if(this.selectedRows != null && this.selectedRows.length == 1) {
      this.dataSource.byKey(this.selectedRows[0]).then((order: FarmerOrder) => {
        console.log(order);
        this.showInfor("Chức năng chưa hoàn thiện");
      });
    }
  }
}
