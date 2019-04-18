import { Component, OnInit, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { BaseImplementorComponent } from 'src/core/build-implementor/base-implementor.component';
import { appUrl } from '../../app-url';
import { HttpConfig, MethodTypes } from 'src/core/build-implementor/implementor.service';
import { Category, CategoryService } from '../category.service';

@Component({
  selector: 'category',
  templateUrl: '../../../../core/build-implementor/treeview-base-implementor.component.html',
  styleUrls: ['../../../../core/build-implementor/base-implementor.component.css']
})
export class CategoryComponent extends BaseImplementorComponent {
  constructor(
    injector: Injector,
    private categoryService: CategoryService
  ) {
    super(injector);
    this.getAllHttp = new HttpConfig({ name: 'gets/all', method: MethodTypes.get });
    this.deleteHttp = new HttpConfig({ name: 'delete', method: MethodTypes.post, nameField: 'categoryId' });
    this.setUrlApiRoot(appUrl.apiCategory);
    this.setUrlDetail(appUrl.categoryDetail);

  }

  categories: Category[];

  async init() {
    this.categories = (await this.categoryService.getsAll().toPromise()).data;

    this.configColumns(
      { dataField: 'id', dataType: 'string', alignment: 'center', width: 100, sortOrder: 'asc', },
      { dataField: 'code', dataType: 'string' },
      { dataField: 'name', caption: 'Name', dataType: 'string' },
      {
        dataField: 'parentId',
        caption: 'Parent',
        lookup: {
          dataSource: this.categories,
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
