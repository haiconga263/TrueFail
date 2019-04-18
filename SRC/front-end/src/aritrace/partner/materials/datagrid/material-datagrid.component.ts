import { Component, OnInit, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { AppUrlConsts } from 'src/aritrace/common/app-constants';
import { BaseImplementorComponent } from 'src/core/build-implementor/base-implementor.component';
import { Category } from 'src/aritrace/common/models/category.model';
import { CommonService } from 'src/aritrace/common/services/common.service';
import { Product } from 'src/aritrace/common/models/product.model';

@Component({
  selector: 'country-datagrid',
  templateUrl: '../../../../core/build-implementor/base-implementor.component.html',
  styleUrls: ['../../../../core/build-implementor/base-implementor.component.css']
})
export class MaterialDatagridComponent extends BaseImplementorComponent {

  products: Product[];

  constructor(
    injector: Injector,
    public commonSvc: CommonService,
  ) {
    super(injector);
    this.setUrlApiRoot(AppUrlConsts.urlApiMaterial);
    this.setUrlDetail(AppUrlConsts.urlMaterialDetail);

  }

  async init() {
    this.products = (await this.commonSvc.getProducts()).data;

    this.configColumns(
      { dataField: 'id', dataType: 'string', alignment: 'center', width: 100, },
      { dataField: 'code', dataType: 'string' },
      { dataField: 'name', caption: 'Name', dataType: 'string' },
      { dataField: 'description', caption: 'Description', dataType: 'string' },
      {
        dataField: 'productId',
        caption: 'Product',
        lookup: {
          dataSource: this.products,
          displayExpr: 'defaultName',
          valueExpr: 'id',
        },
      },
      { dataField: 'isUsed', dataType: 'boolean' },
    );

    this.initialize();
  }

  ngOnInit() {
    super.ngOnInit();
    this.init();
  }
}
