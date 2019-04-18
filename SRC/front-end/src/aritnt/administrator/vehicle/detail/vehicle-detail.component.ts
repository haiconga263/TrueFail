import { Component, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { VehicleService, Vehicle, VehicleType } from '../vehicle.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from '../../app-url';
import { ActivatedRoute, Params } from '@angular/router';
import { ResultModel } from 'src/core/models/http.model';
import { AppConsts } from 'src/core/constant/AppConsts';
import { DistributionService, Distribution } from '../../distribution/distribution.service';

@Component({
  selector: 'vehicle-detail',
  templateUrl: './vehicle-detail.component.html',
  styleUrls: ['./vehicle-detail.component.css']
})
export class VehicleDetailComponent extends AppBaseComponent {
  private params: Params;
  private type: string = 'add';
  private id: number = 0;

  private vehicle: Vehicle = new Vehicle();
  private types: VehicleType[] = [];
  private dises: Distribution[] = [];

  private temperatureTypes: any[] = [];

  private isNameValid: boolean = false;
  private isTypeValid: boolean = false;
  private isTemperatureTypeValid: boolean = false;
  private isDistributionValid: boolean = false;

  //Language
  private modifyLanguages: any[] = [];

  private nonCharPattern = AppConsts.nonSpecialCharPattern;

  constructor(
    injector: Injector,
    private vehcSvc: VehicleService,
    private disSvc: DistributionService,
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
    this.temperatureTypes = this.vehcSvc.getTemperatureTypes();

    this.vehcSvc.getTypes().subscribe(typesRs => { 
      if(typesRs.result == ResultCode.Success)
      {
        this.types = typesRs.data;
      }
    });

    this.disSvc.gets().subscribe(disRs => { 
      if(disRs.result == ResultCode.Success)
      {
        this.dises = disRs.data;
      }
    });

    if (this.params['type'] == 'update') {
      await this.loadDatasource();
    }
  }

  private async loadDatasource() {

    var rs = await this.vehcSvc.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.vehicle = rs.data;
      this.vehicle.imageData = AppConsts.imageDataUrl + this.vehicle.imageURL;
    }
  }
  avatarChangeEvent(fileInput: any, component: VehicleDetailComponent) {
    if (fileInput.target.files && fileInput.target.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e: any) {
            component.vehicle.imageData = e.target.result;
        }
        reader.readAsDataURL(fileInput.target.files[0]);
    }
  }

  private return() {
    console.log('return');
    this.router.navigate([appUrl.vehicleList]);
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    console.log('save');
    console.log(this.vehicle);
    if (!this.checkValid()) {
      return;
    }

    if(this.type == "update")
    {
      this.vehcSvc.update(this.vehicle).subscribe((result: ResultModel<any>) => {
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
        this.vehcSvc.add(this.vehicle).subscribe((result: ResultModel<any>) => {
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

  private checkValid() : boolean{
    return this.isNameValid && this.isTypeValid && this.isTemperatureTypeValid && this.isDistributionValid;
  }


  ngOnInit() {
  }
}
