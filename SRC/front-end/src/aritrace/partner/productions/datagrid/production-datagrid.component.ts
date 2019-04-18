import { Component, OnInit, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { AppUrlConsts } from 'src/aritrace/common/app-constants';
import { BaseImplementorComponent } from 'src/core/build-implementor/base-implementor.component';
import { GrowingMethod } from 'src/aritrace/common/models/growing-method.model';
import { Product } from 'src/aritrace/common/models/product.model';
import { ProductService } from 'src/aritrace/common/services/product.service';
import { GrowingMethodService } from 'src/aritrace/common/services/growing-method.service';
import { ResultCode } from 'src/core/constant/AppEnums';

@Component({
  selector: 'country-datagrid',
  templateUrl: '../../../../core/build-implementor/base-implementor.component.html',
  styleUrls: ['../../../../core/build-implementor/base-implementor.component.css']
})
export class ProductionDatagridComponent extends BaseImplementorComponent {

  products: Product[];
  growingMethods: GrowingMethod[];

  constructor(
    injector: Injector,
    public productSvc: ProductService,
    public growingMethodSvc: GrowingMethodService
  ) {
    super(injector);

    this.setUrlApiRoot(AppUrlConsts.urlApiProduction);
    this.setUrlDetail(AppUrlConsts.urlProductionDetail);
  }

  async init() {
    await this.productSvc.getList().then((rs) => {
      if (rs.result < ResultCode.Success)
        this.alertSvc.alertByHttpResult(rs);
      else this.products = rs.data;
    });

    await this.growingMethodSvc.getList().then((rs) => {
      if (rs.result < ResultCode.Success)
        this.alertSvc.alertByHttpResult(rs);
      else {
        this.growingMethods = rs.data;
        this.growingMethods.unshift(new GrowingMethod({
          id: null,
          name: 'none'
        }));
      }
    });

    this.configColumns(
      { dataField: 'id', dataType: 'string', alignment: 'center', },
      { dataField: 'name', dataType: 'string' },
      {
        dataField: 'productId', dataType: 'string',
        lookup: {
          dataSource: this.products,
          displayExpr: 'defaultName',
          valueExpr: 'id',
        },
      },
      { dataField: 'gtin.code', dataType: 'string', caption: 'GTIN Code' },
      {
        dataField: 'growingMethodId', dataType: 'string',
        caption:'Growing Method',
        lookup: {
          dataSource: this.growingMethods,
          displayExpr: 'name',
          valueExpr: 'id',
        },
      },
      { dataField: 'countryOfOrigin', dataType: 'string' },
      { dataField: 'trademark', dataType: 'string' },
      //{ dataField: 'commercialClaim', dataType: 'string' },
      //{ dataField: 'productSize', dataType: 'string' },
      //{ dataField: 'grade', dataType: 'string' },
      //{ dataField: 'colour', dataType: 'string' },
      //{ dataField: 'shape', dataType: 'string' },
      //{ dataField: 'variety', dataType: 'string' },
      //{ dataField: 'commercialType', dataType: 'string' },
      //{ dataField: 'colourOfFlesh', dataType: 'string' },
      //{ dataField: 'postHarvestTreatment', dataType: 'string' },
      //{ dataField: 'postHarvestProcessing', dataType: 'string' },
      //{ dataField: 'cookingType', dataType: 'string' },
      //{ dataField: 'seedProperties', dataType: 'string' },
      //{ dataField: 'tradePackageContentQuantity', dataType: 'string' },
      //{ dataField: 'tradeUnitPackageType', dataType: 'string' },
      //{ dataField: 'consumerUnitContentQuantity', dataType: 'string' },
      //{ dataField: 'tradeUnit', dataType: 'string' },
      //{ dataField: 'comsumerUnitPackageType', dataType: 'string' },
      //{ dataField: 'comsumerUnit', dataType: 'string' },
      //{ dataField: 'productionImageId', dataType: 'string' },
      { dataField: 'isUsed', dataType: 'boolean' },
      //{ dataField: 'isDeleted', dataType: 'string' },
      //{ dataField: 'createdDate', dataType: 'string' },
      //{ dataField: 'createdBy', dataType: 'string' },
      //{ dataField: 'modifiedDate', dataType: 'string' },
      //{ dataField: 'modifiedBy', dataType: 'string' },
    );

    this.initialize();
  }

  ngOnInit() {
    super.ngOnInit();
    this.init();
  }
}
