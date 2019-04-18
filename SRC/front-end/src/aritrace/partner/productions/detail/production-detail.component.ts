import { Component, Injector, Input } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { AppUrlConsts, CommonConsts, ObjectTypes, Genders } from 'src/aritrace/common/app-constants';
import { ProductionInformation, Production } from 'src/aritrace/common/models/production.model';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from 'src/aritrace/common/services/common.service';
import { SelectListItem } from 'src/core/models/input.model';
import { ProductionService } from 'src/aritrace/common/services/production.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { ConfigService } from 'src/core/common/config.service';
import { ParamUrlKeys } from 'src/core/constant/AppConsts';
import { PartnerService } from 'src/aritrace/common/services/partner.service';
import { Company, CompanyViewModel } from 'src/aritrace/common/models/company.model';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { ErrorAlert } from 'src/core/alert/alert.service';
import { GTINService } from 'src/aritrace/common/services/gtin.service';
import { GTIN, GTINInformation, GTINTypes } from 'src/aritrace/common/models/gtin.model';
import { Product } from 'src/aritrace/common/models/product.model';
import { GrowingMethodService } from 'src/aritrace/common/services/growing-method.service';
import { ProductService } from 'src/aritrace/common/services/product.service';
import { GrowingMethod } from 'src/aritrace/common/models/growing-method.model';

@Component({
  selector: 'production-detail',
  templateUrl: './production-detail.component.html',
  styleUrls: ['./production-detail.component.css']
})
export class ProductionDetailComponent extends AppBaseComponent {
  id: string;
  returnUrl: string;

  company: CompanyViewModel;
  @Input() production: ProductionInformation;
  @Input() gTIN: GTINInformation;

  GTINTypes: any[];
  @Input() hasIndicatorDigit: boolean = false;
  @Input() btnGenerateOptions: any;

  @Input() classChecking: string;
  @Input() msgChecking: string;
  @Input() btnCheckOptions: any;
  @Input() isHidden: boolean = false;

  products: Product[];
  growingMethods: GrowingMethod[];

  constructor(
    injector: Injector,
    public activatedRoute: ActivatedRoute,
    public commonSvc: CommonService,
    public productionSvc: ProductionService,
    public configSvc: ConfigService,
    public growingMethodSvc: GrowingMethodService,
    public productSvc: ProductService,
    public partnerSvc: PartnerService,
    public gtinSvc: GTINService
  ) {
    super(injector);

    this.production = new ProductionInformation();
    this.gTIN = new GTINInformation();
    this.generateGTIN = this.generateGTIN.bind(this);
    this.btnGenerateOptions = {
      text: 'Generate',
      type: 'default',
      icon: '',
      disabled: false,
    };

    this.classChecking = '';
    this.msgChecking = '';
    this.btnCheckOptions = {
      text: 'Check',
      type: 'success',
      icon: '',
      disabled: false,
    };
  }
  // *** Selection
  selectionChangedHandler(key: string, event: any) {
    console.log(key);
    console.log(event);
    if (!FuncHelper.isNull(event)
      && !FuncHelper.isNull(event['selectedRowKeys']))
      this.production[key] = event['selectedRowKeys'][0];
    else this.production[key] = null;
  }
  // ***

  validation(): boolean {
    this.calculateGTIN(null);
    if (this.classChecking != 'success') {
      this.alertSvc.alert(new ErrorAlert({
        text: this.msgChecking
      }));
      return false;
    }

    return true;
  }

  async calculateGTIN(event: any) {
    if (this.gTIN.type == GTINTypes.gtin_14)
      this.hasIndicatorDigit = true;
    else this.hasIndicatorDigit = false;

    let rs = await this.gtinSvc.calculateCheckDigitByGTIN(this.gTIN);
    if (rs.result == ResultCode.Success) {
      this.classChecking = 'success';
      this.gTIN = rs.data;
    }
    else if (rs.result == ResultCode.Warning) {
      this.classChecking = 'warning';
      this.msgChecking = rs.errorMessage;
    }
    else {
      this.classChecking = 'error';
      this.msgChecking = rs.errorMessage;
    }
  }

