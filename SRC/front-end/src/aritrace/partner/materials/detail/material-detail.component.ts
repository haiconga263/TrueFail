import { Component, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { AppUrlConsts, CommonConsts, ObjectTypes, Genders } from 'src/aritrace/common/app-constants';
import { Material } from 'src/aritrace/common/models/material.model';
import { ActivatedRoute } from '@angular/router';
import { AppConsts, UrlConsts, ParamUrlKeys } from 'src/core/constant/AppConsts';
import { CommonService } from 'src/aritrace/common/services/common.service';
import { MaterialService } from 'src/aritrace/common/services/material.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { ConfigService } from 'src/core/common/config.service';
import { Language } from 'src/core/common/language.service';
import { Category } from 'src/aritrace/common/models/category.model';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { Product } from 'src/aritrace/common/models/product.model';

@Component({
  selector: 'material-detail',
  templateUrl: './material-detail.component.html',
  styleUrls: ['./material-detail.component.css']
})
export class MaterialDetailComponent extends AppBaseComponent {
  material: Material;
  id: string;
  returnUrl: string;

  products: Product[];
  languages: Language[];

  constructor(
    injector: Injector,
    public activatedRoute: ActivatedRoute,
    public commonSvc: CommonService,
    public materialSvc: MaterialService,
    public configSvc: ConfigService
  ) {
    super(injector);

    this.material = new Material();
    this.cancel = this.cancel.bind(this);
    this.save = this.save.bind(this);
  }

  async loadNewMaterial() {
    this.material = new Material({
      isUsed: true
    });

    this.materialSvc.generatecode().then((rs) => { this.material.code = rs.data });

    this.getDataInit();
  }

  async loadMaterialById() {
    let rs = await this.materialSvc.getById(this.id);
    if (rs.result < ResultCode.Success) {
      this.alertSvc.alertByHttpResult(rs);
      this.cancel();
      return;
    }

    this.material = rs.data;

    this.getDataInit();
  }

  public async getDataInit() {
    this.products = (await this.commonSvc.getProducts()).data;
    this.languages = (await this.commonSvc.getLanguages()).data;
  }

  async save() {
    if (FuncHelper.isNull(this.material.id)) {
      let rs = await this.materialSvc.insert(this.material);
      this.alertSvc.alertByHttpResult(rs);
      if (rs.result == ResultCode.Success) {
        this.router.navigate([AppUrlConsts.urlMaterialDetail], { queryParams: { id: rs.data['id'], returnurl: encodeURIComponent(this.returnUrl) } });
      }
    }
    else {
      let rs = await this.materialSvc.update(this.material);
      this.alertSvc.alertByHttpResult(rs);
      if (rs.result == ResultCode.Success)
        this.router.navigate([this.returnUrl]);
    }
  }

  cancel() {
    this.router.navigate([this.returnUrl]);
  }

  ngOnInit() {
    super.ngOnInit();

    this.activatedRoute.queryParams.subscribe(params => {
      this.id = params[ParamUrlKeys.id];
      this.returnUrl = decodeURIComponent(params[ParamUrlKeys.returnurl]);

      if (FuncHelper.isNull(this.returnUrl))
        this.returnUrl = AppUrlConsts.urlMaterials;

      if (FuncHelper.isNull(this.id)) {
        this.loadNewMaterial()
      }
      else {
        this.loadMaterialById();
      }
    })
  }
}
