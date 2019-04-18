import { Component, Injector, ViewChild } from "@angular/core";
import { AppBaseComponent } from "src/core/basecommon/app-base.component";
import { ReportService } from "../report.service";
import { ResultCode } from "src/core/constant/AppEnums";
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from "src/core/helpers/function-helper";
import { DxDataGridComponent } from "devextreme-angular";
import { Collection, ReceivingService } from "../../receiving/receiving.service";
import { Employee, EmployeeService } from "../../common/services/employee.service";
import { UserService } from "../../common/services/user.service";

@Component({
  selector: 'employee-history',
  templateUrl: './employee-history.component.html',
  styleUrls: ['./employee-history.component.css']
})
export class EmployeeHistoryComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource: ArrayStore;
  selectedRows: number[];


  private from: Date = null;
  private to: Date = null;
  private collectionId: number = 0;
  private collections: Collection[] = [];
  private employeeses: Employee[] = [];

  constructor(
    injector: Injector,
    private rptSvc: ReportService,
    private recSvc: ReceivingService,
    private empSvc: EmployeeService,
    private useSvc: UserService
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
      if(rs.result == ResultCode.Success) {
        this.employeeses = rs.data;
      }
    });
  }

  loadDatasource(callback: () => void = null) {
    if(this.collectionId == null || this.collectionId == 0) {
      this.showError("Common.SearchConditionError");
    }

    this.rptSvc.getHistoriesByEmployee(this.from, this.to, this.collectionId).subscribe((result) => {
      if (result.result == ResultCode.Success) {

        this.dataSource = new ArrayStore({
          key: "employeeId",
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
