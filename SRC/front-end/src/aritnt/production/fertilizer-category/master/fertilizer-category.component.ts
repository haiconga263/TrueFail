import { Component, OnInit, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { BaseImplementorComponent } from 'src/core/build-implementor/base-implementor.component';
import { appUrl } from '../../app-url';
import { HttpConfig, MethodTypes } from 'src/core/build-implementor/implementor.service';
import { FertilizerCategory, FertilizerCategoryService } from 'src/aritnt/common/services/fertilizer-category.service';

@Component({
  selector: 'fertilizer-category',
  templateUrl: '../../../../core/build-implementor/treeview-base-implementor.component.html',
  styleUrls: ['../../../../core/build-implementor/base-implementor.component.css']
})
export class FertilizerCategoryComponent extends BaseImplementorComponent {
  constructor(
    injector: Injector,
    private fertilizerCategoryService: FertilizerCategoryService
  ) {
    super(injector);
    this.getAllHttp = new HttpConfig({ name: 'gets/all', method: MethodTypes.get });
    this.deleteHttp = new HttpConfig({ name: 'delete', method: MethodTypes.post, nameField: 'id' });

    this.setUrlApiRoot(appUrl.apiFertilizerCategory);
    this.setUrlDetail(appUrl.fertilizerCategoryDetail);

  }

  fertilizerCategories: FertilizerCategory[];

  async init() {
    this.fertilizerCategories = (await this.fertilizerCategoryService.getsAll().toPromise()).data;

    this.configColumns(
      { dataField: 'id', dataType: 'string', alignment: 'center', width: 100,  },
      { dataField: 'code', dataType: 'string' },
      { dataField: 'name', caption: 'Name', dataType: 'string' },
      {
        dataField: 'parentId',
        sortOrder: 'asc',
        caption: 'Parent',
        lookup: {
          dataSource: this.fertilizerCategories,
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
