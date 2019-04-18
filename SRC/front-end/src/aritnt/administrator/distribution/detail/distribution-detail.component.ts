import { Component, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { DistributionService, Distribution } from '../distribution.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from '../../app-url';
import { ActivatedRoute, Params } from '@angular/router';
import { ResultModel } from 'src/core/models/http.model';
import { AppConsts } from 'src/core/constant/AppConsts'
import { GeoService, Country, Province, District, Ward } from '../../geographical/geo.service';
import { Employee, EmployeeService } from '../../employee/employee.service';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { DistributionEmployeeService, DistributionEmployee } from '../distribution-employee.service';
import { UserService } from '../../user/user.service';
import { User } from 'src/aritnt/collection/common/services/user.service';

@Component({
  selector: 'distribution-detail',
  templateUrl: './distribution-detail.component.html',
  styleUrls: ['./distribution-detail.component.css']
})
export class DistributionDetailComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource: ArrayStore;
  selectedRows: number[] = [];
  
  private params: Params;
  private type: string = 'add';
  private id: number = 0;

  private distribution: Distribution = new Distribution();
  private genders: any[] = [];

  private employees: Employee[] = null;
  private managers: Employee[] = null;
  private users: User[] = [];

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

  private centerLong: number = 0;
  private centerLat: number = 0;

  constructor(
    injector: Injector,
    private distSvc: DistributionService,
    private disEmployeeSvc: DistributionEmployeeService,
    private empSvc: EmployeeService,
    private geoSvc: GeoService,
    private useSvc: UserService,
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
    let useRs = await this.useSvc.getUsersWithRole(2).toPromise(); //internal
    if(useRs.result == ResultCode.Success) {
      this.users = useRs.data;
    }
    var rs = await this.empSvc.gets().toPromise();
    if(rs.result == ResultCode.Success)
    {
      this.employees = rs.data.filter(m => this.users.find(u => u.id ==m.userId && u.roles.find(r => r.name == "DeliverySupervisor" || r.name == "DeliveryMan" || r.name == "DeliveryDriver") != null) != null);
      this.managers = rs.data.filter(m => this.users.find(u => u.id ==m.userId && u.roles.find(r => r.name == "DeliverySupervisor") != null) != null);
    }
    this.genders = this.distSvc.getGenders();
    if (this.params['type'] == 'update') {
      await this.loadDistributionSource();
      await this.loadEmployeeSource();

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
          this.provinces = this.allProvinces.filter(p => p.countryId == this.distribution.address.countryId);
        }
      });

      this.geoSvc.getDistricts().subscribe((result) => {
        if(result.result == ResultCode.Success)
        {
          this.allDistricts = result.data;
          this.districts = this.allDistricts.filter(d => d.provinceId == this.distribution.address.provinceId);
        }
      });

      this.geoSvc.getWards().subscribe((result) => {
        if(result.result == ResultCode.Success)
        {
          this.allWards = result.data;
          this.wards = this.allWards.filter(w => w.districtId == this.distribution.address.districtId);
        }
      });
    }
  }

  private async loadDistributionSource() {

    var rs = await this.distSvc.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.distribution = rs.data;
      this.distribution.imageData = AppConsts.imageDataUrl + this.distribution.imageURL;

      if(this.distribution.managerId != null && this.distribution.managerId != 0 && this.managers == null)
      {
        this.empSvc.get(this.distribution.managerId).subscribe((result) => {
          if(rs.result == ResultCode.Success)
          {
            this.managers = [];
            this.managers.push(result.data);
          }
        });
      }
    }
  }

  private async loadEmployeeSource() {
    var empRs = await this.disEmployeeSvc.gets(this.id).toPromise();
    if (empRs.result == ResultCode.Success) {
      this.dataSource = new ArrayStore({
        key: "id",
        data: empRs.data
      });
    }
  }

  avatarChangeEvent(fileInput: any, component: DistributionDetailComponent) {
    if (fileInput.target.files && fileInput.target.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e: any) {
            component.distribution.imageData = e.target.result;
        }
        reader.readAsDataURL(fileInput.target.files[0]);
    }
  }

  private return() {
    console.log('return');
    this.router.navigate([appUrl.distributionList]);
  }

  private refresh() {
    this.loadDistributionSource();
  }

  private save() {
    console.log('save');
    console.log(this.distribution);
    if (!this.checkValid()) {
      return;
    }

    if(this.type == "update")
    {
      this.distSvc.update(this.distribution).subscribe((result: ResultModel<any>) => {
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
        this.distSvc.add(this.distribution).subscribe((result: ResultModel<any>) => {
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
    else if(this.distribution.address != null && this.allCountries.length == 1 && this.allCountries[0].id == this.distribution.address.countryId)
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
        this.provinces = this.allProvinces.filter(p => p.countryId == this.distribution.address.countryId);
      }
    }
    else if(this.distribution.address != null && this.allProvinces.length == 1 && this.allProvinces[0].id == this.distribution.address.provinceId)
    {
      var rs = await this.geoSvc.getProvinces().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allProvinces = rs.data;
        this.provinces = this.allProvinces.filter(p => p.countryId == this.distribution.address.countryId);
      }
    }
    else
    {
      this.provinces = this.allProvinces.filter(p => p.countryId == this.distribution.address.countryId);
    }
  }

  private async districtFocusIn(event: any) {
    if(this.allDistricts == null)
    {
      var rs = await this.geoSvc.getDistricts().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allDistricts = rs.data;
        this.districts = this.allDistricts.filter(d => d.provinceId == this.distribution.address.provinceId);
      }
    }
    else if(this.distribution.address != null && this.allDistricts.length == 1 && this.allDistricts[0].id == this.distribution.address.provinceId)
    {
      var rs = await this.geoSvc.getDistricts().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allDistricts = rs.data;
        this.districts = this.allDistricts.filter(d => d.provinceId == this.distribution.address.provinceId);
      }
    }
    else
    {
      this.districts = this.allDistricts.filter(d => d.provinceId == this.distribution.address.provinceId);
    }
  }

  private async wardFocusIn(event: any) {
    if(this.allWards == null)
    {
      var rs = await this.geoSvc.getWards().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allWards = rs.data;
        this.wards = this.allWards.filter(w => w.districtId == this.distribution.address.districtId);
      }
    }
    else if(this.distribution.address != null && this.allWards.length == 1 && this.allWards[0].id == this.distribution.address.provinceId)
    {
      var rs = await this.geoSvc.getWards().toPromise();
      if(rs.result == ResultCode.Success)
      {
        this.allWards = rs.data;
        this.wards = this.allWards.filter(w => w.districtId == this.distribution.address.districtId)
      }
    }
    else{
      this.wards = this.allWards.filter(w => w.districtId == this.distribution.address.districtId)
    }
  }

  private async employeeFocusIn(event: any) {
    // if(this.managers == null)
    // {
    //   var rs = await this.empSvc.gets().toPromise();
    //   if(rs.result == ResultCode.Success)
    //   {
    //     this.managers = rs.data.filter(m => this.users.find(u => u.id ==m.userId && u.roles.find(r => r.name == "DeliverySupervisor") != null) != null);
    //   }
    // }
    // else if(this.managers.length == 1 && this.managers[0].id == this.distribution.managerId)
    // {
    //   var rs = await this.empSvc.gets().toPromise();
    //   if(rs.result == ResultCode.Success)
    //   {
    //     this.managers = rs.data.filter(m => this.users.find(u => u.id ==m.userId && u.roles.find(r => r.name == "DeliverySupervisor") != null) != null);
    //   }
    // }
  }

  private checkValid() : boolean{
    return this.isNameValid && this.isManagerValid && this.isAddressWardValid &&
           this.isAddressStreetValid && this.isAddressCountryValid &&
           this.isAddressProvinceValid && this.isAddressDistrictValid &&
           this.isContactNameValid && this.isContactPhoneValid &&
           this.isContactEmailValid && this.isGenderValid;
  }

  grid= {
    create: () => {
      this.dataGrid.instance.addRow();
    },
    createProcess: async (event: any) => {
      event.cancel = true;
      console.log(event);
      let emp = new DistributionEmployee();
      emp.distributionId = this.distribution.id;
      emp.employeeId = event.data.employeeId;
      this.disEmployeeSvc.add(emp).subscribe(rs => {
        if(rs.result == ResultCode.Success) {
          this.showSuccess("Common.AddSuccess");
          this.dataGrid.instance.cancelEditData();
          this.loadEmployeeSource();
        }
        else {
          this.showError(rs.errorMessage);
        }
      });
    },
    update: () => {
      if(this.selectedRows.length == 1) {
        let rowIdx = this.dataGrid.instance.getRowIndexByKey(this.selectedRows[0]);
        this.dataGrid.instance.editRow(rowIdx);
      }
    },
    updateProcess: (event: any) => {
      event.cancel = true;
      this.dataSource.byKey(this.selectedRows[0]).then(emp => {
        this.disEmployeeSvc.update(emp).subscribe(rs => {
          if(rs.result == ResultCode.Success) {
            this.showSuccess("Common.UpdateSuccess");
            event.cancel = false;
          }
          else {
            this.showError(rs.errorMessage);
          }
        });
      });
    },
    delete: () => {
      if(this.selectedRows.length == 1) {
       this.showYesNoQuestion("Common.ConfirmDeleteMessag", () =>{
        this.disEmployeeSvc.delete(this.selectedRows[0]).subscribe(rs => {
          if(rs.result == ResultCode.Success) {
            this.showSuccess("Common.DeleteSuccess");
            this.dataSource.remove(this.selectedRows[0]);
            this.dataGrid.instance.refresh();
          }
          else {
            this.showError(rs.errorMessage);
          }
        });
       }); 
      }
    },
    refresh: () => {
      this.loadEmployeeSource();
    },
  }

  private isOpenMap: boolean = false;
  private openMap() {
    console.log(this.distribution.address);
    this.isOpenMap = true;
    this.centerLong = FuncHelper.clone(this.distribution.address.longitude);
    this.centerLat = FuncHelper.clone(this.distribution.address.latitude);
  }

  private centerChange(event) {
    this.distribution.address.longitude = event.lng;
    this.distribution.address.latitude = event.lat;
  }

  private radiusChange(event) {
    this.distribution.radius = event;
  }
}
