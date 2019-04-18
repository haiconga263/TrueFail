import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { TripService, Trip, TripStatus } from '../trip/trip.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { Distribution, DistributionService } from '../common/services/distribution.service';

@Component({
  selector: 'map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent extends AppBaseComponent implements OnInit {
  protected map: any;
  private centerLong: number = 106.6949914;
  private centerLat: number = 10.8390474;

  private trips: Trip[] = [];
  private distributions: Distribution[] = [];
  private chooseDistribution: Distribution;

  constructor(
    injector: Injector,
    private tripSvc: TripService,
    private disSvc: DistributionService
  ) {
    super(injector);

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
    this.tripSvc.gets(distributionId).subscribe(rs => {
      if(rs.result == ResultCode.Success) {
        this.trips = rs.data;
      }
      else {
        this.showError(rs.errorMessage);
      }
    }); 
  }

  private distributionChanged(event) {
    if(event.value == null) {
      return;
    }
    this.loadTripSource(event.value.id); 
  }

  protected mapReady(map) {
    this.map = map;
  }
}
