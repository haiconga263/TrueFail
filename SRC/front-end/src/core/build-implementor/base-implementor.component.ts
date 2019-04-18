import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import CustomStore from 'devextreme/data/custom_store';
import { load } from '@angular/core/src/render3/instructions';
import { ResultCode } from 'src/core/constant/AppEnums';
import ArrayStore from 'devextreme/data/array_store'
import { DxDataGridComponent } from 'devextreme-angular';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { ImplementorService, HttpConfig } from './implementor.service';
import DataGrid from "devextreme/ui/data_grid";
import { DxiDataGridColumn } from 'devextreme-angular/ui/nested/base/data-grid-column-dxi';
import { ResultModel } from '../models/http.model';

export class Filter {
  public caption: string = '';
  public dataField: string = '';
  public data: any[] = [];
  public selected: any = null;

  public constructor(init?: Partial<Filter>) {
    Object.assign(this, init);
  }
}

export class Action {
  public classButton: string = 'btn-default';
  public isSelected: boolean = false;
  public isOneSelected: boolean = false;
  public click: any;
  public text: string = '';
  public icon: string = '';

  public constructor(init?: Partial<Action>) {
    Object.assign(this, init);
  }
}

export enum BaseImplementorTypes {
  DataGrid,
  TreeView
}

export abstract class BaseImplementorComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource: ArrayStore;
  actions: any;
  selectedRows: number[];
  public type: BaseImplementorTypes = BaseImplementorTypes.DataGrid;
  public service: ImplementorService;

  // Tree View
  parentIdExpr: string = 'parentId';

  filters: Filter[] = [];
  btnActions: Action[] = [];

  columns: any[] = [];
  rowCurrent: any = null;

  getAllHttp: HttpConfig = new HttpConfig({ name: 'all' });
  updateHttp: HttpConfig = new HttpConfig({ name: 'update' });
  deleteHttp: HttpConfig = new HttpConfig({ name: 'delete' });
  insertHttp: HttpConfig = new HttpConfig({ name: 'insert' });

  private urlDetail: string = '';

  constructor(
    injector: Injector
  ) {
    super(injector);
    this.service = injector.get(ImplementorService);

    this.onEditorPreparing = this.onEditorPreparing.bind(this);
    this.onCellClick = this.onCellClick.bind(this);
  }

  onEditorPreparing(data: any) { }

  onCellClick(data: any) {
    this.rowCurrent = data;
  }

  configActions(...btnActions: Action[]) {
    for (var i = 0; i < btnActions.length; i++) {
      this.btnActions.push(btnActions[i]);
    }
  }

  configColumns(...columns: any[]) {
    for (var i = 0; i < columns.length; i++) {
      this.columns.push(columns[i]);
    }
  }

  configFilters(...filters: Filter[]) {
    for (var i = 0; i < filters.length; i++) {
      this.filters.push(filters[i]);
    }
  }

  setUrlApiRoot(urlApi: string) {
    this.service.UrlApi = urlApi;
  }

  setUrlDetail(urlDetail: string) {
    this.urlDetail = urlDetail;
  }


  initialize() {
    this.actions = {
      refresh: () => {
        this.refresh();
      },
      showCreate: () => {
        if (this.urlDetail != '' && this.urlDetail != null)
          this.router.navigate([this.urlDetail], { queryParams: { returnurl: encodeURIComponent(this.router.url) } });
        else this.dataGrid.instance.addRow();
      },
      showUpdate: () => {
        if (this.urlDetail != '' && this.urlDetail != null)
          this.router.navigate([this.urlDetail], { queryParams: { id: this.selectedRows[0], returnurl: encodeURIComponent(this.router.url) } });
        else {
          let rowIdx = this.dataGrid.instance.getRowIndexByKey(this.selectedRows[0]);
          this.dataGrid.instance.editRow(rowIdx);
        }
      },
      delete: () => {
        this.dataSource.remove(this.selectedRows[0]);
      },
    };
    this.refresh();
  }


  async refresh() {
    let params = {};
    for (var i = 0; i < this.filters.length; i++) {
      params[this.filters[i].dataField] = (this.filters[i].selected == null) ? null : this.filters[i].selected.value;
    }

    let rs = await this.service.sendRequest(this.getAllHttp, params);
    if (rs.result != ResultCode.Success) {
      this.alertSvc.alertByHttpResult(rs);
    }

    this.dataSource = new ArrayStore({
      key: 'id',
      data: rs.data,
      onUpdating: (key: any, value: any) => {
        let oldData = this.dataGrid.instance.getSelectedRowsData().find(x => x.id == key);
        let data = FuncHelper.syncData(value, oldData);
        this.service.sendRequest(this.updateHttp, data).then((result) => {
          this.alertSvc.alertByHttpResult(result);
          this.refresh();
        });
      },
      onInserting: (data: any) => {
        this.service.sendRequest(this.insertHttp, data).then((result: ResultModel<any>) => {
          this.alertSvc.alertByHttpResult(result);
          this.refresh();
        });
      },
      onRemoving: (data: any) => {
        this.service.sendRequest(this.deleteHttp, data).then((result: ResultModel<any>) => {
          this.alertSvc.alertByHttpResult(result);
          this.refresh();
        });
      },
    });
  }

  ngOnInit() { }
}
