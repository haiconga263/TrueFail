import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MapRoutingModule } from './map-routing.module';
import { MapComponent } from './map.component';
// import { TripService } from '../trip/trip.service'
import { AgmCoreModule } from '@agm/core';

@NgModule({
  imports: [
    CommonModule,
    MapRoutingModule,
    AgmCoreModule
  ],
  declarations: [MapComponent]
  // ,providers: [TripService]
})
export class MapModule { }
