import { Component, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';
import { FarmerProduct, InventoryService, VirtualIntProduct } from 'src/aritnt/administrator/common/services/inventory.service';
import { DataLayerManager } from '@agm/core';

import { OrderService } from 'src/aritnt/retailer/order/order.service';
import { RetailerOrder, RetailerOrderProcessing, RetailerOrderItemProcessing, RetailerOrderPlanningItemProcessing } from 'src/aritnt/administrator/retailer/retailer-order.service';
import { FulfillmentService, Fulfillment, FulfillmentCollection, Pack } from '../../fulfillment.service';
import { appUrl } from '../../app-url';


@Component({
  selector: 'order',
  templateUrl: './pack-master.component.html',
  styleUrls: ['./pack-master.component.css']
})

export class PackMasterComponent extends AppBaseComponent {

  packs: Pack[] = [];
  private fulfillments: Fulfillment[] = [];
  selectedRows: FulfillmentCollection = new FulfillmentCollection();
  private fulfillment: Fulfillment = new Fulfillment();
  private from: Date = null;
  private to: Date = null;
  private fulfillmentId: number = 0;
  constructor(
    injector: Injector,
    private fufilmentSvc: FulfillmentService,
  ) {
    super(injector);

    this.from = new Date(Date.now());
    this.to = new Date(Date.now());

    this.fufilmentSvc.getFulfillment().subscribe(rs => {
      if (rs.result == ResultCode.Success) {
        this.fulfillments = rs.data;
        this.fulfillment = this.fulfillments[0];
      }
    });
    // console.log(this.fulfillments[0].id);
    // this.loadByCollectionSource(this.fulfillments[0].id);
    this.loadDatasource();
  }

  loadDatasource(callback: () => void = null) {

    
  }
  private save() {
    this.router.navigate([appUrl.fcOrderDetail],
      {
        queryParams: {
          type: 'update',
          id: this.selectedRows.id
        }
      });    
  }
  private refresh() {
    console.log(this.from);
    console.log(this.to);

    this.loadDatasource();
  }

  private fulfillmentChanged(event: any) {
    this.loadByCollectionSource(event.value);
  }


  private infor(orderId: number) {
    alert("Chức năng chưa hoàn thiện");
  }
  private onSelectionChanged(event: any){
    if(event.selectedRowsData != null && event.selectedRowsData.length == 1){      
      this.selectedRows = event.selectedRowsData[0];       
    }    
  }
  private loadByCollectionSource(id: number) {
    this.fufilmentSvc.getRetailerOrder(id).subscribe(result => {      
      if (result.result == ResultCode.Success) {
        this.packs = result.data;
      }
    });
  }
}
