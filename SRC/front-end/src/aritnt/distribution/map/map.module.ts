import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MapRoutingModule } from './map-routing.module';
import { MapComponent } from './map.component';
import { TripService } from '../trip/trip.service'
import { AgmCoreModule } from '@agm/core';
import { FormsModule } from '@angular/forms';
import { DistributionService } from '../common/services/distribution.service';
import { 
  DxSelectBoxModule
} from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    MapRoutingModule,
    AgmCoreModule,
    DxSelectBoxModule,
    FormsModule
  ],
  declarations: [MapComponent],
  providers: [TripService, DistributionService]
})
export class MapModule { }
