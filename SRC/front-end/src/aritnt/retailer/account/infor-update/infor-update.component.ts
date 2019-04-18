import { Component, OnInit, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { Retailer, AccountService } from '../account.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from 'src/aritnt/retailer/app-url';
import { AppConsts } from 'src/core/constant/AppConsts';
import { Country, Province, District, Ward, GeoService } from '../../common/services/geo.service';
import { ResultModel } from 'src/core/models/http.model';

@Component({
  selector: 'update',
  templateUrl: './infor-update.component.html',
  styleUrls: ['./infor-update.component.css']
})
export class InforUpdateComponent extends AppBaseComponent {

  private account: Retailer = new Retailer();

  private allCountries: Country[] = null;
  private allProvinces: Province[] = null;
  private allDistricts: District[] = null;
  private allWards: Ward[] = null;

  private isNameValid: boolean = false;
  private isTaxCodeValid: boolean = false;

  private provinces: Province[] = [];
  private districts: District[] = [];
  private wards: Ward[] = [];

  private isAddressStreetValid: boolean = false;
  private isAddressCountryValid: boolean = false;
  private isAddressProvinceValid: boolean = false;
  private isAddressDistrictValid: boolean = false;
  private isAddressWardValid: boolean = false;

  private isContactNameValid: boolean = false;
  private isContactEmailValid: boolean = false;
  private isContactPhoneValid: boolean = false;

  private phonePattern = AppConsts.vietnamPhonePattern;
  private emailPattern = AppConsts.emailPattern;
  private numberPattern = AppConsts.numberPattern;
  private vietnamesePattern = AppConsts.nonSpecialCharVietnamesePattern;

  private genders: any[] = [];

  constructor(
    private accSvc: AccountService,
    private geoSvc: GeoService,
    injector: Injector
  ) {
    super(injector);

    this.Init();
  }

  private Init() {
    this.genders = this.accSvc.getGenders();

    this.accSvc.get(this.authenticSvc.getSession().id).subscribe(rRs => {
      if(rRs.result == ResultCode.Success)
      {
        this.account = rRs.data;
        this.account.imageData = AppConsts.imageDataUrl + this.account.imageURL;

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
            this.provinces = this.allProvinces.filter(p => p.countryId == this.account.address.countryId);
          }
        });
  
        this.geoSvc.getDistricts().subscribe((result) => {
          if(result.result == ResultCode.Success)
          {
            this.allDistricts = result.data;
            this.districts = this.allDistricts.filter(d => d.provinceId == this.account.address.provinceId);
          }
        });
  
        this.geoSvc.getWards().subscribe((result) => {
          if(result.result == ResultCode.Success)
          {
            this.allWards = result.data;
            this.wards = this.allWards.filter(w => w.districtId == this.account.address.districtId);
          }
        });
      }
    });
  }

  private refresh() {
    this.Init();
  }

  private save() {
    if (!this.checkValid()) {
      return;
    }
    this.accSvc.update(this.account).subscribe((result: ResultModel<any>) => {
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

  private return() {
    this.router.navigate([appUrl.account]);
  }

  private checkValid() : boolean{
    return this.isNameValid && this.isAddressWardValid &&
           this.isAddressStreetValid && this.isAddressCountryValid &&
           this.isAddressProvinceValid && this.isAddressDistrictValid &&
           this.isContactNameValid && this.isContactPhoneValid &&
           this.isContactEmailValid;
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
    else if(this.account.address != null && this.allCountries.length == 1 && this.allCountries[0].id == this.account.address.countryId)
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
        this.provinces = this.allProvinces.filter(p => p.countryId == this.account.address.countryId);
      }
    }
    else if(this.account.address != null && this.allProvinces.length == 1 && this.allProvinces[0].id == this.account.address.provinceId)
    {
      var rs = await this.geoSvc.getProvinces().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allProvinces = rs.data;
        this.provinces = this.allProvinces.filter(p => p.countryId == this.account.address.countryId);
      }
    }
    else
    {
      this.provinces = this.allProvinces.filter(p => p.countryId == this.account.address.countryId);
    }
  }

  private async districtFocusIn(event: any) {
    if(this.allDistricts == null)
    {
      var rs = await this.geoSvc.getDistricts().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allDistricts = rs.data;
        this.districts = this.allDistricts.filter(d => d.provinceId == this.account.address.provinceId);
      }
    }
    else if(this.account.address != null && this.allDistricts.length == 1 && this.allDistricts[0].id == this.account.address.provinceId)
    {
      var rs = await this.geoSvc.getDistricts().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allDistricts = rs.data;
        this.districts = this.allDistricts.filter(d => d.provinceId == this.account.address.provinceId);
      }
    }
    else
    {
      this.districts = this.allDistricts.filter(d => d.provinceId == this.account.address.provinceId);
    }
  }

  private async wardFocusIn(event: any) {
    if(this.allWards == null)
    {
      var rs = await this.geoSvc.getWards().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allWards = rs.data;
        this.wards = this.allWards.filter(w => w.districtId == this.account.address.districtId);
      }
    }
    else if(this.account.address != null && this.allWards.length == 1 && this.allWards[0].id == this.account.address.provinceId)
    {
      var rs = await this.geoSvc.getWards().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allWards = rs.data;
        this.wards = this.allWards.filter(w => w.districtId == this.account.address.districtId)
      }
    }
    else{
      this.wards = this.allWards.filter(w => w.districtId == this.account.address.districtId)
    }
  }
  private isOpenMap: boolean = false;
  private openMap() {
    this.isOpenMap = true;
  }

  private mapClick(event: any) {
    this.account.address.longitude = event.coords.lng;
    this.account.address.latitude = event.coords.lat;
  }

  private isCompanyChanged(event) {
    console.log(event);
    if(event.value == true) {
      if(this.account.taxCode == null || this.account.taxCode == "") {
        this.isTaxCodeValid = false;
      }
    }
    else {
      this.isTaxCodeValid = true;
      this.account.taxCode = "";
    }
  }
}
