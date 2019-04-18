import { Component, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { FulfillmentService, Fulfillment } from '../fulfillment.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from '../../app-url';
import { ActivatedRoute, Params } from '@angular/router';
import { ResultModel } from 'src/core/models/http.model';
import { AppConsts } from 'src/core/constant/AppConsts'
import { GeoService, Country, Province, District, Ward } from '../../geographical/geo.service';
import { Employee, EmployeeService } from '../../employee/employee.service';

@Component({
  selector: 'fulfillment-detail',
  templateUrl: './fulfillment-detail.component.html',
  styleUrls: ['./fulfillment-detail.component.css']
})
export class FulfillmentDetailComponent extends AppBaseComponent {
  private params: Params;
  private type: string = 'add';
  private id: number = 0;

  private fulfillment: Fulfillment = new Fulfillment();
  private genders: any[] = [];

  private managers: Employee[] = null;

  private allCountries: Country[] = null;
  private allProvinces: Province[] = null;
  private allDistricts: District[] = null;
  private allWards: Ward[] = null;

  private provinces: Province[] = [];
  private districts: District[] = [];
  private wards: Ward[] = [];

  private isNameValid: boolean = false;
  private isManagerValid: boolean = false;

  private isAddressStreetValid: boolean = false;
  private isAddressCountryValid: boolean = false;
  private isAddressProvinceValid: boolean = false;
  private isAddressDistrictValid: boolean = false;
  private isAddressWardValid: boolean = false;

  private isContactNameValid: boolean = false;
  private isContactEmailValid: boolean = false;
  private isContactPhoneValid: boolean = false;
  private isGenderValid: boolean = false;

  private phonePattern = AppConsts.vietnamPhonePattern;
  private emailPattern = AppConsts.emailPattern;
  private vietnamesePattern = AppConsts.nonSpecialCharVietnamesePattern;

  constructor(
    injector: Injector,
    private collSvc: FulfillmentService,
    private empSvc: EmployeeService,
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
    this.genders = this.collSvc.getGenders();
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
          this.provinces = this.allProvinces.filter(p => p.countryId == this.fulfillment.address.countryId);
        }
      });

      this.geoSvc.getDistricts().subscribe((result) => {
        if(result.result == ResultCode.Success)
        {
          this.allDistricts = result.data;
          this.districts = this.allDistricts.filter(d => d.provinceId == this.fulfillment.address.provinceId);
        }
      });

      this.geoSvc.getWards().subscribe((result) => {
        if(result.result == ResultCode.Success)
        {
          this.allWards = result.data;
          this.wards = this.allWards.filter(w => w.districtId == this.fulfillment.address.districtId);
        }
      });
    }
  }

  private async loadDatasource() {

    var rs = await this.collSvc.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.fulfillment = rs.data;
      this.fulfillment.imageData = AppConsts.imageDataUrl + this.fulfillment.imageURL;

      if(this.fulfillment.managerId != null && this.fulfillment.managerId != 0 && this.managers == null)
      {
        this.empSvc.get(this.fulfillment.managerId).subscribe((result) => {
          if(rs.result == ResultCode.Success)
          {
            this.managers = [];
            this.managers.push(result.data);
          }
        });
      }
    }
  }
  avatarChangeEvent(fileInput: any, component: FulfillmentDetailComponent) {
    if (fileInput.target.files && fileInput.target.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e: any) {
            component.fulfillment.imageData = e.target.result;
        }
        reader.readAsDataURL(fileInput.target.files[0]);
    }
  }

  private return() {
    console.log('return');
    this.router.navigate([appUrl.fulfillmentList]);
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    console.log('save');
    console.log(this.fulfillment);
    if (!this.checkValid()) {
      return;
    }

    if(this.type == "update")
    {
      this.collSvc.update(this.fulfillment).subscribe((result: ResultModel<any>) => {
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
        this.collSvc.add(this.fulfillment).subscribe((result: ResultModel<any>) => {
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
    else if(this.fulfillment.address != null && this.allCountries.length == 1 && this.allCountries[0].id == this.fulfillment.address.countryId)
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
        this.provinces = this.allProvinces.filter(p => p.countryId == this.fulfillment.address.countryId);
      }
    }
    else if(this.fulfillment.address != null && this.allProvinces.length == 1 && this.allProvinces[0].id == this.fulfillment.address.provinceId)
    {
      var rs = await this.geoSvc.getProvinces().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allProvinces = rs.data;
        this.provinces = this.allProvinces.filter(p => p.countryId == this.fulfillment.address.countryId);
      }
    }
    else
    {
      this.provinces = this.allProvinces.filter(p => p.countryId == this.fulfillment.address.countryId);
    }
  }

  private async districtFocusIn(event: any) {
    if(this.allDistricts == null)
    {
      var rs = await this.geoSvc.getDistricts().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allDistricts = rs.data;
        this.districts = this.allDistricts.filter(d => d.provinceId == this.fulfillment.address.provinceId);
      }
    }
    else if(this.fulfillment.address != null && this.allDistricts.length == 1 && this.allDistricts[0].id == this.fulfillment.address.provinceId)
    {
      var rs = await this.geoSvc.getDistricts().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allDistricts = rs.data;
        this.districts = this.allDistricts.filter(d => d.provinceId == this.fulfillment.address.provinceId);
      }
    }
    else
    {
      this.districts = this.allDistricts.filter(d => d.provinceId == this.fulfillment.address.provinceId);
    }
  }

  private async wardFocusIn(event: any) {
    if(this.allWards == null)
    {
      var rs = await this.geoSvc.getWards().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allWards = rs.data;
        this.wards = this.allWards.filter(w => w.districtId == this.fulfillment.address.districtId);
      }
    }
    else if(this.fulfillment.address != null && this.allWards.length == 1 && this.allWards[0].id == this.fulfillment.address.provinceId)
    {
      var rs = await this.geoSvc.getWards().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allWards = rs.data;
        this.wards = this.allWards.filter(w => w.districtId == this.fulfillment.address.districtId)
      }
    }
    else{
      this.wards = this.allWards.filter(w => w.districtId == this.fulfillment.address.districtId)
    }
  }

  private async employeeFocusIn(event: any) {
    if(this.managers == null)
    {
      var rs = await this.empSvc.gets().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.managers = rs.data;
      }
    }
    else if(this.managers.length == 1 && this.managers[0].id == this.fulfillment.managerId)
    {
      var rs = await this.empSvc.gets().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.managers = rs.data;
      }
    }
  }

  private checkValid() : boolean{
    return this.isNameValid && this.isManagerValid && this.isAddressWardValid &&
           this.isAddressStreetValid && this.isAddressCountryValid &&
           this.isAddressProvinceValid && this.isAddressDistrictValid &&
           this.isContactNameValid && this.isContactPhoneValid &&
           this.isContactEmailValid && this.isGenderValid;
  }

  private isOpenMap: boolean = false;
  private openMap() {
    this.isOpenMap = true;
  }

  private mapClick(event: any) {
    this.fulfillment.address.longitude = event.coords.lng;
    this.fulfillment.address.latitude = event.coords.lat;
  }
}
