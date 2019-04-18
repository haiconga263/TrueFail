import { Component, OnInit, Injector } from '@angular/core';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { UrlConsts } from 'src/core/constant/AppConsts';
import { Router } from '@angular/router';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';

@Component({
  selector: 'page-forbidden',
  templateUrl: './page-forbidden.component.html',
  styleUrls: ['./page-forbidden.component.css']
})
export class PageForbiddenComponent extends AppBaseComponent {

  constructor(injector: Injector
  ) {
    super(injector);
  }

  ngOnInit() {
  }

}
