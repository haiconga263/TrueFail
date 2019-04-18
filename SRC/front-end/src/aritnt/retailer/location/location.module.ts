import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LocationRoutingModule } from './location-routing.module';
import { LocationComponent } from './master/location.component';
import { LocationDetailComponent } from './detail/location-detail.component';
import { 
  DxDataGridModule, 
  DxButtonModule, 
  DxSelectBoxModule, 
  DxTextBoxModule, 
  DxValidatorModule,
  DxCheckBoxModule,
  DxDateBoxModule,
  DxNumberBoxModule,
  DxPopupModule
} from 'devextreme-angular';
import { LocationService } from './location.service';
import { FormsModule } from '@angular/forms';
import { GeoService } from 'src/aritnt/administrator/geographical/geo.service';
import { AgmCoreModule } from '@agm/core';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    LocationRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxTextBoxModule,
    DxValidatorModule,
    DxCheckBoxModule,
    DxDateBoxModule,
    DxNumberBoxModule,
    DxPopupModule,
    AgmCoreModule
  ],
  declarations: [LocationComponent, LocationDetailComponent],
  providers: [LocationService, GeoService]
})
export class LocationModule { }
