import { Component, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { AppUrlConsts, CommonConsts, ObjectTypes, Genders } from 'src/aritrace/common/app-constants';
import { ProductMultipleLanguage, Product, ProductLanguage } from 'src/aritrace/common/models/product.model';
import { ActivatedRoute } from '@angular/router';
import { AppConsts, UrlConsts, ParamUrlKeys } from 'src/core/constant/AppConsts';
import { CommonService } from 'src/aritrace/common/services/common.service';
import { ProductService } from 'src/aritrace/common/services/product.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { ConfigService } from 'src/core/common/config.service';
import { Language } from 'src/core/common/language.service';
import { Category } from 'src/aritrace/common/models/category.model';
import { FuncHelper } from 'src/core/helpers/function-helper';

@Component({
  selector: 'product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent extends AppBaseComponent {
  product: ProductMultipleLanguage;
  id: string;
  returnUrl: string;

  idLoadDataLocation: boolean = false;
  allCategories: Category[];
  languages: Language[];

  constructor(
    injector: Injector,
    public activatedRoute: ActivatedRoute,
    public commonSvc: CommonService,
    public productSvc: ProductService,
    public configSvc: ConfigService
  ) {
    super(injector);

    this.imageChanged = this.imageChanged.bind(this);
    this.product = new ProductMultipleLanguage();
  }

  async loadNewProduct() {
    this.product = new ProductMultipleLanguage({
      imagePath: '',
      isChangedImage: false,
      productLanguages: [],
      isUsed: true
    });


    this.configSvc.pushEvent(() => {
      this.product.imageData = AppConsts.imageDefaultUrl;
    });
    this.product.isChangedImage = false;
    this.getDataInit();
  }

  async loadProductById() {
    let rs = await this.productSvc.getById(this.id);
    if (rs.result < ResultCode.Success) {
      this.alertSvc.alertByHttpResult(rs);
      this.cancel();
      return;
    }

    this.product = rs.data;
    this.configSvc.pushEvent(() => {
      if (this.product.imagePath == '' || this.product.imagePath == null)
        this.product.imageData = AppConsts.imageDefaultUrl;
      else this.product.imageData = AppConsts.imageDataUrl + '/products/' + this.product.imagePath + '?' + Date.now();
    });

    this.product.isChangedImage = false;
    this.getDataInit();
  }

  public async getDataInit() {
    this.allCategories = (await this.commonSvc.getCategories()).data;
    this.languages = (await this.commonSvc.getLanguages()).data;

    if (FuncHelper.isNull(this.product.productLanguages))
      this.product.productLanguages = [];
    for (var i = 0; i < this.languages.length; i++) {
      let isExist = false;
      for (var j = 0; j < this.product.productLanguages.length; j++) {
        if (this.product.productLanguages[j].languageId == this.languages[i].id) {
          isExist = true;
          this.product.productLanguages[j].langName = this.languages[i].name;
          this.product.productLanguages[j].langClassIcon = this.languages[i].classIcon;
          break;
        }
      }

      if (!isExist)
        this.product.productLanguages.push(new ProductLanguage({
          name: '',
          description: '',
          productId: this.product.id,
          languageId: this.languages[i].id,
          langName: this.languages[i].name,
          langClassIcon: this.languages[i].classIcon,
        }));
    }
  }

  imageChanged(fileInput: any) {
    if (fileInput.target.files && fileInput.target.files[0]) {
      var reader = new FileReader();
      let _component = this;
      reader.onload = function (e: any) {
        _component.product.imageData = e.target.result;
        _component.product.isChangedImage = true;
      }
      reader.readAsDataURL(fileInput.target.files[0]);
    }
  }

  async save() {
    if (this.product.id == 0 || this.product.id == null) {
      let rs = await this.productSvc.insert(this.product);
      this.alertSvc.alertByHttpResult(rs);
      if (rs.result == ResultCode.Success) {
        this.router.navigate([AppUrlConsts.urlProductDetail], { queryParams: { id: rs.data['id'], returnurl: encodeURIComponent(this.returnUrl) } });
      }
    }
    else {
      let rs = await this.productSvc.update(this.product);
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
        this.returnUrl = AppUrlConsts.urlProduct;

      if (FuncHelper.isNull(this.id)) {
        this.loadNewProduct()
      }
      else {
        this.loadProductById();
      }
    })
  }
}
