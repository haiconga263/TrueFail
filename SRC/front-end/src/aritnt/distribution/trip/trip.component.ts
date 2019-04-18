import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { TripService, Trip, TripStatus } from './trip.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { Employee, EmployeeService } from '../common/services/employee.service';
import { RouterService, Router } from '../router/router.service';
import { User, UserService } from '../common/services/user.service';
import { DxDataGridComponent } from 'devextreme-angular/ui/data-grid';
import { Vehicle, VehicleService } from '../common/services/vehicle.service';
import { LocationService } from '../common/services/location.service';
import { DistributionService, Distribution } from '../common/services/distribution.service';
import { DistributionEmployeeService } from '../common/services/distribution-employee.service';
import { FuncHelper } from 'src/core/helpers/function-helper';

@Component({
  selector: 'trip',
  templateUrl: './trip.component.html',
  styleUrls: ['./trip.component.css']
})
export class TripComponent extends AppBaseComponent implements OnInit {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  selectedRows: number[];

  private distributions: Distribution[] = [];
  private chooseDistribution: Distribution;

  private trips: Trip[] = [];
  private users: User[] = [];
  private employees: Employee[] = [];
  private routers: Router[] = [];
  private statuses: TripStatus[] = [];
  private vehicles: Vehicle[] = [];

  private deliveryMans: Employee[] = [];
  private drivers: Employee[] = [];

  private awaitTrips: Trip[] = [];

  private now = new Date(Date.now());

