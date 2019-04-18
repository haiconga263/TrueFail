import { Component, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';
// import { RetailerOrderStatus } from '../../retailer-order.service';
import { FarmerProduct, InventoryService, VirtualIntProduct } from 'src/aritnt/administrator/common/services/inventory.service';
import { DataLayerManager } from '@agm/core';
import { OrderService } from 'src/aritnt/retailer/order/order.service';

import { RetailerOrder, RetailerOrderProcessing, RetailerOrderItemProcessing, RetailerOrderPlanningItemProcessing } from 'src/aritnt/administrator/retailer/retailer-order.service';
import { FulfillmentService, Collection, FulfillmentCollection, Status } from '../../fulfillment.service';
import { Fulfillment } from 'src/aritnt/administrator/fulfillment/fulfillment.service';
import { Product, ProductService } from 'src/aritnt/administrator/product/product.service';
import { UoM, UoMService } from 'src/aritnt/administrator/common/services/uom.service';
import { ResultModel } from 'src/core/models/http.model';
import { appUrl } from '../../app-url';
import { ConstantPool } from '@angular/compiler/src/constant_pool';

@Component({
  selector: 'order',
  templateUrl: './fc-master.component.html',
  styleUrls: ['./fc-master.component.css']
})

export class FCMasterComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;

  fcModel: FulfillmentCollection[] = [];
  collections: Collection[] = [];
  fulfillments: Fulfillment[] = [];
  statusModify: Status[] = [];
  statusFulCol: Status[] = [];
  _status: Status;
  selectedRows: FulfillmentCollection;
  chooseCollection: Fulfillment = new Fulfillment();

  selCol: Collection[] = [];
  selFul: Fulfillment[] = [];
  private products: Product[] = [];
  private uoms: UoM[] = [];

  private from: Date = null;
  private to: Date = null;
  private fulCol: FulfillmentCollection = new FulfillmentCollection();
  private statusSelectedId: number = 0;

  constructor(
    injector: Injector,
    private orderSvc: OrderService,
    private inventorySvc: InventoryService,
    private fcservice: FulfillmentService,
    private proSvc: ProductService,
    private uomSvc: UoMService,
  ) {
    super(injector);

    this.from = new Date(Date.now());
    this.to = new Date(Date.now());
    this._status = new Status({
      id: 1,
      description: 'OK',
      name: 'OK'
    });

    this.statusModify.push(this._status);
    this._status = new Status({
      id: 2,
      description: 'NOT OK',
      name: 'NOT OK'
    });
    this.statusModify.push(this._status);

    this.loadDatasource();

    this.selectedRows = new FulfillmentCollection();
  }


  loadDatasource(callback: () => void = null) {
    this.fcservice.getFulfillmentCollection().subscribe((result) => {
      if (result.result == ResultCode.Success) {
        this.fcModel = result.data;      
      }

      if (FuncHelper.isFunction(callback))
        callback();
    });

    this.fcservice.getCollection().subscribe((result) => {
      if (result.result == ResultCode.Success) {
        this.collections = result.data;
      }

      if (FuncHelper.isFunction(callback))
        callback();

    });


    this.proSvc.getsForOrder().subscribe((result) => {
      if (result.result == ResultCode.Success) {
        this.products = result.data;
      }
    });

    this.uomSvc.gets().subscribe((result) => {
      if (result.result == ResultCode.Success) {
        this.uoms = result.data;
      }
    });

    this.fcservice.getFulfillment().subscribe((result) => {
      if (result.result == ResultCode.Success) {
        this.fulfillments = result.data;
      }

      if (FuncHelper.isFunction(callback))
        callback();
    });
  }
  grid = {
    refresh: () => {
      this.loadDatasource(() => {
        this.dataGrid.instance.refresh();
      });
    }
  }
  private loadByCollectionSource(collectionId: number) {
    this.fcservice.getFulfillmentCollectionById(collectionId).subscribe(result => {
      if (result.result == ResultCode.Success) {
        this.fcModel = result.data;
      }
    });
  }

  private collectionChanged(event: any) {
    this.loadByCollectionSource(event.value);   
  }

  private ImportWarehouse() {
    debugger
    this.statusModify;
    
    debugger
    var c =  FuncHelper.syncData(this.selectedRows, this.fulCol)
    var st = this.statusModify.find(x => x.id == this.statusSelectedId);
    this.fulCol.statusFulCols =[];
    this.fulCol.statusFulCols.push(st);
    this.fcservice.add(c).subscribe((result: ResultModel<any>) => {
      
      debugger
      if (result.result == ResultCode.Success) {
        //alert
        this.showSuccess(this.lang.instant('Common.AddSuccess'));
      }
      else {
        //alert
        this.showError(result.errorMessage);
      }
    });
    this.router.navigate([appUrl.fulfillmentColDetail],
      {
        queryParams: {
          type: 'get',
          id: this.selectedRows.code
        }
      });
  }
  private onSelectionChanged(event: any) {
    if (event.selectedRowsData != null && event.selectedRowsData.length == 1) {
      this.selectedRows = event.selectedRowsData[0];
      if (this.selCol != null) {
        this.selCol = [];
      }
      if (this.selFul != null) {
        this.selFul = [];
      }
      console.log(this.selectedRows);
      this.selCol.push(this.selectedRows.collection);
      this.selFul.push(this.selectedRows.fulfillment);
    }
  }
  private statusChanged(event: any) {
    console.log(event);
    this.statusSelectedId = event.value;
  }
  private mapFulfillmentCollection(fulCol: FulfillmentCollection) {
    fulCol.code = this.selectedRows.code;
    fulCol.collectionId = this.selectedRows.collectionId;

  }

  private refresh() {
    console.log(this.from);
    console.log(this.to);

    this.loadDatasource();
  }

  private infor(orderId: number) {
    alert("Chức năng chưa hoàn thiện");
  }
  private save() {
    this.fcservice.add(this.fulCol).subscribe((result: ResultModel<any>) => {
      debugger
      if (result.result == ResultCode.Success) {
        //alert
        this.showSuccess(this.lang.instant('Common.AddSuccess'));
      }
      else {
        //alert
        this.showError(result.errorMessage);
      }
    });
  }

}
