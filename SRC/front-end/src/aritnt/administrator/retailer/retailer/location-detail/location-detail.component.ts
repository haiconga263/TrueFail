import { Component, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { RetailerService, RetailerLocation } from '../../retailer.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from '../../../app-url';
import { ActivatedRoute, Params } from '@angular/router';
import { ResultModel } from 'src/core/models/http.model';
import { AppConsts } from 'src/core/constant/AppConsts'
import { User, UserService } from '../../../user/user.service';
import { Country, GeoService, District, Ward, Province } from 'src/aritnt/administrator/geographical/geo.service';

@Component({
  selector: 'location-detail',
  templateUrl: './location-detail.component.html',
  styleUrls: ['./location-detail.component.css']
})
export class LocationDetailComponent extends AppBaseComponent {
  private params: Params;
  private type: string = 'add';
  private id: number = 0;
  private retailerId: number = 0;

  private location: RetailerLocation = new RetailerLocation();
  private genders: any[] = [];

  private allCountries: Country[] = null;
  private allProvinces: Province[] = null;
  private allDistricts: District[] = null;
  private allWards: Ward[] = null;

  private provinces: Province[] = [];
  private districts: District[] = [];
  private wards: Ward[] = [];

  private isNameValid: boolean = false;

  private isAddressStreetValid: boolean = false;
  private isAddressCountryValid: boolean = false;
  private isAddressProvinceValid: boolean = false;
  private isAddressDistrictValid: boolean = false;
  private isAddressWardValid: boolean = false;

  private isContactNameValid: boolean = false;
  private isContactEmailValid: boolean = false;
  private isContactPhoneValid: boolean = false;

  private phonePattern = AppConsts.vietnamPhonePattern;

  constructor(
    injector: Injector,
    private retSvc: RetailerService,
    private useSvc: UserService,
    private geoSvc: GeoService,
    private activatedRoute: ActivatedRoute
  ) {
    super(injector);
      this.activatedRoute.queryParams.subscribe((params: Params) => {
        this.params = params;
        if (this.params['type'] == 'update') {
          this.type = 'update';
          this.id = this.params["id"];
          this.retailerId = this.params["retailerId"];
        }
        else
        {
          this.retailerId = this.params["retailerId"];
        }
      });
      this.Init();
  }

  async Init()
  {
    this.genders = this.retSvc.getGenders();


    if (this.params['type'] == 'update') {
      await this.loadDatasource();

      this.geoSvc.getCountries().subscribe((result) => {
        if(result.result == ResultCode.Success)
        {
          this.allCountries = result.data;
        }
      });

      this.geoSvc.getProvinces().subscribe((result) => {
        if(result.result == ResultCode.Success)
        {
          this.allProvinces = result.data;
          this.provinces = this.allProvinces.filter(p => p.countryId == this.location.address.countryId);
        }
      });

      this.geoSvc.getDistricts().subscribe((result) => {
        if(result.result == ResultCode.Success)
        {
          this.allDistricts = result.data;
          this.districts = this.allDistricts.filter(d => d.provinceId == this.location.address.provinceId);
        }
      });

      this.geoSvc.getWards().subscribe((result) => {
        if(result.result == ResultCode.Success)
        {
          this.allWards = result.data;
          this.wards = this.allWards.filter(w => w.districtId == this.location.address.districtId);
        }
      });
    }
    else
    {
      this.location.retailerId = this.retailerId;
    }
  }

  private async loadDatasource() {

    var rs = await this.retSvc.getLocation(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.location = rs.data;
      this.location.imageData = AppConsts.imageDataUrl + this.location.imageURL;
    }
  }
  avatarChangeEvent(fileInput: any, component: LocationDetailComponent) {
    if (fileInput.target.files && fileInput.target.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e: any) {
            component.location.imageData = e.target.result;
        }
        reader.readAsDataURL(fileInput.target.files[0]);
    }
  }

  private return() {
    console.log('return');
    this.router.navigate([appUrl.retailerDetail],
      {
        queryParams: {
          type: 'update',
          id: this.retailerId
        }
      });
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    console.log('save');
    console.log(this.location);
    if (!this.checkValid()) {
      return;
    }

    if(this.type == "update")
    {
      this.retSvc.updateLocation(this.location).subscribe((result: ResultModel<any>) => {
        if(result.result == ResultCode.Success)
        {
          //alert
          this.showSuccess(this.lang.instant('Common.UpdateSuccess'));
          this.return();
        }
        else
        {
          //alert
          this.showError(result.errorMessage);
        }
      });
    }
      else{
        this.retSvc.addLocation(this.location).subscribe((result: ResultModel<any>) => {
          if(result.result == ResultCode.Success)
          {
            //alert
            this.showSuccess(this.lang.instant('Common.AddSuccess'));
            this.return();
          }
          else
          {
            //alert
            this.showError(result.errorMessage);
          }
        });
      }
  }

  private async countryFocusIn(event: any) {
    if(this.allCountries == null)
    {
      var rs = await this.geoSvc.getCountries().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allCountries = rs.data;
      }
    }
    else if(this.location.address != null && this.allCountries.length == 1 && this.allCountries[0].id == this.location.address.countryId)
    {
      var rs = await this.geoSvc.getCountries().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allCountries = rs.data;
      }
    }
  }

  private async provinceFocusIn(event: any) {
    if(this.allProvinces == null)
    {
      var rs = await this.geoSvc.getProvinces().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allProvinces = rs.data;
        this.provinces = this.allProvinces.filter(p => p.countryId == this.location.address.countryId);
      }
    }
    else if(this.location.address != null && this.allProvinces.length == 1 && this.allProvinces[0].id == this.location.address.provinceId)
    {
      var rs = await this.geoSvc.getProvinces().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allProvinces = rs.data;
        this.provinces = this.allProvinces.filter(p => p.countryId == this.location.address.countryId);
      }
    }
    else
    {
      this.provinces = this.allProvinces.filter(p => p.countryId == this.location.address.countryId);
    }
  }

  private async districtFocusIn(event: any) {
    if(this.allDistricts == null)
    {
      var rs = await this.geoSvc.getDistricts().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allDistricts = rs.data;
        this.districts = this.allDistricts.filter(d => d.provinceId == this.location.address.provinceId);
      }
    }
    else if(this.location.address != null && this.allDistricts.length == 1 && this.allDistricts[0].id == this.location.address.provinceId)
    {
      var rs = await this.geoSvc.getDistricts().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allDistricts = rs.data;
        this.districts = this.allDistricts.filter(d => d.provinceId == this.location.address.provinceId);
      }
    }
    else
    {
      this.districts = this.allDistricts.filter(d => d.provinceId == this.location.address.provinceId);
    }
  }

  private async wardFocusIn(event: any) {
    if(this.allWards == null)
    {
      var rs = await this.geoSvc.getWards().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allWards = rs.data;
        this.wards = this.allWards.filter(w => w.districtId == this.location.address.districtId);
      }
    }
    else if(this.location.address != null && this.allWards.length == 1 && this.allWards[0].id == this.location.address.provinceId)
    {
      var rs = await this.geoSvc.getWards().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allWards = rs.data;
        this.wards = this.allWards.filter(w => w.districtId == this.location.address.districtId)
      }
    }
    else{
      this.wards = this.allWards.filter(w => w.districtId == this.location.address.districtId)
    }
  }

  private checkValid() : boolean{
    return this.isNameValid && this.isAddressWardValid &&
           this.isAddressStreetValid && this.isAddressCountryValid &&
           this.isAddressProvinceValid && this.isAddressDistrictValid &&
           this.isContactNameValid && this.isContactPhoneValid &&
           this.isContactEmailValid;
  }

  private isOpenMap: boolean = false;
  private openMap() {
    this.isOpenMap = true;
  }

  private mapClick(event: any) {
    this.location.address.longitude = event.coords.lng;
    this.location.address.latitude = event.coords.lat;
  }
}
