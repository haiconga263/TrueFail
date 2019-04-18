import { Component, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { PlanningService, Planning } from '../planning.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';
import { appUrl } from '../../app-url';

@Component({
  selector: 'planning',
  templateUrl: './planning.component.html',
  styleUrls: ['./planning.component.css']
})
export class PlanningComponent extends AppBaseComponent {
  selectedRows: Planning[] = [];

  private plannings: Planning[] = [];

  private from: Date = null;
  private to: Date = null;

  private statuses: any[] = [];
  private status: number = 0;

  constructor(
    injector: Injector,
    private planSvc: PlanningService
  ) {
    super(injector);

    this.from = new Date(Date.now());
    this.to = new Date(Date.now());

    this.statuses = this.planSvc.getStatuses();
    this.loadDatasource();
  }

  loadDatasource(callback: () => void = null) {
    this.planSvc.gets(this.from, this.to, this.status, this.authenticSvc.getSession().id).subscribe((result) => {
      if (result.result == ResultCode.Success) {

        this.plannings = result.data;

        if (FuncHelper.isFunction(callback))
          callback();
      }
    });
  }

  private search()
  {
    console.log(this.from);
    console.log(this.to);

    this.loadDatasource();
  }

  private create() {
    console.log('create');
    this.router.navigate([appUrl.planningDetail],
    {
      queryParams: {
        type: 'add'
      }
    });
  }

  private update() {
    console.log('update');
    if(this.selectedRows.length == 1)
    {
      this.router.navigate([appUrl.planningDetail],
      {
        queryParams: {
          type: 'update',
          id: this.selectedRows[0].id
        }
      });
    }
  }

  private delete() {
    console.log('delete');
    if(this.selectedRows.length == 1)
    {
      this.planSvc.delete(this.selectedRows[0].id).subscribe(rs => {
        if(rs.result == ResultCode.Success)
        {
          this.showSuccess("Common.DeleteSuccess");
          this.search();
        }
        else
        {
          this.showError(rs.errorMessage);
        }
      });
    }
  }

  private isCantAction() {
    if(this.selectedRows.length != 1 || this.selectedRows[0].isOrdered || this.selectedRows[0].isExpired)
    {
      return true;
    }
    return false;
  }
}
