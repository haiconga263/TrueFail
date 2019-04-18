import { Component, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { RetailerService, Retailer } from '../../retailer.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from '../../../app-url';
import { ActivatedRoute, Params } from '@angular/router';
import { ResultModel } from 'src/core/models/http.model';
import { AppConsts } from 'src/core/constant/AppConsts'
import { User, UserService } from '../../../user/user.service';
import { Country, GeoService, District, Ward, Province } from 'src/aritnt/administrator/geographical/geo.service';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'

@Component({
  selector: 'retailer-detail',
  templateUrl: './retailer-detail.component.html',
  styleUrls: ['./retailer-detail.component.css']
})
export class RetailerDetailComponent extends AppBaseComponent {
  private params: Params;
  private type: string = 'add';
  private id: number = 0;

  private retailer: Retailer = new Retailer();
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
  private isTaxCodeValid: boolean = false;

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

  private isOpenMap: boolean = false;

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
          this.provinces = this.allProvinces.filter(p => p.countryId == this.retailer.address.countryId);
        }
      });

      this.geoSvc.getDistricts().subscribe((result) => {
        if(result.result == ResultCode.Success)
        {
          this.allDistricts = result.data;
          this.districts = this.allDistricts.filter(d => d.provinceId == this.retailer.address.provinceId);
        }
      });

      this.geoSvc.getWards().subscribe((result) => {
        if(result.result == ResultCode.Success)
        {
          this.allWards = result.data;
          this.wards = this.allWards.filter(w => w.districtId == this.retailer.address.districtId);
        }
      });
    }
  }

  private async loadDatasource() {

    var rs = await this.retSvc.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.retailer = rs.data;
      this.retailer.imageData = AppConsts.imageDataUrl + this.retailer.imageURL;

      this.retSvc.getLocations(this.retailer.id).subscribe((result) => {
        if(result.result == ResultCode.Success)
        {
          this.dataSource = new ArrayStore({
            key: "id",
            data: result.data
          });
        }
        else
        {
          this.dataSource = new ArrayStore({
            key: "id",
            data: []
          });
        }
      });

      if(this.retailer.userId != null && this.retailer.userId != 0 && this.users == [])
      {
        this.useSvc.getUser(this.retailer.userId).subscribe((result) => {
          if(rs.result == ResultCode.Success)
          {
            this.users = [];
            this.users.push(result.data);
          }
        });
      }
    }
  }
  avatarChangeEvent(fileInput: any, component: RetailerDetailComponent) {
    if (fileInput.target.files && fileInput.target.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e: any) {
            component.retailer.imageData = e.target.result;
        }
        reader.readAsDataURL(fileInput.target.files[0]);
    }
  }

  private return() {
    console.log('return');
    this.router.navigate([appUrl.retailerList]);
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    console.log('save');
    console.log(this.retailer);
    if (!this.checkValid()) {
      return;
    }

    if(this.type == "update")
    {
      this.retSvc.update(this.retailer).subscribe((result: ResultModel<any>) => {
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
        this.retSvc.add(this.retailer).subscribe((result: ResultModel<any>) => {
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
    else if(this.retailer.address != null && this.allCountries.length == 1 && this.allCountries[0].id == this.retailer.address.countryId)
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
        this.provinces = this.allProvinces.filter(p => p.countryId == this.retailer.address.countryId);
      }
    }
    else if(this.retailer.address != null && this.allProvinces.length == 1 && this.allProvinces[0].id == this.retailer.address.provinceId)
    {
      var rs = await this.geoSvc.getProvinces().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allProvinces = rs.data;
        this.provinces = this.allProvinces.filter(p => p.countryId == this.retailer.address.countryId);
      }
    }
    else
    {
      this.provinces = this.allProvinces.filter(p => p.countryId == this.retailer.address.countryId);
    }
  }

  private async districtFocusIn(event: any) {
    if(this.allDistricts == null)
    {
      var rs = await this.geoSvc.getDistricts().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allDistricts = rs.data;
        this.districts = this.allDistricts.filter(d => d.provinceId == this.retailer.address.provinceId);
      }
    }
    else if(this.retailer.address != null && this.allDistricts.length == 1 && this.allDistricts[0].id == this.retailer.address.provinceId)
    {
      var rs = await this.geoSvc.getDistricts().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allDistricts = rs.data;
        this.districts = this.allDistricts.filter(d => d.provinceId == this.retailer.address.provinceId);
      }
    }
    else
    {
      this.districts = this.allDistricts.filter(d => d.provinceId == this.retailer.address.provinceId);
    }
  }

  private async wardFocusIn(event: any) {
    if(this.allWards == null)
    {
      var rs = await this.geoSvc.getWards().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allWards = rs.data;
        this.wards = this.allWards.filter(w => w.districtId == this.retailer.address.districtId);
      }
    }
    else if(this.retailer.address != null && this.allWards.length == 1 && this.allWards[0].id == this.retailer.address.provinceId)
    {
      var rs = await this.geoSvc.getWards().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allWards = rs.data;
        this.wards = this.allWards.filter(w => w.districtId == this.retailer.address.districtId)
      }
    }
    else{
      this.wards = this.allWards.filter(w => w.districtId == this.retailer.address.districtId)
    }
  }

  private async userFocusIn(event: any) {
    if(this.users == [])
    {
      var rs = await this.useSvc.getUsersNotAssignBy(true, 'Retailer').toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.users = rs.data;
      }
    }
    else if(this.users.length == 1 && this.users[0].id == this.retailer.userId)
    {
      var currentUser = this.users[0];
      var rs = await this.useSvc.getUsersNotAssignBy(true, 'Retailer').toPromise();
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
           this.isContactEmailValid && this.isTaxCodeValid;
  }


  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource: ArrayStore;
  selectedRows: number[];
  
  grid = {
    delete: async () => {
      console.log('delete');
      let rs = await this.retSvc.deleteLocation(this.selectedRows[0]).toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.dataGrid.instance.removeRow(this.dataGrid.instance.getRowIndexByKey(this.selectedRows[0]));
      }
      else
      {
        //alert
      }
    },
    update: () => {
      console.log('update');
      if(this.selectedRows.length == 1)
      {
        this.router.navigate([appUrl.retailerLocation],
        {
          queryParams: {
            type: 'update',
            id: this.selectedRows[0],
            retailerId: this.retailer.id
          }
        });
      }
    },
    create: () => {
      console.log('create');
      this.router.navigate([appUrl.retailerLocation],
      {
        queryParams: {
          type: 'add',
          retailerId: this.retailer.id
        }
      });
    }
  };

  private openMap() {
    this.isOpenMap = true;
  }

  private mapClick(event: any) {
    this.retailer.address.longitude = event.coords.lng;
    this.retailer.address.latitude = event.coords.lat;
  }

  private isCompanyChanged(event) {
    console.log(event);
    if(event.value == true) {
      if(this.retailer.taxCode == null || this.retailer.taxCode == "") {
        this.isTaxCodeValid = false;
      }
    }
    else {
      this.isTaxCodeValid = true;
      this.retailer.taxCode = "";
    }
  }
}
