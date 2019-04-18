import { Component, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { RetailerOrderService, RetailerOrder, RetailerOrderItem, RetailerOrderProcessing, RetailerOrderItemProcessing, RetailerOrderPlanningItemProcessing } from '../../retailer-order.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';
import { RetailerOrderStatus } from '../../retailer-order.service';
import { FarmerProduct, InventoryService, VirtualIntProduct } from 'src/aritnt/administrator/common/services/inventory.service';
import { DataLayerManager } from '@agm/core';
import { appUrl } from 'src/aritnt/administrator/app-url';

@Component({
  selector: 'order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent extends AppBaseComponent {
  private orders: RetailerOrder[] = [];
  selectedRows: number[];
  selectedOrderRows: RetailerOrderItem[];
  private isPopupVisible: boolean = false;
  private isCantProcess: boolean = true;
  private choosingOrder: RetailerOrder = new RetailerOrder();

  private from: Date = null;
  private to: Date = null;

  private statuses: RetailerOrderStatus[] = [];

  private virtInvsWithCurrentOrder: VirtualIntProduct[] = [];
  private currentItemIntProduct: FarmerProduct[] = [];

  constructor(
    injector: Injector,
    private orderSvc: RetailerOrderService,
    private inventorySvc: InventoryService
  ) {
    super(injector);

    this.from = new Date(Date.now());
    this.to = new Date(Date.now());

    this.orderSvc.getStatuses().subscribe(rs => {
      if(rs.result == ResultCode.Success)
      {
        this.statuses = rs.data;
      }
    });

    this.loadDatasource();
  }

  loadDatasource(callback: () => void = null) {
    this.orderSvc.getsUncompleted().subscribe((result) => {
      if (result.result == ResultCode.Success) {

        this.orders = result.data;

        if (FuncHelper.isFunction(callback))
          callback();
      }
    });
  }

  private refresh()
  {
    console.log(this.from);
    console.log(this.to);

    this.loadDatasource();
  }

  private infor(orderId: number) {
    alert("Chức năng chưa hoàn thiện");
  }

  private async comfirmed(orderId: number) {
    if(!this.checkCantComfirm(orderId)) {
      let rs = await this.orderSvc.updateStatus(orderId, 2).toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.setStatus(orderId, 2);
        this.showSuccess("Order.Retailer.ComfirmSuccess");
      }
      else
      {
        this.showError(rs.errorMessage);
        //alert
      }
    }
  }

  private add() {
    this.router.navigate([appUrl.retailerOrderDetail]);
  }

  private async showProcessing(orderId: number)
  {
    if(!this.checkCantProcess(orderId)) {
      this.isPopupVisible = true;
      this.choosingOrder = FuncHelper.clone(this.orders.find(o => o.id == orderId)) as RetailerOrder;

      // clear inventory
      this.virtInvsWithCurrentOrder = [];

      this.choosingOrder.items.forEach(item => {
        item.isAdaped = item.adapQuantity >= item.orderedQuantity;
        if(item.planningItems == null) {
          item.planningItems = [];
        }

        // get virtual inventory with this order
        this.inventorySvc.get(item.productId, this.choosingOrder.buyingDate).subscribe(result =>{
          if(result.result == ResultCode.Success) {
            if(result.data.length > 0) {
              this.virtInvsWithCurrentOrder.push({
                productId: item.productId,
                product: result.data[0].product,
                farmerProducts: result.data
              });; 
            }
          }
        });
      });
    }
  }

  private async processing() {
    // Check 
    let notAdapedItems = this.choosingOrder.items.filter(i => !i.isAdaped);
    notAdapedItems.forEach(item => {
      let stockQuantity = this.currentItemIntProduct
                              .filter(p => p.productId == item.productId && p.uoMId == item.uoMId && p.quantity > 0);
      if(stockQuantity.length > 0){
        //alert question
        return;
      }
    });

    let outOfOrderedItems = this.choosingOrder.items.filter(i => i.orderedQuantity < i.adapQuantity);
    if(outOfOrderedItems.length > 0) {
      //alert question
      return;
    }

    let processing = new RetailerOrderProcessing();
    processing.orderId = this.choosingOrder.id;
    this.choosingOrder.items.forEach(item => {
      let orderItem = new RetailerOrderItemProcessing();
      orderItem.orderItemId = item.id;
      item.planningItems.forEach(planning => {
        let planningItem = new RetailerOrderPlanningItemProcessing();
        planningItem.farmerId = planning.farmerId;
        planningItem.quantity = planning.quantity;
        orderItem.plannings.push(planningItem);
      });
      processing.items.push(orderItem);
    });

    let rs = await this.orderSvc.process(processing).toPromise();
    if(rs.result == ResultCode.Success){
      this.showSuccess(this.lang.instant("Order.Convert.SuccessMessage"));
      this.orderSvc.get(this.choosingOrder.id).subscribe(result => {
        if(result.result == ResultCode.Success) {
          this.orders = FuncHelper.removeItemInArray(this.orders, "id", result.data.id);
          this.orders.push(result.data);
        }
      });
      this.isPopupVisible = false;
      this.choosingOrder = new RetailerOrder();
      // clear inventory
      this.virtInvsWithCurrentOrder = [];
    }
    else {
      this.showError(this.lang.instant(rs.errorMessage));
    }
  }

  private selectionChanged(e: any) {
    // Change CurrentIntProduct
    this.currentItemIntProduct = [];
    let item: RetailerOrderItem = e.currentSelectedRowKeys[0] as RetailerOrderItem;
    let virtualInt = this.virtInvsWithCurrentOrder.find(p => p.productId == item.productId);
    if(virtualInt != null) {
      this.currentItemIntProduct = virtualInt.farmerProducts.filter(p => p.uoMId == item.uoMId);
    }


    e.component.collapseAll(-1);
    e.component.expandRow(item);
  }

  private checkCantProcess(orderId: number) {
    let order = this.orders.find(o => o.id == orderId);
    if(order != null && order.statusId != 2){ // Đã comfirmed
      return true;
    }
    return false;
  }

  private checkCantComfirm(orderId: number) {
    let order = this.orders.find(o => o.id == orderId);
    if(order != null && order.statusId != 1){ // Đã đặt hàng
      return true;
    }
    return false;
  }

  private setStatus(orderId: number, statusId: number)
  {
    let order = this.orders.find(o => o.id == orderId);
    if(order != null){
      order.statusId = statusId;
    }
  }

  getInventoryFarmerExpr(item: FarmerProduct) {
    if(!item) {
        return "";
    }

    return item.farmer.name + " - " + item.quantity + " " + item.uoM.name;
  }

  update(data: any){
    console.log('update');
    console.log(data);  

    if(this.selectedOrderRows[0].planningItems.find(i => i.__KEY__ != data.key.__KEY__ && i.farmerId == data.newData.farmerId) != null) {
      this.showError(this.lang.instant("Order.Convert.ExsitedFarmerPlanningProduct"));
      data.cancel = true;
      return;
    }

    if(data.newData.farmerId != null) {
      // rollback quantity to old farmer inventory
      let oldfarmerProduct = this.currentItemIntProduct.find(p => p.farmerId == data.oldData.farmerId);
      oldfarmerProduct.quantity += data.oldData.quantity;

      let farmerProduct = this.currentItemIntProduct.find(p => p.farmerId == data.newData.farmerId);
      if(data.newData.quantity != null) {
        if(data.newData.quantity > farmerProduct.quantity) {
          this.showError(this.lang.instant("Order.Convert.OutOfInventoryMessage"));
          data.cancel = true;
          return;
        }

        if(this.selectedOrderRows[0].adapQuantity - data.oldData.quantity + data.newData.quantity > this.selectedOrderRows[0].orderedQuantity) {
          this.showError((this.lang.instant("Order.Convert.OutOfOrderedQuantity") as string).replace("{0}", (this.selectedOrderRows[0].orderedQuantity - (this.selectedOrderRows[0].adapQuantity - data.oldData.quantity)).toString()));
          data.cancel = true;
          return;
        }

        this.selectedOrderRows[0].adapQuantity = this.selectedOrderRows[0].adapQuantity - data.oldData.quantity + data.newData.quantity;
        farmerProduct.quantity -= data.newData.quantity;
      }
      else {
        if(data.oldData.quantity > farmerProduct.quantity) {
          this.showError(this.lang.instant("Order.Convert.OutOfInventoryMessage"));
          data.cancel = true;
          return;
        }
        farmerProduct.quantity -= data.oldData.quantity;
      }
    }
    else
    {
      if(data.newData.quantity != null) {
        let farmerProduct = this.currentItemIntProduct.find(p => p.farmerId == data.oldData.farmerId);
        if(data.newData.quantity - data.oldData.quantity > farmerProduct.quantity) {
          this.showError(this.lang.instant("Order.Convert.OutOfInventoryMessage"));
          data.cancel = true;
          return;
        }

        // if(this.selectedOrderRows[0].adapQuantity - data.oldData.quantity + data.newData.quantity > this.selectedOrderRows[0].orderedQuantity) {
        //   this.showError((this.lang.instant("Order.Convert.OutOfOrderedQuantity") as string).replace("{0}", (this.selectedOrderRows[0].orderedQuantity - (this.selectedOrderRows[0].adapQuantity - data.oldData.quantity)).toString()));
        //   data.cancel = true;
        //   return;
        // }

        this.selectedOrderRows[0].adapQuantity = this.selectedOrderRows[0].adapQuantity - data.oldData.quantity + data.newData.quantity;
        farmerProduct.quantity -= (data.newData.quantity - data.oldData.quantity);
      }
      else
      {
        return;
      }
    }

    if(this.selectedOrderRows[0].adapQuantity >= this.selectedOrderRows[0].orderedQuantity) {
      this.selectedOrderRows[0].isAdaped = true;
    }
    else this.selectedOrderRows[0].isAdaped = false;
  }
  create(data: any){
    console.log('create');  
    console.log(data);  

    if(this.selectedOrderRows[0].planningItems.find(i => i.farmerId == data.data.farmerId) != null) {
      this.showError(this.lang.instant("Order.Convert.ExsitedFarmerPlanningProduct"));
      data.cancel = true;
      return;
    }

    let farmerProduct = this.currentItemIntProduct.find(p => p.farmerId == data.data.farmerId);
    if(data.data.quantity > farmerProduct.quantity) {
      this.showError(this.lang.instant("Order.Convert.OutOfInventoryMessage"));
      data.cancel = true;
      return;
    }

    // if(this.selectedOrderRows[0].adapQuantity + data.data.quantity > this.selectedOrderRows[0].orderedQuantity) {
    //   this.showError((this.lang.instant("Order.Convert.OutOfOrderedQuantity") as string).replace("{0}", (this.selectedOrderRows[0].orderedQuantity - this.selectedOrderRows[0].adapQuantity).toString()));
    //   data.cancel = true;
    //   return;
    // }

    this.selectedOrderRows[0].adapQuantity = this.selectedOrderRows[0].adapQuantity + data.data.quantity;
    if(this.selectedOrderRows[0].adapQuantity >= this.selectedOrderRows[0].orderedQuantity) {
      this.selectedOrderRows[0].isAdaped = true;
    }
    else this.selectedOrderRows[0].isAdaped = false;
    
    //update current inventory of farmer
    farmerProduct.quantity -= data.data.quantity;
  }

  delete(data: any){
    console.log('delete');
    console.log(data);  

    let farmerProduct = this.currentItemIntProduct.find(p => p.farmerId == data.data.farmerId);
    farmerProduct.quantity += data.data.quantity;

    this.selectedOrderRows[0].adapQuantity = this.selectedOrderRows[0].adapQuantity - data.data.quantity;
    if(this.selectedOrderRows[0].adapQuantity >= this.selectedOrderRows[0].orderedQuantity) {
      this.selectedOrderRows[0].isAdaped = true;
    }
    else this.selectedOrderRows[0].isAdaped = false;
    data.component.refresh();
  }
}