import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
// import { TripService, Trip, TripStatus } from '../trip/trip.service';
import { ResultCode } from 'src/core/constant/AppEnums';

@Component({
  selector: 'map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent extends AppBaseComponent implements OnInit {
  protected map: any;
  private centerLong: number = 106.6949914;
  private centerLat: number = 10.8390474;

  // private trips: Trip[] = [];

  constructor(
    injector: Injector,
    // private tripSvc: TripService
  ) {
    super(injector);

    // this.loadMainSource();
  }
  // private loadMainSource() {
  //   this.tripSvc.gets().subscribe(rs => {
  //     if (rs.result == ResultCode.Success) {
  //       this.trips = rs.data;
  //       this.trips = this.trips.filter(t => t.statusId != 1);
  //     }
  //   });
  // }

  protected mapReady(map) {
    this.map = map;
  }
}
