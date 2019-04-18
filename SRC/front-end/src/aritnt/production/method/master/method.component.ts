import { Component, OnInit, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { BaseImplementorComponent } from 'src/core/build-implementor/base-implementor.component';
import { appUrl } from '../../app-url';
import { HttpConfig, MethodTypes } from 'src/core/build-implementor/implementor.service';
import { Method, MethodService } from 'src/aritnt/common/services/method.service';

@Component({
  selector: 'method',
  templateUrl: '../../../../core/build-implementor/base-implementor.component.html',
  styleUrls: ['../../../../core/build-implementor/base-implementor.component.css']
})
export class MethodComponent extends BaseImplementorComponent {
  constructor(
    injector: Injector,
    private methodService: MethodService,
  ) {
    super(injector);
    this.getAllHttp = new HttpConfig({ name: 'gets/all', method: MethodTypes.get });
    this.deleteHttp = new HttpConfig({ name: 'delete', method: MethodTypes.post, nameField: 'id' });
    this.setUrlApiRoot(appUrl.apiMethod);
    this.setUrlDetail(appUrl.methodDetail);

  }

  async init() {
    this.configColumns(
      { dataField: 'id', dataType: 'string', alignment: 'center', width: 100, },
      { dataField: 'code', dataType: 'string' },
      { dataField: 'name', caption: 'Name', dataType: 'string' },
      { dataField: 'isUsed', dataType: 'boolean' },
    );

    this.initialize();
  }

  ngOnInit() {
    super.ngOnInit();
    this.init();
  }
}
