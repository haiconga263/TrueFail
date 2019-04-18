import { Component, Injector } from '@angular/core';
import { BaseImplementorComponent } from 'src/core/build-implementor/base-implementor.component';
import { AppUrlConsts } from 'src/aritrace/common/app-constants';
import { Country } from 'src/aritrace/common/models/country.model';
import { CommonService } from 'src/aritrace/common/services/common.service';
import { Province } from 'src/aritrace/common/models/province.model';
import { Caption } from 'src/aritrace/common/models/caption.model';

@Component({
  selector: 'caption',
  templateUrl: '../../../../../../core/build-implementor/base-implementor.component.html',
  styleUrls: ['../../../../../../core/build-implementor/base-implementor.component.css']
})
export class CaptionDatagridComponent extends BaseImplementorComponent {

  constructor(
    injector: Injector,
    public commonSvc: CommonService,
  ) {
    super(injector);
    this.setUrlApiRoot(AppUrlConsts.urlApiCaption);
    this.setUrlDetail(AppUrlConsts.urlCaptionDetail);
  }

  async init() {

    this.configColumns(
      {
        dataField: 'id',
        dataType: 'string',
        width: 100,
        sortOrder: 'asc',
        alignment: 'center',
        formItem: { visible: false },
      },
      {
        dataField: 'name',
        dataType: 'string',
        validationRules: [{
          type: 'required',
          message: 'Name is required'
        }]
      },
      {
        dataField: 'defaultCaption',
        dataType: 'string',
        validationRules: [{
          type: 'required',
          message: 'Default Caption is required'
        }]
      },
      {
        dataField: 'isCommon',
        dataType: 'boolean',
      },
      {
        dataField: 'isUsed',
        dataType: 'boolean',
      }
    );

    this.initialize();
  }

  ngOnInit() {
    super.ngOnInit();
    this.init();
  }
}
