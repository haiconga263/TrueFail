import { Component, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { FarmerPlanningService, FarmerPlanning } from '../../farmer-planning.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';
import { appUrl } from 'src/aritnt/administrator/app-url';

@Component({
  selector: 'planning',
  templateUrl: './planning.component.html',
  styleUrls: ['./planning.component.css']
})
export class PlanningComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource: ArrayStore;
  selectedRows: number[];

  private choosePlanning : FarmerPlanning = new FarmerPlanning();

  private isPopupVisible:  boolean = false;
  private isCantUpdate: boolean = true;

  constructor(
    injector: Injector,
    private planSvc: FarmerPlanningService
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
    showConvert: (planningId: number) => {
      this.router.navigate([appUrl.farmerOrderDetail],
        {
          queryParams: {
            type: 'add',
            planningId: planningId,
            returnUrl: appUrl.farmerPlanning
          }
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