  async generateGTIN(event: any) {
    this.btnGenerateOptions = {
      text: 'Generating',
      type: 'default',
      icon: 'fa fa-spinner fa-spin fa-2x fa-fw',
      disabled: true,
    }

    let rs = await this.gtinSvc.generateGTIN(this.gTIN.type);
    if (rs.result == ResultCode.Success) {
      this.gTIN = rs.data;
    }

    this.btnGenerateOptions = {
      text: 'Generate',
      type: 'default',
      icon: '',
      disabled: false,
    };
  }

  async checkGTIN(event: any) {
    this.btnCheckOptions = {
      text: 'Checking',
      type: 'success',
      icon: 'fa fa-spinner fa-spin fa-2x fa-fw',
      disabled: true,
    };

    let rs = await this.gtinSvc.checkNewGTIN(this.gTIN);
    if (rs.result == ResultCode.Success) {
      this.classChecking = 'success';
      this.msgChecking = 'You can use this code!';
    }
    else if (rs.result == ResultCode.Warning) {
      this.classChecking = 'warning';
      this.msgChecking = rs.errorMessage;
    }
    else {
      this.classChecking = 'error';
      this.msgChecking = rs.errorMessage;
    }


    this.btnCheckOptions = {
      text: 'Check',
      type: 'success',
      icon: '',
      disabled: false,
    };
  }

  async loadNewProduction() {
    this.production = new ProductionInformation({
      isUsed: true,
    });

    this.gTIN = new GTINInformation({
      companyCode: this.company.gS1Code,
      type: GTINTypes.gtin_8
    });

    this.calculateGTIN(null);
    this.getDataInit();
  }

  async loadProductionById() {
    let rs = await this.productionSvc.getById(this.id);
    if (rs.result < ResultCode.Success) {
      this.alertSvc.alertByHttpResult(rs);
      this.cancel();
      return;
    }

    this.production = rs.data;

    if (FuncHelper.isNull(this.production.gtin)) {
      this.gTIN = new GTINInformation({
        companyCode: this.company.gS1Code,
        type: GTINTypes.gtin_8
      });
      this.production.gtin = this.gTIN
    }
    else {
      this.isHidden = true;
      this.gTIN = this.production.gtin;
    }

    this.calculateGTIN(null);
    this.getDataInit();
  }

  public async getDataInit() {
    this.productSvc.getList().then((rs) => {
      if (rs.result < ResultCode.Success)
        this.alertSvc.alertByHttpResult(rs);
      else this.products = rs.data;
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
    this.production.gtin = this.gTIN;
    if (this.production.id == 0 || this.production.id == null) {
      let rs = await this.productionSvc.insert(this.production);
      this.alertSvc.alertByHttpResult(rs);
      if (rs.result == ResultCode.Success) {
        this.router.navigate([AppUrlConsts.urlProductionDetail], { queryParams: { id: rs.data['id'], returnurl: encodeURIComponent(this.returnUrl) } });
      }
    }
    else {
      let rs = await this.productionSvc.update(this.production);
      this.alertSvc.alertByHttpResult(rs);
      if (rs.result == ResultCode.Success)
        this.router.navigate([this.returnUrl]);
    }
  }

  cancel() {
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
    this.GTINTypes = this.gtinSvc.getGTINTypes();
    callback();
  }

  ngOnInit() {
    super.ngOnInit();

    this.initialize(() => {
      this.activatedRoute.queryParams.subscribe(params => {
        this.id = params[ParamUrlKeys.id];
        this.returnUrl = decodeURIComponent(params[ParamUrlKeys.returnurl]);

        if (FuncHelper.isNull(this.returnUrl))
          this.returnUrl = AppUrlConsts.urlProduction;

        if (FuncHelper.isNull(this.id)) {
          this.loadNewProduction()
        }
        else {
          this.loadProductionById();
        }
      });
    });
  }
}
