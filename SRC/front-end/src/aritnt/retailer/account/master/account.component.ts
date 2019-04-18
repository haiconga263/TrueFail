import { Component, OnInit, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { Retailer, AccountService } from '../account.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from 'src/aritnt/retailer/app-url';
import { AppConsts } from 'src/core/constant/AppConsts';
import { GeoService, Ward } from '../../common/services/geo.service';

@Component({
  selector: 'account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent extends AppBaseComponent {

  private account: Retailer = new Retailer();
  private accountWard: Ward = null;

  constructor(
    private accSvc: AccountService,
    private geoSvc: GeoService,
    injector: Injector
  ) {
    super(injector);

    this.Init();
  }

  private Init() {
    this.accSvc.get(this.authenticSvc.getSession().id).subscribe(rRs => {
      if(rRs.result == ResultCode.Success)
      {
        this.account = rRs.data;
        this.account.imageURL = AppConsts.imageDataUrl + this.account.imageURL;

        this.geoSvc.getWard(this.account.address.wardId).subscribe(rs => {
          if(rs.result == ResultCode.Success) {
            this.accountWard = rs.data;
          }
        });
      }
    });
  }

  private inforUpdate() {
    this.router.navigate([appUrl.accountUpdate]);
  }

  private paymentUpdate() {
    alert("Chức năng chưa hoàn thiện");
    // this.router.navigate([appUrl.accountUpdate]);
  }
}
