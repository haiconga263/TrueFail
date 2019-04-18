import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { ShippingService, CFShipping, CFShippingItem } from './shipping.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { Employee, EmployeeService } from '../common/services/employee.service';
import { User, UserService } from '../common/services/user.service';
import { DxDataGridComponent } from 'devextreme-angular/ui/data-grid';
import { Vehicle, VehicleService } from '../common/services/vehicle.service';
import { LocationService } from '../common/services/location.service';
import { ReceivingService, Collection } from '../receiving/receiving.service';
import { FulfillmentService, Fulfillment } from '../common/services/fulfillment.service';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { InventoryService, Inventory } from '../inventory/inventory.service';
import { Product, ProductService } from '../common/services/product.service';
import { UoM, UoMService } from '../common/services/uom.service';
import { AppConsts } from 'src/core/constant/AppConsts';

@Component({
  selector: 'shipping',
  templateUrl: './shipping.component.html',
  styleUrls: ['./shipping.component.css']
})
export class ShippingComponent extends AppBaseComponent implements OnInit {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  selectedRows: number[];

  private collections: Collection[] = [];
  private chooseCollection: Collection;

  private shippings: CFShipping[] = [];
  private users: User[] = [];
  private employees: Employee[] = [];
  private vehicles: Vehicle[] = [];
  private fulfillments: Fulfillment[] = [];
  private statuses: any[] = [];

  private deliveryMans: Employee[] = [];
  private inventories: Inventory[] = [];

  private products: Product[] = [];
  private uoms: UoM[] = [];

  private now = new Date(Date.now());

  private numberPattern = AppConsts.numberPattern;

  constructor(
    injector: Injector,
    private shipSvc: ShippingService,
    private userSvc: UserService,
    private empSvc: EmployeeService,
    private vehiSvc: VehicleService,
    private colSvc: ReceivingService,
    private fulSvc: FulfillmentService,
    private invSvc: InventoryService,
    private proSvc: ProductService,
    private uomSvc: UoMService
  ) {
    super(injector);

    this.loadSource();
  }

  private async loadSource() {

    let rs = await this.userSvc.getUsers(2).toPromise();
    if (rs.result == ResultCode.Success) {
      this.users = rs.data;
    }

    this.empSvc.gets().subscribe(rs => {
      if (rs.result == ResultCode.Success) {
        this.employees = rs.data;
        if (this.users != null) {
          this.deliveryMans = this.employees.filter(e => this.users.find(u => u.id == e.userId && u.roles.find(r => r.name == "Collector") != null) != null);
        }
      }
    });

    this.vehiSvc.gets().subscribe(rs => {
      if (rs.result == ResultCode.Success) {
        this.vehicles = rs.data;
      }
    });

    this.fulSvc.gets().subscribe(rs => {
      if (rs.result == ResultCode.Success) {
        this.fulfillments = rs.data;
      }
    });

    this.proSvc.getsOnly().subscribe(rs => {
      if (rs.result == ResultCode.Success) {
        this.products = rs.data;
      }
    });

    this.uomSvc.gets().subscribe(rs => {
      if (rs.result == ResultCode.Success) {
        this.uoms = rs.data;
      }
    });

    this.statuses = this.shipSvc.getStatuses();

    this.loadMainSource();
  }

  private loadMainSource() {
    this.colSvc.gets().subscribe(disRs => {
      if (disRs.result == ResultCode.Success) {
        this.collections = disRs.data;
      }
    });
  }

  private loadShippingSource(collectionId: number) {
    this.shippings = null;
    this.shipSvc.gets(collectionId).subscribe(rs => {
      if(rs.result == ResultCode.Success) {
        this.shippings = rs.data;
        this.dataGrid.instance.collapseAll(-1);
        this.dataGrid.instance.refresh();
      }
      else {
        this.showError(rs.errorMessage);
      }
    }); 
  }

