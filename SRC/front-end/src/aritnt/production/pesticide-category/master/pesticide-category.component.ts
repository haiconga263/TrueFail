import { Component, OnInit, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { BaseImplementorComponent } from 'src/core/build-implementor/base-implementor.component';
import { appUrl } from '../../app-url';
import { HttpConfig, MethodTypes } from 'src/core/build-implementor/implementor.service';
import { PesticideCategory, PesticideCategoryService } from 'src/aritnt/common/services/pesticide-category.service';

@Component({
  selector: 'pesticide-category',
  templateUrl: '../../../../core/build-implementor/treeview-base-implementor.component.html',
  styleUrls: ['../../../../core/build-implementor/base-implementor.component.css']
})
export class PesticideCategoryComponent extends BaseImplementorComponent {
  constructor(
    injector: Injector,
    private pesticideCategoryService: PesticideCategoryService
  ) {
    super(injector);
    this.getAllHttp = new HttpConfig({ name: 'gets/all', method: MethodTypes.get });
    this.deleteHttp = new HttpConfig({ name: 'delete', method: MethodTypes.post, nameField: 'id' });

    this.setUrlApiRoot(appUrl.apiPesticideCategory);
    this.setUrlDetail(appUrl.pesticideCategoryDetail);

  }

  pesticideCategories: PesticideCategory[];

  async init() {
    this.pesticideCategories = (await this.pesticideCategoryService.getsAll().toPromise()).data;

    this.configColumns(
      { dataField: 'id', dataType: 'string', alignment: 'center', width: 100,  },
      { dataField: 'code', dataType: 'string' },
      { dataField: 'name', caption: 'Name', dataType: 'string' },
      {
        dataField: 'parentId',
        sortOrder: 'asc',
        caption: 'Parent',
        lookup: {
          dataSource: this.pesticideCategories,
          displayExpr: 'name',
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
