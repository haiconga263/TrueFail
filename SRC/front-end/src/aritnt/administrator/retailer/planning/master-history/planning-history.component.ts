import { Component, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { RetailerPlanningService, RetailerPlanning } from '../../retailer-planning.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';
import { from } from 'rxjs';

@Component({
  selector: 'planning-history',
  templateUrl: './planning-history.component.html',
  styleUrls: ['./planning-history.component.css']
})
export class PlanningHistoryComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource: ArrayStore;
  selectedRows: number[];

  private from: Date = null;
  private to: Date = null;

  constructor(
    injector: Injector,
    private planSvc: RetailerPlanningService
  ) {
    super(injector);

    this.from = new Date(Date.now());
    this.to = new Date(Date.now());

  }

  loadDatasource(callback: () => void = null) {
    this.planSvc.getsCompleted(this.from, this.to).subscribe((result) => {
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
