import { Component, OnInit, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { AppUrlConsts } from 'src/aritrace/common/app-constants';
import { BaseImplementorComponent, Action } from 'src/core/build-implementor/base-implementor.component';
import { CompanyViewModel } from 'src/aritrace/common/models/company.model';
import { Product } from 'src/aritrace/common/models/product.model';
import { ProductionInformation } from 'src/aritrace/common/models/production.model';
import { GrowingMethod } from 'src/aritrace/common/models/growing-method.model';
import { ProductionService } from 'src/aritrace/common/services/production.service';
import { ProductService } from 'src/aritrace/common/services/product.service';
import { CompanyService } from 'src/aritrace/common/services/company.service';
import { GrowingMethodService } from 'src/aritrace/common/services/growing-method.service';
import { ResultCode } from 'src/core/constant/AppEnums';

@Component({
  selector: 'country-datagrid',
  templateUrl: '../../../../core/build-implementor/base-implementor.component.html',
  styleUrls: ['../../../../core/build-implementor/base-implementor.component.css']
})
export class ProcessDatagridComponent extends BaseImplementorComponent {

  companies: CompanyViewModel[];
  productions: ProductionInformation[];
  products: Product[];
  growingMethods: GrowingMethod[];

  constructor(
    injector: Injector,
    public productionSvc: ProductionService,
    public productSvc: ProductService,
    public companySvc: CompanyService,
    public growingMethodSvc: GrowingMethodService
  ) {
    super(injector);

    this.setUrlApiRoot(AppUrlConsts.urlApiProcess);
    this.setUrlDetail(AppUrlConsts.urlProcessDetail);
  }

  async init() {
    await this.productionSvc.getList().then((rs) => {
      if (rs.result < ResultCode.Success)
        this.alertSvc.alertByHttpResult(rs);
      else this.productions = rs.data;
    });

    await this.productSvc.getList().then((rs) => {
      if (rs.result < ResultCode.Success)
        this.alertSvc.alertByHttpResult(rs);
      else this.products = rs.data;
    });

    await this.companySvc.getList().then((rs) => {
      if (rs.result < ResultCode.Success)
        this.alertSvc.alertByHttpResult(rs);
      else {
        this.companies = rs.data;
        this.companies.unshift(new CompanyViewModel({
          id: null,
          name: 'none'
        }));
      }
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
      { dataField: 'id', dataType: 'string' },
      { dataField: 'code', dataType: 'string' },
      // { dataField: 'pointId', dataType: 'string' },
      {
        dataField: 'productionId', dataType: 'string',
        caption: 'Production',
        lookup: {
          dataSource: this.productions,
          displayExpr: 'name',
          valueExpr: 'id',
        },
      },
      {
        dataField: 'productId', dataType: 'string',
        caption: 'Product',
        lookup: {
          dataSource: this.products,
          displayExpr: 'defaultName',
          valueExpr: 'id',
        },
      },
      {
        dataField: 'farmerId', dataType: 'string',
        caption: 'Farmer',
      },
      {
        dataField: 'companyCultivationId', dataType: 'string',
        caption: 'Company Cultivation',
        lookup: {
          dataSource: this.companies,
          displayExpr: 'name',
          valueExpr: 'id',
        },
      },
      {
        dataField: 'companyCollectionId', dataType: 'string',
        caption: 'Company Collection',
        lookup: {
          dataSource: this.companies,
          displayExpr: 'name',
          valueExpr: 'id',
        },
      },
      {
        dataField: 'companyFulfillmentId', dataType: 'string',
        caption: 'Company Fulfillment',
        lookup: {
          dataSource: this.companies,
          displayExpr: 'name',
          valueExpr: 'id',
        },
      },
      {
        dataField: 'companyDistributionId', dataType: 'string',
        caption: 'Company Distribution',
        lookup: {
          dataSource: this.companies,
          displayExpr: 'name',
          valueExpr: 'id',
        },
      },
      {
        dataField: 'companyRetailerId', dataType: 'string',
        caption: 'Company Retailer',
        lookup: {
          dataSource: this.companies,
          displayExpr: 'name',
          valueExpr: 'id',
        },
      },
      { dataField: 'collectionDate', dataType: 'string' },
      { dataField: 'fulfillmentDate', dataType: 'string' },
      { dataField: 'distributionDate', dataType: 'string' },
      { dataField: 'retailerDate', dataType: 'string' },
      { dataField: 'expiryDate', dataType: 'string' },
      { dataField: 'manufacturingDate', dataType: 'string' },
      {
        dataField: 'growingMethodId', dataType: 'string',
        caption: 'Growing Method',
        lookup: {
          dataSource: this.growingMethods,
          displayExpr: 'name',
          valueExpr: 'id',
        },
      },
      { dataField: 'standardExpiryDate', dataType: 'string' },
      { dataField: 'description', dataType: 'string' },
      { dataField: 'quantity', dataType: 'string' },
      { dataField: 'uom', dataType: 'string' },
      { dataField: 'isUsed', dataType: 'boolean' },
      // { dataField: 'createdDate', dataType: 'string' },
      // { dataField: 'createdBy', dataType: 'string' },
      // { dataField: 'modifiedDate', dataType: 'string' },
      // { dataField: 'modifiedBy', dataType: 'string' },
    );

    this.initialize();
  }

  ngOnInit() {
    super.ngOnInit();
    this.init();
  }
}