  constructor(
    injector: Injector,
    private tripSvc: TripService,
    private userSvc: UserService,
    private empSvc: EmployeeService,
    private routerSvc: RouterService,
    private vehiSvc: VehicleService,
    private locSvc: LocationService,
    private disSvc: DistributionService,
    private disEmpSvc: DistributionEmployeeService
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
      }
    });

    this.vehiSvc.gets().subscribe(rs => {
      if (rs.result == ResultCode.Success) {
        this.vehicles = rs.data;
      }
    });

    let rsStatuses = await this.tripSvc.getStatuses().toPromise();
    if (rsStatuses.result == ResultCode.Success) {
      this.statuses = rsStatuses.data;
      this.statuses.push({
        id: -2,
        name: "Pending",
        description: "",
        flagColor: "#000000"
      });
    }

    this.loadMainSource();
  }

  private loadMainSource() {
    this.disSvc.getByOwners().subscribe(disRs => {
      if (disRs.result == ResultCode.Success) {
        this.distributions = disRs.data;
      }
    });
  }

  private loadTripSource(distributionId: number) {
    this.trips = null;
    this.routerSvc.gets(distributionId).subscribe(rs => {
      if (rs.result == ResultCode.Success) {
        this.routers = rs.data;
      }
    });
    this.tripSvc.gets(distributionId).subscribe(rs => {
      if(rs.result == ResultCode.Success) {
        let pendingTrip = new Trip();
        pendingTrip.id = -1;
        pendingTrip.code = "PENDING TRIP";
        pendingTrip.statusId = -2;

        this.trips = [];
        this.trips.push(pendingTrip);
        this.trips = this.trips.concat(rs.data);
        this.awaitTrips = this.trips.filter(t => t.statusId == 1 || t.id == -1);
      }
      else {
        this.showError(rs.errorMessage);
      }
    }); 
  }

  private loadEmployeeSource(distributionId: number) {
    if (this.users != null) {
      this.disEmpSvc.gets(this.chooseDistribution.id).subscribe(rs => {
        if(rs.result == ResultCode.Success) {
          this.deliveryMans = this.employees.filter(e => rs.data.find(de => de.employeeId == e.id) != null && this.users.find(u => u.id == e.userId && u.roles.find(r => r.name == "DeliveryMan") != null) != null);
          this.drivers = this.employees.filter(e => rs.data.find(de => de.employeeId == e.id) != null && this.users.find(u => u.id == e.userId && u.roles.find(r => r.name == "DeliveryDriver") != null) != null);
        }
      })
    }
  }

  private distributionChanged(event) {
    if(event.value == null) {
      return;
    }
    this.loadTripSource(event.value.id); 
    this.loadEmployeeSource(event.value.id);
    this.dataGrid.instance.collapseAll(-1);
    this.dataGrid.instance.cancelEditData();
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
      let trip = this.trips.find(t => t.id == this.selectedRows[0]);
      if (trip != null) {
        if(trip.deliveryManId == null) {
          this.showError("Distribution.Trip.RequiredDeliveryMan");
          return;
        }
        if(trip.driverId == null) {
          this.showError("Distribution.Trip.RequiredDriver");
          return;
        }
        if(trip.vehicleId == null) {
          this.showError("Distribution.Trip.RequiredVehicle");
          return;
        }
        let rs = await this.tripSvc.changeStatus(this.selectedRows[0], 2).toPromise();
        if (rs.result == ResultCode.Success) {
          this.showSuccess('Common.UpdateSuccess');
          trip.statusId = 2;
        }
        else {
          this.showError(rs.errorMessage);
        }
      }
    }
  }

  async delete() {
    let rs = await this.tripSvc.delete(this.selectedRows[0]).toPromise();
      if (rs.result == ResultCode.Success) {
        this.showSuccess('Common.DeleteSuccess');
        this.trips = FuncHelper.removeItemInArray(this.trips, "id", this.selectedRows[0]);
      }
      else {
        this.showError(rs.errorMessage);
      }
  }

  async tripUpdating(event) {
    event.cancel = true;
    let model = event['oldData'];
    for (var k in event.newData) {
      model[k] = event.newData[k];
    }

    this.tripSvc.update(model).subscribe(rs => {
      if (rs.result == ResultCode.Success) {
        //alert
        this.showSuccess('Common.UpdateSuccess');
        this.dataGrid.instance.cancelEditData();
      }
      else {
        this.showError(rs.errorMessage);
      }
    });
  }

  async tripAdding(event) {
    event.cancel = true;
    console.log(event);
    let model: Trip = new Trip();
    model.id = 0;
    model.distributionId = this.chooseDistribution.id;
    model.statusId = 0;
    for (var k in event.data) {
      model[k] = event.data[k];
    }
    let rs = await this.tripSvc.add(model).toPromise();
    if (rs.result == ResultCode.Success) {
      this.showSuccess('Common.AddSuccess');
      this.loadTripSource(this.chooseDistribution.id); 
      this.dataGrid.instance.collapseAll(-1);
      this.dataGrid.instance.cancelEditData();
    }
    else {
      this.showError(rs.errorMessage);
    }
  }

  private async onTripExpanding(event) {
    let id = event.key;
    let trip = this.trips.find(t => t.id == id);
    if (trip.orders == null) {
      trip.orders = [];
      this.tripSvc.getOrders(this.chooseDistribution.id, trip.id).subscribe(async rs => {
        if (rs.result == ResultCode.Success) {
          trip.orders = rs.data;

          for (var i = 0; i < trip.orders.length; i++) {
            let locationRs = await this.locSvc.get(trip.orders[i].shipTo).toPromise();
            if (locationRs.result == ResultCode.Success) {
              trip.orders[i].ship = locationRs.data;
            }
          }
        }
      });
    }
  }

  private async onOrderChangeTrip(event: any, orderId: number, sourceTripId: number) {
    console.log(event);
    var trip = this.trips.find(t => t.orders != null && t.orders.find(o => o.id == orderId) != null);
    if (trip == null || event.value != trip.id) {
      let rs = await this.tripSvc.moveOrder(orderId, event.value).toPromise();
      if (rs.result == ResultCode.Success) {
        this.showSuccess('Common.UpdateSuccess');
        
        let order = trip.orders.find(o => o.id == orderId);
        if(order != null) {
          order.tripId = event.value;
          trip.orders = FuncHelper.removeItemInArray(trip.orders, "id", orderId);
          let desTrip = this.trips.find(t => t.id == event.value);
          if(desTrip != null) {
            desTrip.orders.push(order);
          }
        }
      }
      else {
        this.showError(rs.errorMessage);
      }
    }
  }

  getPendingTrip(tripId: number) {
    if(this.trips == null) {
      return null;
    }
    return this.trips.filter(t => t.statusId == 1 && t.id != tripId);
  }

  getStatus(id: number) {
    return this.statuses.find(s => s.id == id);
  }

  getTrip(id: number) {
    if(this.trips == null) {
      return null;
    }
    return this.trips.find(t => t.id == id);
  }
}
