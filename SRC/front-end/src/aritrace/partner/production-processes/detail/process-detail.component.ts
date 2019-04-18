import { Component, Injector, Input } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { AppUrlConsts, CommonConsts, ObjectTypes, Genders } from 'src/aritrace/common/app-constants';
import { ProcessInformation, Process } from 'src/aritrace/common/models/production-process.model';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from 'src/aritrace/common/services/common.service';
import { SelectListItem } from 'src/core/models/input.model';
import { ProcessService } from 'src/aritrace/common/services/process.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { ConfigService } from 'src/core/common/config.service';
import { ParamUrlKeys } from 'src/core/constant/AppConsts';
import { PartnerService } from 'src/aritrace/common/services/partner.service';
import { Company, CompanyViewModel } from 'src/aritrace/common/models/company.model';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { ErrorAlert } from 'src/core/alert/alert.service';
import { GTINService } from 'src/aritrace/common/services/gtin.service';
import { GTIN, GTINInformation, GTINTypes } from 'src/aritrace/common/models/gtin.model';
import { ProductionInformation } from 'src/aritrace/common/models/production.model';
import { ProductionService } from 'src/aritrace/common/services/production.service';
import { Product } from 'src/aritrace/common/models/product.model';
import { ProductService } from 'src/aritrace/common/services/product.service';
import { CompanyService } from 'src/aritrace/common/services/company.service';
import { GrowingMethod } from 'src/aritrace/common/models/growing-method.model';
import { GrowingMethodService } from 'src/aritrace/common/services/growing-method.service';


@Component({
  selector: 'process-detail',
  templateUrl: './process-detail.component.html',
  styleUrls: ['./process-detail.component.css']
})
export class ProcessDetailComponent extends AppBaseComponent {
  id: string;
  returnUrl: string;

  company: CompanyViewModel;
  @Input() process: ProcessInformation;

  constructor(
    injector: Injector,
    public activatedRoute: ActivatedRoute,
    public commonSvc: CommonService,
    public processSvc: ProcessService,
    public configSvc: ConfigService,
    public partnerSvc: PartnerService,
    public gtinSvc: GTINService,
    public productionSvc: ProductionService,
    public productSvc: ProductService,
    public companySvc: CompanyService,
    public growingMethodSvc: GrowingMethodService
  ) {
    super(injector);

    this.process = new ProcessInformation({ productionId: null, productId: null });
    this.production_displayExpr = this.production_displayExpr.bind(this);
    this.product_displayExpr = this.product_displayExpr.bind(this);
    this.selectionChangedHandler = this.selectionChangedHandler.bind(this);
  }

  validation(): boolean {

    return true;
  }


  async loadNewProcess() {
    let rs = await this.processSvc.getNew();
    if (rs.result < ResultCode.Success) {
      this.alertSvc.alertByHttpResult(rs);
      this.cancel();
      return;
    }
    else {
      if (rs.result >= ResultCode.Success) {
        this.router.navigate([AppUrlConsts.urlProcessDetail], { queryParams: { id: rs.data['id'], returnurl: encodeURIComponent(this.returnUrl) } });
      }
    }
  }

  async loadProcessById() {
    let rs = await this.processSvc.getById(this.id);
    if (rs.result < ResultCode.Success) {
      this.alertSvc.alertByHttpResult(rs);
      this.cancel();
      return;
    }

    this.process = rs.data;
    this.getDataInit();
  }

  public async getDataInit() {
    this.productionSvc.getList().then((rs) => {
      if (rs.result < ResultCode.Success)
        this.alertSvc.alertByHttpResult(rs);
      else this.productions = rs.data;
    });

    this.productSvc.getList().then((rs) => {
      if (rs.result < ResultCode.Success)
        this.alertSvc.alertByHttpResult(rs);
      else this.products = rs.data;
    });

    this.companySvc.getList().then((rs) => {
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

    this.growingMethodSvc.getList().then((rs) => {
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
  }

  async save() {

    if (this.validation() == false)
      return;

    let rs = await this.processSvc.update(this.process);
    this.alertSvc.alertByHttpResult(rs);
    if (rs.result == ResultCode.Success)
      this.router.navigate([this.returnUrl]);

  }

  cancel() {
    if (this.process.isNew)
      this.processSvc.delete(this.id);
    this.router.navigate([this.returnUrl]);
  }

  async initialize(callback: () => void) {
    let rs = await this.partnerSvc.getByUserId(this.authenticSvc.getSession().id);
    if (rs.result) { this.alertSvc.alertByHttpResult(rs); }
    if (FuncHelper.isNull(rs.data))
      this.alertSvc.alert(new ErrorAlert({
        text: 'Your account does not have enough information to continue, please contact the administrator!'
      })).then(() => {
        this.cancel();
      });

    this.company = rs.data;
    callback();
  }

  // *** Production
  productions: ProductionInformation[];
  production_displayExpr(item: ProductionInformation) {
    if (!FuncHelper.isNull(item))
      return (item.name + ' ' + (FuncHelper.isNull(item.gtin) ? '' : item.gtin.code));
    else return 'undefined';
  }
  // ***


  // *** Product
  products: Product[];
  product_displayExpr(item: Product) {
    if (!FuncHelper.isNull(item))
      return item.defaultName;
    else return 'undefined';
  }
  // ***

  // *** Company
  companies: CompanyViewModel[];
  // ***

  // *** Growing Method
  growingMethods: GrowingMethod[];
  // ***

  // *** Selection
  selectionChangedHandler(key: string, event: any) {
    console.log(key);
    console.log(event);
    if (!FuncHelper.isNull(event)
      && !FuncHelper.isNull(event['selectedRowKeys']))
      this.process[key] = event['selectedRowKeys'][0];
    else this.process[key] = null;
  }
  // ***

  ngOnInit() {
    super.ngOnInit();

    this.initialize(() => {
      this.activatedRoute.queryParams.subscribe(params => {
        this.id = params[ParamUrlKeys.id];
        this.returnUrl = decodeURIComponent(params[ParamUrlKeys.returnurl]);

        if (FuncHelper.isNull(this.returnUrl))
          this.returnUrl = AppUrlConsts.urlProcess;

        if (FuncHelper.isNull(this.id)) {
          this.loadNewProcess()
        }
        else {
          this.loadProcessById();
        }
      });
    });
  }
}
