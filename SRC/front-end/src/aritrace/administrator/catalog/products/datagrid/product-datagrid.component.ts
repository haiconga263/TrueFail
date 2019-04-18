import { Component, OnInit, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { AppUrlConsts } from 'src/aritrace/common/app-constants';
import { BaseImplementorComponent } from 'src/core/build-implementor/base-implementor.component';
import { Category } from 'src/aritrace/common/models/category.model';
import { CommonService } from 'src/aritrace/common/services/common.service';

@Component({
  selector: 'country-datagrid',
  templateUrl: '../../../../../core/build-implementor/base-implementor.component.html',
  styleUrls: ['../../../../../core/build-implementor/base-implementor.component.css']
})
export class ProductDatagridComponent extends BaseImplementorComponent {

  categories: Category[];

  constructor(
    injector: Injector,
    public commonSvc: CommonService,
  ) {
    super(injector);
    this.setUrlApiRoot(AppUrlConsts.urlApiProduct);
    this.setUrlDetail(AppUrlConsts.urlProductDetail);

  }

  async init() {
    this.categories = (await this.commonSvc.getCategories()).data;

    this.configColumns(
      { dataField: 'id', dataType: 'string' },
      { dataField: 'code', dataType: 'string' },
      { dataField: 'defaultName', dataType: 'string' },
      {
        dataField: 'categoryId',
        dataType: 'string',
        caption: 'Category',
        lookup: {
          dataSource: this.categories,
          displayExpr: 'name',
          valueExpr: 'id',
        },
      },
      { dataField: 'defaultDecription', dataType: 'string' },
      { dataField: 'isUsed', dataType: 'boolean' },
    );

    this.initialize();
  }

  ngOnInit() {
    super.ngOnInit();
    this.init();
  }
}