  private loadInventorySource(collectionId: number){
    this.inventories = null;
    this.invSvc.gets(collectionId).subscribe(rs => {
      if(rs.result == ResultCode.Success) {
        this.inventories = rs.data;
      }
      else {
        this.showError(rs.errorMessage);
      }
    }); 
  }

  private loadShippingItems(shippingId: number) {
    let shipping = this.shippings.find(t => t.id == shippingId);
    if (shipping.items == null) {
      this.shipSvc.getItems(shipping.id).subscribe(async rs => {
        if (rs.result == ResultCode.Success) {
          shipping.items = rs.data;
        }
      });
    }
  }

  private collectionChanged(event) {
    if(event.value == null) {
      return;
    }
    this.loadShippingSource(event.value.id); 
    this.loadInventorySource(event.value.id);
  }

  private refresh() {
    this.loadMainSource();
  }

  create() {
    this.dataGrid.instance.addRow();
  }

  update() {
    if (this.selectedRows != null && this.selectedRows.length == 1) {
      var index = this.dataGrid.instance.getRowIndexByKey(this.selectedRows[0]);
      console.log(index);
      this.dataGrid.instance.editRow(this.dataGrid.instance.getRowIndexByKey(this.selectedRows[0]));
    }
  }


  private async confirm() {
    if (this.selectedRows != null && this.selectedRows.length == 1) {
      let shipping = this.shippings.find(t => t.id == this.selectedRows[0]);
      if (shipping != null) {
        if(shipping.shipperId == null) {
          this.showError("Distribution.Trip.RequiredDeliveryMan");
          return;
        }
        let rs = await this.shipSvc.changeStatus(this.selectedRows[0], 2).toPromise();
        if (rs.result == ResultCode.Success) {
          this.showSuccess('Common.UpdateSuccess');
          shipping.statusId = 2;
          this.loadInventorySource(this.chooseCollection.id);
        }
        else {
          this.showError(rs.errorMessage);
        }
      }
    }
  }

  private async onTriped() {
    if (this.selectedRows != null && this.selectedRows.length == 1) {
      let shipping = this.shippings.find(t => t.id == this.selectedRows[0]);
      if (shipping != null) {
        if(shipping.statusId != 2) {
          this.showError("Trip.WrongStep");
          return;
        }
        let rs = await this.shipSvc.changeStatus(this.selectedRows[0], 3).toPromise();
        if (rs.result == ResultCode.Success) {
          this.showSuccess('Common.UpdateSuccess');
          shipping.statusId = 3;
        }
        else {
          this.showError(rs.errorMessage);
        }
      }
    }
  }

  private async canceled() {
    if (this.selectedRows != null && this.selectedRows.length == 1) {
      let shipping = this.shippings.find(t => t.id == this.selectedRows[0]);
      if (shipping != null) {
        if(shipping.statusId == 1) {
          this.showError("Trip.WrongStep");
          return;
        }
        let rs = await this.shipSvc.changeStatus(this.selectedRows[0], -1).toPromise();
        if (rs.result == ResultCode.Success) {
          this.showSuccess('Common.UpdateSuccess');
          shipping.statusId = -1;
        }
        else {
          this.showError(rs.errorMessage);
        }
      }
    }
  }

  async delete() {
    let rs = await this.shipSvc.delete(this.selectedRows[0]).toPromise();
      if (rs.result == ResultCode.Success) {
        this.showSuccess('Common.DeleteSuccess');
        this.shippings = FuncHelper.removeItemInArray(this.shippings, "id", this.selectedRows[0]);
        this.selectedRows = null;
        this.dataGrid.instance.refresh();
      }
      else {
        this.showError(rs.errorMessage);
      }
  }

  async shippingUpdating(event) {
    event.cancel = true;
    let model = event['oldData'];
    for (var k in event.newData) {
      model[k] = event.newData[k];
    }
    let rs = await this.shipSvc.update(model).toPromise();
    if (rs.result == ResultCode.Success) {
      //alert
      event.cancel = false;
      this.showSuccess('Common.UpdateSuccess');
      this.loadShippingSource(this.chooseCollection.id); 
    }
    else {
      this.showError(rs.errorMessage);
    }
  }

