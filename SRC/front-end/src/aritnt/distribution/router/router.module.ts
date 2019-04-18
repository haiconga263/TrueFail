import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RouterRoutingModule } from './router-routing.module';
import { RouterComponent } from './router.component';
import { RouterService } from './router.service';
import { GeoService } from '../common/services/geo.service'
import { AgmCoreModule } from '@agm/core';
import { 
  DxButtonModule,
  DxTextBoxModule,
  DxValidatorModule,
  DxSelectBoxModule
} from 'devextreme-angular';
import { FormsModule } from '@angular/forms';
import { DistributionService } from '../common/services/distribution.service';

@NgModule({
  imports: [
    CommonModule,
    AgmCoreModule,
    FormsModule,
    DxButtonModule,
    DxTextBoxModule,
    DxValidatorModule,
    DxSelectBoxModule,
    RouterRoutingModule
  ],
  declarations: [RouterComponent],
  providers: [RouterService, GeoService, DistributionService]
})
export class RouterModule { }
