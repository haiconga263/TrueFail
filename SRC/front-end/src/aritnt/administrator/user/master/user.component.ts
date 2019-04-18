import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { UserService } from '../user.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';
import { appUrl } from '../../app-url';

@Component({
  selector: 'user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource: ArrayStore;
  selectedRows: number[];

  constructor(
    injector: Injector,
    private userSvc: UserService
  ) {
    super(injector);
    this.loadDatasource();
  }

  loadDatasource(callback: () => void = null) {
    this.userSvc.getUsers().subscribe((result) => {
      if (result.result == ResultCode.Success) {
        let ary = result.data;
        for (var i = 0; i < ary.length; i++) {
          ary[i].password = '';
          ary[i].comfirmPassword = '';
        }

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
    delete: () => {
      console.log('delete');
      console.log(this.dataSource);
      if(this.selectedRows.length == 1) {
        this.userSvc.removeUser(this.selectedRows[0]).subscribe((result) => {
          if(result.result == ResultCode.Success)
          {
            //alert
            this.showSuccess(this.lang.instant('Common.DeleteSuccess'));
            this.grid.refresh();
          }
          else
          {
            //alert
            this.showError(this.lang.instant(result.errorMessage));
          }
        });
      }
    },
    refresh: () => {
      this.loadDatasource(() => {
        this.dataGrid.instance.refresh();
      });
    },
    update: () => {
      if(this.selectedRows.length == 1)
      {
        this.router.navigate([appUrl.userDetail],
        {
          queryParams: {
            type: 'update',
            id: this.selectedRows[0]
          }
        });
      }
    },
    create: () => {
      console.log('create');
      this.router.navigate([appUrl.userDetail],
        {
          queryParams: {
            type: 'add'
          }
        });
    },
    resetPassword: async () => {
      if(this.selectedRows.length == 1)
      {
        var rs = await this.userSvc.resetPassword(this.selectedRows[0]).toPromise();
        if(rs.result == ResultCode.Success) {
          this.showSuccess(this.lang.instant('Common.UpdateSuccess'));
        }
        else {
          this.showError(rs.errorMessage);
        }
      }
    }
  };



  ngOnInit() {
  }
}
