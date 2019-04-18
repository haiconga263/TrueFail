import { Component, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { RetailerPlanningService, RetailerPlanning } from '../../retailer-planning.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';

@Component({
  selector: 'planning',
  templateUrl: './planning.component.html',
  styleUrls: ['./planning.component.css']
})
export class PlanningComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource: ArrayStore;
  selectedRows: number[];

  private choosePlanning : RetailerPlanning = new RetailerPlanning();

  private isPopupVisible:  boolean = false;
  private isCantUpdate: boolean = true;

  constructor(
    injector: Injector,
    private planSvc: RetailerPlanningService
  ) {
    super(injector);
    this.loadDatasource();
  }

  loadDatasource(callback: () => void = null) {
    this.planSvc.getsUncompleted().subscribe((result) => {
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

  grid = {
    refresh: () => {
      this.loadDatasource(() => {
        this.dataGrid.instance.refresh();
      });
    },
    showUpdate: (planningId: number) => {
      console.log('show update: ' + planningId);
      this.dataSource.byKey(planningId).then((item) => {
        this.choosePlanning = JSON.parse(JSON.stringify(item));
        this.isPopupVisible = true;
        this.isCantUpdate = false;
      });
    },
    setAdap: async () => {
      console.log('setAdap');
      this.isCantUpdate = true;

      let rs = await this.planSvc.updateAdap(this.choosePlanning).toPromise();
      if(rs.result == ResultCode.Success)
      {
        //alert
        this.isPopupVisible = false;
        this.grid.refresh();
      }
      else
      {
        //alert
      }
      this.isCantUpdate = false;
    }
  };



  ngOnInit() {
  }
}
