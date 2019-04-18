import { Component, Injector, ViewChild } from "@angular/core";
import { AppBaseComponent } from "src/core/basecommon/app-base.component";
import { ReportService } from "../report.service";
import { ResultCode } from "src/core/constant/AppEnums";
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from "src/core/helpers/function-helper";
import { DxDataGridComponent } from "devextreme-angular";
import { ReceivingService, Collection, FarmerOrderStatus, FarmerOrder } from "../../receiving/receiving.service";
import { Employee, EmployeeService } from "../../common/services/employee.service";
import { Product, ProductService } from "../../common/services/product.service";
import { UoM, UoMService } from "../../common/services/uom.service";
import { VehicleService, Vehicle } from "../../common/services/vehicle.service";
import { Fulfillment, FulfillmentService } from "../../common/services/fulfillment.service";
import { ShippingService } from "../../shipping/shipping.service";

@Component({
  selector: 'shipping-history',
  templateUrl: './shipping-history.component.html',
  styleUrls: ['./shipping-history.component.css']
})
export class ShippingHistoryComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource: ArrayStore;
  selectedRows: number[];


  private from: Date = null;
  private to: Date = null;
  private collectionId: number = 0;
  private collections: Collection[] = [];
  
  private employees: Employee[] = [];
  private fulfillments: Fulfillment[] = [];
  private vehicles: Vehicle[] = [];
  private statuses: any[] = [];

  constructor(
    injector: Injector,
    private rptSvc: ReportService,
    private recSvc: ReceivingService,
    private vehSvc: VehicleService,
    private fulSvc: FulfillmentService,
    private empSvc: EmployeeService,
    private shipSvc: ShippingService,
    
  ) {
    super(injector);

    this.from = new Date(Date.now());
    this.to = new Date(Date.now());

    this.recSvc.getsByManager().subscribe(rs => {
      if(rs.result == ResultCode.Success) {
        this.collections = rs.data;
      }
    });

    this.empSvc.gets().subscribe(rs => {
      if (rs.result == ResultCode.Success) {
        this.employees = rs.data;
      }
    });

    this.vehSvc.gets().subscribe(rs => {
      if (rs.result == ResultCode.Success) {
        this.vehicles = rs.data;
      }
    });

    this.fulSvc.gets().subscribe(rs => {
      if (rs.result == ResultCode.Success) {
        this.fulfillments = rs.data;
      }
    });
    
    this.statuses = this.shipSvc.getStatuses();
  }

  loadDatasource(callback: () => void = null) {
    if(this.collectionId == null || this.collectionId == 0) {
      this.showError("Common.SearchConditionError");
    }
    this.rptSvc.getShippingHistories(this.from, this.to, this.collectionId).subscribe((result) => {
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

  private getStatus(id: number) {
    return this.statuses.find(s => s.id == id);
  }
}
