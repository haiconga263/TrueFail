import { Component, Injector } from '@angular/core';
import { BaseImplementorComponent } from 'src/core/build-implementor/base-implementor.component';
import { AppUrlConsts } from 'src/aritrace/common/app-constants';

@Component({
  selector: 'role',
  templateUrl: '../../../../core/build-implementor/base-implementor.component.html',
  styleUrls: ['../../../../core/build-implementor/base-implementor.component.css']
})
export class RoleComponent extends BaseImplementorComponent {
  constructor(
    injector: Injector
  ) {
    super(injector);

    this.setUrlApiRoot(AppUrlConsts.urlApiRole);
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
        dataField: 'description',
        dataType: 'string',
      },
      {
        dataField: 'isUsed',
        dataType: 'boolean',
      },
    );

    this.initialize();
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
