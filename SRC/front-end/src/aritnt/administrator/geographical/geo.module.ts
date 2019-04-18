import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GeoRoutingModule } from './geo-routing.module';
import { CountryComponent } from './country/country.component';
import { RegionComponent } from './region/region.component';
import { ProvinceComponent } from './province/province.component';
import { DistrictComponent } from './district/district.component';
import { WardComponent } from './ward/ward.component';
import { 
  DxDataGridModule, 
  DxButtonModule, 
  DxSelectBoxModule, 
  DxTextBoxModule, 
  DxValidatorModule,
  DxCheckBoxModule,
  DxNumberBoxModule,
  DxLookupModule
} from 'devextreme-angular';
import { GeoService } from './geo.service';
import { UoMService } from '../common/services/uom.service';
import { FormsModule } from '@angular/forms';
import { SweetAlert2Module } from '@toverux/ngx-sweetalert2';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    GeoRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxTextBoxModule,
    DxValidatorModule,
    DxCheckBoxModule,
    DxNumberBoxModule,
    DxLookupModule,
    SweetAlert2Module
  ],
  declarations: [CountryComponent, RegionComponent, ProvinceComponent, DistrictComponent, WardComponent],
  providers: [GeoService, UoMService]
})
export class GeoModule { }
