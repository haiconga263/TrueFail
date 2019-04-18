import { Component, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { ProductService, Product } from '../product.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from '../../app-url';
import { ActivatedRoute, Params } from '@angular/router';
import { ResultModel } from 'src/core/models/http.model';
import { Language } from 'src/core/common/language.service';
import { UoMService, UoM } from '../../common/services/uom.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { Category, CategoryService } from '../../category/category.service';

@Component({
  selector: 'product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent extends AppBaseComponent {
  private params: Params;
  private type: string = 'add';
  private id: number = 0;

  private product: Product = new Product();
  private languages: Language[] = [];
  private uoms: UoM[] = [];
  private categories: Category[] = [];

  private isNameValid: boolean = false;
  private isUoMValid: boolean = false;
  private isCategoryValid: boolean = false;

  //Language
  private modifyLanguages: any[] = [];

  private numberPattern = AppConsts.numberPattern;

  constructor(
    injector: Injector,
    private prodSvc: ProductService,
    private uoMSvc: UoMService,
    private cateSvc: CategoryService,
    private activatedRoute: ActivatedRoute
  ) {
    super(injector);
    this.activatedRoute.queryParams.subscribe((params: Params) => {
      this.params = params;
      if (this.params['type'] == 'update') {
        this.type = 'update';
        this.id = this.params["id"];
      }
    });
    this.Init();
  }

  async Init() {
    var langRs = await this.languageSvc.getLanguages().toPromise();
    if (langRs.result == ResultCode.Success) {
      this.languages = langRs.data;
    }

    var uoMRs = await this.uoMSvc.gets().toPromise();
    if (uoMRs.result == ResultCode.Success) {
      this.uoms = uoMRs.data;
    }

    var cateRs = await this.cateSvc.gets().toPromise();
    if (cateRs.result == ResultCode.Success) {
      this.categories = cateRs.data;
    }

    if (this.params['type'] == 'update') {
      await this.loadDatasource();
    }
    else {
      this.modifyLanguages = [];
      this.languages.forEach((language) => {
        this.modifyLanguages.push({
          productLanguageId: 0,
          languageId: language.id,
          langShowName: language.code + " - " + language.name,
          name: '',
          description: ''
        });
      });
    }
  }

  private async loadDatasource() {

    var rs = await this.prodSvc.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.product = rs.data;
      this.product.imageData = AppConsts.imageDataUrl + this.product.imageURL;

      this.modifyLanguages = [];
      this.languages.forEach((language) => {

        var productLanguage = this.product.languages.find(l => l.languageId == language.id);
        if (productLanguage == null) {
          this.modifyLanguages.push({
            productLanguageId: 0,
            languageId: language.id,
            langShowName: language.code + " - " + language.name,
            name: '',
            description: ''
          });
        }
        else {
          this.modifyLanguages.push({
            productLanguageId: productLanguage.id,
            languageId: language.id,
            langShowName: language.code + " - " + language.name,
            name: productLanguage.name,
            description: productLanguage.description
          });
        }
      });
    }
  }

  treeView_itemSelectionChanged(e) {
    this.product.categoryId = e.node.itemData.id;
  }

  syncTreeViewSelection(event: any, treeView: any) {
    if (!treeView) return;

    if (!this.product.categoryId) {
      treeView.instance.unselectAll();
    } else {
      treeView.instance.selectItem(this.product.categoryId);
    }
  }

  avatarChangeEvent(fileInput: any, component: ProductDetailComponent) {
    if (fileInput.target.files && fileInput.target.files[0]) {
      var reader = new FileReader();
      reader.onload = function (e: any) {
        component.product.imageData = e.target.result;
      }
      reader.readAsDataURL(fileInput.target.files[0]);
    }
  }

  private return() {
    console.log('return');
    this.router.navigate([appUrl.productList]);
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    console.log('save');
    console.log(this.product);
    if (!this.checkValid()) {
      return;
    }

    this.product.languages = [];
    this.modifyLanguages.forEach((item) => {
      this.product.languages.push({
        id: item.productLanguageId,
        productId: 0,
        languageId: item.languageId,
        name: item.name,
        description: item.description
      });
    });

    if (this.type == "update") {
      this.prodSvc.update(this.product).subscribe((result: ResultModel<any>) => {
        if (result.result == ResultCode.Success) {
          //alert
          this.showSuccess(this.lang.instant('Common.UpdateSuccess'));
          this.return();
        }
        else {
          //alert
          this.showError(result.errorMessage);
        }
      });
    }
    else {
      this.prodSvc.add(this.product).subscribe((result: ResultModel<any>) => {
        if (result.result == ResultCode.Success) {
          //alert
          this.showSuccess(this.lang.instant('Common.AddSuccess'));
          this.return();
        }
        else {
          //alert
          this.showError(result.errorMessage);
        }
      });
    }
  }

  private checkValid(): boolean {
    return this.isNameValid && this.isUoMValid && this.isCategoryValid;
  }

  buyingPriceChanged(event) {
    if(event.value == null) {
      this.product.defaultBuyingPrice = 0;
    }
  }

  sellingPriceChanged(event) {
    if(event.value == null) {
      this.product.defaultSellingPrice = 0;
    }
  }

  ngOnInit() {
  }
}
