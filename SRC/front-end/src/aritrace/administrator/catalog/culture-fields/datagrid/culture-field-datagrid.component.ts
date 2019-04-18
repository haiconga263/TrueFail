import { Component, Injector } from '@angular/core';
import { BaseImplementorComponent } from 'src/core/build-implementor/base-implementor.component';
import { AppUrlConsts } from 'src/aritrace/common/app-constants';

@Component({
  selector: 'cultureField',
  templateUrl: '../../../../../core/build-implementor/base-implementor.component.html',
  styleUrls: ['../../../../../core/build-implementor/base-implementor.component.css']
})
export class CultureFieldDatagridComponent extends BaseImplementorComponent {
  constructor(
    injector: Injector
  ) {
    super(injector);

    this.setUrlApiRoot(AppUrlConsts.urlApiCultureField);
    this.setUrlDetail(AppUrlConsts.urlCultureFieldDetail);

    this.configColumns(
      {
        dataField: 'id',
        dataType: 'string',
        width: 100,
        sortOrder: 'asc',
        alignment: 'center',
        formItem: { visible: false },
      },
      { dataField: 'codeName', dataType: 'string' },
      { dataField: 'name', dataType: 'string' },
      { dataField: 'dataType', dataType: 'string', },
      { dataField: 'minimum', dataType: 'string', },
      { dataField: 'maximum', dataType: 'string', },
      { dataField: 'isRequired', dataType: 'boolean', },
      { dataField: 'source', dataType: 'string', },
      { dataField: 'description', dataType: 'string', },
      { dataField: 'isUsed', dataType: 'boolean', },
    );

    this.initialize();
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
