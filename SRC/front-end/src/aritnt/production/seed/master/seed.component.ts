import { Component, OnInit, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { BaseImplementorComponent } from 'src/core/build-implementor/base-implementor.component';
import { appUrl } from 'src/aritnt/production/app-url';
import { HttpConfig, MethodTypes } from 'src/core/build-implementor/implementor.service';
import { Seed, SeedService } from 'src/aritnt/common/services/seed.service';
import { ProductService, Product } from 'src/aritnt/common/services/product.service';

@Component({
  selector: 'seed',
  templateUrl: '../../../../core/build-implementor/base-implementor.component.html',
  styleUrls: ['../../../../core/build-implementor/base-implementor.component.css']
})
export class SeedComponent extends BaseImplementorComponent {
  constructor(
    injector: Injector,
    private seedService: SeedService,
    private productService: ProductService,
  ) {
    super(injector);
    this.getAllHttp = new HttpConfig({ name: 'gets/all', method: MethodTypes.get });
    this.deleteHttp = new HttpConfig({ name: 'delete', method: MethodTypes.post, nameField: 'id' });
    this.setUrlApiRoot(appUrl.apiSeed);
    this.setUrlDetail(appUrl.seedDetail);

  }

  products: Product[];

  async init() {
    this.products = (await this.productService.getsOnly().toPromise()).data;

    this.configColumns(
      { dataField: 'id', dataType: 'string', alignment: 'center', width: 100, },
      { dataField: 'code', dataType: 'string' },
      { dataField: 'name', caption: 'Name', dataType: 'string' },
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
