import { Component, OnInit, Input, Injector, ViewChild } from '@angular/core';
import { BaseComponent } from 'src/core/basecommon/base.component';
import { ServerDisconnectedService } from './server-disconnected.service';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { AppConsts, UrlConsts } from 'src/core/constant/AppConsts';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';

@Component({
  selector: 'app-server-disconnected',
  templateUrl: './server-disconnected.component.html',
  styleUrls: ['./server-disconnected.component.css']
})
export class ServerDisconnectedComponent extends AppBaseComponent {
  isError: boolean = false;
  msg: string;

  constructor(injector: Injector,
    public authenticSvc: AuthenticService,
    public serverDisconnectedSvc: ServerDisconnectedService) {
    super(injector);
    this.changedStatus = this.changedStatus.bind(this);
    this.serverDisconnectedSvc.changedStatus = this.changedStatus;

  }

  changedStatus(isError: boolean): void {
    this.isError = isError;
  }

  ngOnInit() {
    super.ngOnInit();
    this.msg = 'Server are too busy, please try again later!'; //this.lang.instant('Alert.ServerBusy');
  }
}