  async shippingAdding(event) {
    event.cancel = true;
    console.log(event);
    let model: CFShipping = new CFShipping();
    model.id = 0;
    model.collectionId = this.chooseCollection.id;
    model.statusId = 0;
    for (var k in event.data) {
      model[k] = event.data[k];
    }
    let rs = await this.shipSvc.add(model).toPromise();
    if (rs.result == ResultCode.Success) {
      this.showSuccess('Common.AddSuccess');
      this.loadShippingSource(this.chooseCollection.id); 
    }
    else {
      this.showError(rs.errorMessage);
    }
  }

  private async onShippingExpanding(event) {
    this.loadShippingItems(event.key);
  }

  private getStatus(id: number) {
    return this.statuses.find(s => s.id == id);
  }

  private getShipping(id: number) {
    if(this.shippings == null) {
      return null;
    }
    return this.shippings.find(t => t.id == id);
  }

  private changeItem(shipping: CFShipping) {
    console.log(shipping);

    this.shipSvc.updateItems(shipping.id, shipping.items).subscribe(rs => {
      if(rs.result == ResultCode.Success) {
        this.showSuccess("Common.UpdateSuccess");
        this.loadShippingItems(shipping.id);
      }
      else {
        this.showError(rs.errorMessage);
      }
    });
  }

  private setInventoryCellValue(newData: any, value: any, currentRowData: any) {
    let column = this as any;
    let component = column.editorOptions.tempData as ShippingComponent;
    let inventory = component.inventories.find(i => i.traceCode == value);
    newData.productId = inventory.productId;
    newData.uoMId = inventory.productId;

    if(currentRowData.shippedQuantity != null && currentRowData.shippedQuantity > inventory.quantity) {
      newData.shippedQuantity = inventory.quantity;
    }
    column.defaultSetCellValue(newData, value);
  }

  private setQuantityCellValue(newData: any, value: any, currentRowData: any) {
    let column = this as any;
    if(currentRowData.traceCode != null) {
      let inventories = column.editorOptions.tempData as Inventory[];
      let inventory = inventories.find(i => i.traceCode == currentRowData.traceCode);
      if(value > inventory.quantity) {
        value = inventory.quantity;
      }
    }
    column.defaultSetCellValue(newData, value);
  }

  private traceCodeDisplayExpr(item: Inventory) {
    return `${item.traceCode} - ${item.quantity}`;
  }

  private shippingItemUpdated(event: any, items: CFShippingItem[], datagrid: DxDataGridComponent) {
    console.log(event);
    let checkingItem = items.find(i => i.traceCode == event.key.traceCode && i.__KEY__ != event.key.__KEY__);
    if(checkingItem != null) {
      checkingItem.shippedQuantity += event.key.shippedQuantity;
      let inventory = this.inventories.find(i => i.traceCode == event.key.traceCode);
      if(inventory.quantity < checkingItem.shippedQuantity) {
        checkingItem.shippedQuantity = inventory.quantity;
      }
      items = FuncHelper.removeItemInArray(items, "__KEY__" , event.key.__KEY__);
      datagrid.dataSource = items;
      event.component.refresh();
    }
  }

  private shippingItemAdding(event: any) {
    var currentItems = event.component.getDataSource()._items as CFShippingItem[];
    let checkingItem = currentItems.find(i => i.traceCode == event.data.traceCode);
    if(checkingItem != null) {
      checkingItem.shippedQuantity += event.data.shippedQuantity;
      let inventory = this.inventories.find(i => i.traceCode == event.data.traceCode);
      if(inventory.quantity < checkingItem.shippedQuantity) {
        checkingItem.shippedQuantity = inventory.quantity;
      }
      event.cancel = true;
      event.component.refresh();
    }
  }
}
