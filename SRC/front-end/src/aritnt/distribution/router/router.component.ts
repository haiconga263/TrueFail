import { Component, OnInit, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { RouterService, Router } from './router.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { Country, Province, GeoService } from '../common/services/geo.service';
import { LatLng } from '@agm/core';
import { Distribution, DistributionService } from '../common/services/distribution.service';

@Component({
  selector: 'router-1',
  templateUrl: './router.component.html',
  styleUrls: ['./router.component.css']
})
export class RouterComponent extends AppBaseComponent {
  protected map: any;


  private centerLong: number = 106.6949914;
  private centerLat: number = 10.8390474;
  private defaultRadius: number = 5000;

  private distributions: Distribution[] = [];
  private chooseDistribution: Distribution;
  private routers: Router[] = [];

  private route: Router = null;

  private countries: Country[] = [];
  private allProvinces: Province[] = [];
  private provinces: Province[] = [];

  private isInforOpen: boolean = true;

  private usePanning: boolean = true;

  constructor(
    injector: Injector,
    private routeSvc: RouterService,
    private geoSvc: GeoService,
    private disSvc: DistributionService
  ) {
    super(injector);

    this.loadSource();
  }

  private loadSource() {
    this.geoSvc.getCountries().subscribe(rs => {
      if(rs.result == ResultCode.Success)
      {
        this.countries = rs.data;
      }
    });

    this.geoSvc.getProvinces().subscribe(rs => {
      if(rs.result == ResultCode.Success)
      {
        this.allProvinces = rs.data;
      }
    });

    this.loadMainSource();
  }

  private loadMainSource() {
    this.disSvc.getByOwners().subscribe(disRs => {
      if (disRs.result == ResultCode.Success) {
        this.distributions = disRs.data;
      }
    });
  }

  private loadRouterSource(distributionId: number) {
    this.routers = null;
    this.routeSvc.gets(distributionId).subscribe(rs => {
      if(rs.result == ResultCode.Success) {
        this.routers = rs.data;
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
    this.loadRouterSource(event.value.id); 
    this.route = null;
  }

  protected mapReady(map) {
    this.map = map;
  }

  private mapClick(event) {
    this.isInforOpen = false;
  }

  private refresh() {
    this.loadRouterSource(this.chooseDistribution.id);
    this.route = null;
  }

  private create() {
    this.route = new Router();
    var center = this.map.getCenter();
    this.route.currentLongitude = center.lng();
    this.route.currentLatitude = center.lat();
    this.route.radius = this.defaultRadius;
  }

  private async save() {
    console.log(this.route);
    if(this.route.id == 0)
    {
      let rs = await this.routeSvc.add(this.route).toPromise();
      if(rs != null && rs.result == ResultCode.Success) {
        this.showSuccess("Common.AddSuccess");
        this.loadRouterSource(this.chooseDistribution.id);
        this.route = null;
      }
      else {
        this.showError(rs.errorMessage);
      }
    }
    else
    {
      let rs = await this.routeSvc.update(this.route).toPromise();
      if(rs != null && rs.result == ResultCode.Success) {
        this.showSuccess("Common.UpdateSuccess");
      }
      else {
        this.showError(rs.errorMessage);
      }
    }
  }
  
  private cancel() {
    this.route = null;
  }

  private async delete() {
    let rs = await this.routeSvc.delete(this.route.id).toPromise();
    if(rs != null && rs.result == ResultCode.Success) {
      this.showSuccess("Common.DeleteSuccess");
      this.loadRouterSource(this.chooseDistribution.id);
      this.route = null;
    }
    else {
      this.showSuccess(rs.errorMessage);
    }
  }

  private routerChange(event) {
    console.log(event);
    this.route = this.routers.find(r => r.id == event.value);
    this.map.setCenter({ lat: this.route.currentLatitude, lng: this.route.currentLongitude });
  }

  private routerClick(router: Router) {
    this.route = router;
    this.isInforOpen =true;
    console.log(this.map);
  }

  private centerChange(event, routerId) {
    if(this.route == null || (this.route.id == 0 && routerId != 0) || (this.route.id != 0 && this.route.id != routerId))
    {
      this.route = this.routers.find(r => r.id == routerId);
    }
    this.route.currentLongitude = event.lng;
    this.route.currentLatitude = event.lat;
  }

  private radiusChange(event, routerId) {
    if(this.route == null || (this.route.id == 0 && routerId != 0) || (this.route.id != 0 && this.route.id != routerId))
    {
      this.route = this.routers.find(r => r.id == routerId);
    }
    this.route.radius = event;
  }

  private countryChange(event) {
    this.provinces = this.allProvinces.filter(p => p.countryId == this.route.countryId);
  }

  private openClose() {
    this.isInforOpen = !this.isInforOpen;
  }
}
