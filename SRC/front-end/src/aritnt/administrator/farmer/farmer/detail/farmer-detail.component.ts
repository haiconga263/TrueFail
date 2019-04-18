import { Component, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { FarmerService, Farmer } from '../../farmer.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from '../../../app-url';
import { ActivatedRoute, Params } from '@angular/router';
import { ResultModel } from 'src/core/models/http.model';
import { AppConsts } from 'src/core/constant/AppConsts'
import { User, UserService } from '../../../user/user.service';
import { Country, GeoService, District, Ward, Province, Region } from 'src/aritnt/administrator/geographical/geo.service';

@Component({
  selector: 'farmer-detail',
  templateUrl: './farmer-detail.component.html',
  styleUrls: ['./farmer-detail.component.css']
})
export class FarmerDetailComponent extends AppBaseComponent {
  private params: Params;
  private type: string = 'add';
  private id: number = 0;

  private farmer: Farmer = new Farmer();
  private genders: any[] = [];

  private users: User[] = [];

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
  private vietnamesePattern = AppConsts.nonSpecialCharVietnamesePattern;
  private numberPattern = AppConsts.numberPattern;

  constructor(
    injector: Injector,
    private farmSvc: FarmerService,
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
        }
      });
      this.Init();
  }

  async Init()
  {
    this.genders = this.farmSvc.getGenders();


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
          this.provinces = this.allProvinces.filter(p => p.countryId == this.farmer.address.countryId);
        }
      });

      this.geoSvc.getDistricts().subscribe((result) => {
        if(result.result == ResultCode.Success)
        {
          this.allDistricts = result.data;
          this.districts = this.allDistricts.filter(d => d.provinceId == this.farmer.address.provinceId);
        }
      });

      this.geoSvc.getWards().subscribe((result) => {
        if(result.result == ResultCode.Success)
        {
          this.allWards = result.data;
          this.wards = this.allWards.filter(w => w.districtId == this.farmer.address.districtId);
        }
      });
    }
  }

  private async loadDatasource() {

    var rs = await this.farmSvc.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.farmer = rs.data;
      this.farmer.imageData = AppConsts.imageDataUrl + this.farmer.imageURL;

      if(this.farmer.userId != null && this.farmer.userId != 0 && this.users == [])
      {
        this.useSvc.getUser(this.farmer.userId).subscribe((result) => {
          if(rs.result == ResultCode.Success)
          {
            this.users = [];
            this.users.push(result.data);
          }
        });
      }
    }
  }
  avatarChangeEvent(fileInput: any, component: FarmerDetailComponent) {
    if (fileInput.target.files && fileInput.target.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e: any) {
            component.farmer.imageData = e.target.result;
        }
        reader.readAsDataURL(fileInput.target.files[0]);
    }
  }

  private return() {
    console.log('return');
    this.router.navigate([appUrl.farmerList]);
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    console.log('save');
    console.log(this.farmer);
    if (!this.checkValid()) {
      return;
    }

    if(this.type == "update")
    {
      this.farmSvc.update(this.farmer).subscribe((result: ResultModel<any>) => {
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
        this.farmSvc.add(this.farmer).subscribe((result: ResultModel<any>) => {
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
    else if(this.farmer.address != null && this.allCountries.length == 1 && this.allCountries[0].id == this.farmer.address.countryId)
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
        this.provinces = this.allProvinces.filter(p => p.countryId == this.farmer.address.countryId);
      }
    }
    else if(this.farmer.address != null && this.allProvinces.length == 1 && this.allProvinces[0].id == this.farmer.address.provinceId)
    {
      var rs = await this.geoSvc.getProvinces().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allProvinces = rs.data;
        this.provinces = this.allProvinces.filter(p => p.countryId == this.farmer.address.countryId);
      }
    }
    else
    {
      this.provinces = this.allProvinces.filter(p => p.countryId == this.farmer.address.countryId);
    }
  }

  private async districtFocusIn(event: any) {
    if(this.allDistricts == null)
    {
      var rs = await this.geoSvc.getDistricts().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allDistricts = rs.data;
        this.districts = this.allDistricts.filter(d => d.provinceId == this.farmer.address.provinceId);
      }
    }
    else if(this.farmer.address != null && this.allDistricts.length == 1 && this.allDistricts[0].id == this.farmer.address.provinceId)
    {
      var rs = await this.geoSvc.getDistricts().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allDistricts = rs.data;
        this.districts = this.allDistricts.filter(d => d.provinceId == this.farmer.address.provinceId);
      }
    }
    else
    {
      this.districts = this.allDistricts.filter(d => d.provinceId == this.farmer.address.provinceId);
    }
  }

  private async wardFocusIn(event: any) {
    if(this.allWards == null)
    {
      var rs = await this.geoSvc.getWards().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allWards = rs.data;
        this.wards = this.allWards.filter(w => w.districtId == this.farmer.address.districtId);
      }
    }
    else if(this.farmer.address != null && this.allWards.length == 1 && this.allWards[0].id == this.farmer.address.provinceId)
    {
      var rs = await this.geoSvc.getWards().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allWards = rs.data;
        this.wards = this.allWards.filter(w => w.districtId == this.farmer.address.districtId)
      }
    }
    else{
      this.wards = this.allWards.filter(w => w.districtId == this.farmer.address.districtId)
    }
  }

  private async userFocusIn(event: any) {
    if(this.users == [])
    {
      var rs = await this.useSvc.getUsersNotAssignBy(true, 'Farmer').toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.users = rs.data;
      }
    }
    else if(this.users.length == 1 && this.users[0].id == this.farmer.userId)
    {
      var currentUser = this.users[0];
      var rs = await this.useSvc.getUsersNotAssignBy(true, 'Farmer').toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.users = rs.data;
        if(this.users.find(u => u.id == currentUser.id) == null)
        {
          this.users.push(currentUser);
        }
      }
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
    this.farmer.address.longitude = event.coords.lng;
    this.farmer.address.latitude = event.coords.lat;
  }
}
