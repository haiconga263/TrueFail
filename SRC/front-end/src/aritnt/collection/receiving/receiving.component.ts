import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { ReceivingService, Collection, FarmerOrderStatuses, FarmerOrder } from './receiving.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { Employee, EmployeeService } from '../common/services/employee.service';
import { User, UserService } from '../common/services/user.service';
import { DxDataGridComponent } from 'devextreme-angular/ui/data-grid';
import { Vehicle, VehicleService } from '../common/services/vehicle.service';
import { LocationService } from '../common/services/location.service';
import { FuncHelper } from 'src/core/helpers/function-helper';

@Component({
  selector: 'receiving',
  templateUrl: './receiving.component.html',
  styleUrls: ['./receiving.component.css']
})
export class ReceivingComponent extends AppBaseComponent implements OnInit {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  selectedRows: FarmerOrder[];

  private collections: Collection[] = [];
  private chooseCollection: Collection = new Collection();

  private getCurrencyFormat = FuncHelper.getCurrenyFormat;

  constructor(
    injector: Injector,
    private recSvc: ReceivingService
  ) {
    super(injector);

    this.loadCollectionSource();
  }

  private loadCollectionSource() {
    this.recSvc.gets().subscribe(rs => {
      if (rs.result == ResultCode.Success) {
        this.collections = rs.data;
      }
    });
  }

  private loadByCollectionSource(collectionId: number) {
    var collectionFind = this.collections.find(c => c.id == collectionId);
    if(collectionFind != null && collectionFind.items == null) {
      this.recSvc.getsOrder(collectionId).subscribe(orderRs => {
        if(orderRs.result == ResultCode.Success) {
          collectionFind.items = orderRs.data.filter(o => o.statusId == FarmerOrderStatuses.ConfirmedOrder);
        }
      });
    }
  }

  private collectionChanged(event: any) {
    console.log(event);
    this.loadByCollectionSource(event.value.id);
  }

  private refresh() {
    this.loadCollectionSource();
  }

  create() {
    console.log(this.chooseCollection);
  }

  update() {
    if (this.selectedRows != null && this.selectedRows.length == 1) {
      var index = this.dataGrid.instance.getRowIndexByKey(this.selectedRows[0]);
      console.log(index);
      this.dataGrid.instance.editRow(this.dataGrid.instance.getRowIndexByKey(this.selectedRows[0]));
    }
  }

  async cancel() {
    if(this.selectedRows.length == 1) {
      var order = this.selectedRows[0];

      let rs = await this.recSvc.cancel(order).toPromise();
      if(rs.result == ResultCode.Success) {
        let collection = this.collections.find(c => c.id == order.collectionId);
        if(collection != null) {
          collection.items = FuncHelper.removeItemInArray(collection.items, "id", order.id);
        }
        this.showSuccess("Order.Farmer.CanceledSuccess");
      }
      else {
        this.showError(rs.errorMessage);
      }
    }
  }

  private isPopupVisible: boolean = false;
  private processOrder: FarmerOrder = null;
  private showProcess() {
    if(this.selectedRows.length == 1) {
      this.isPopupVisible = true;
      this.processOrder = FuncHelper.clone(this.selectedRows[0]);
      this.processOrder.items.forEach(item => {
        item.totalAmount = 0;
      });
      this.processOrder.currentTotalAmount = 0;
    }
  }

  private updatedItem(event: any) {
    this.processOrder.items.forEach(item => {
      this.processOrder.currentTotalAmount += item.price * item.deliveriedQuantity;
    })
  }
  private async processing() {
    let rs = await this.recSvc.complete(this.processOrder).toPromise();
      if(rs.result == ResultCode.Success) {
        let collection = this.collections.find(c => c.id == this.processOrder.collectionId);
        if(collection != null) {
          collection.items = FuncHelper.removeItemInArray(collection.items, "id", this.processOrder.id);
        }
        this.showSuccess("Order.Farmer.CompleteSuccess");
      }
      else {
        this.showError(rs.errorMessage);
      }
  }

  private print() {
    this.showInfor("Chức năng chưa hoàn thiện");
  }
}
