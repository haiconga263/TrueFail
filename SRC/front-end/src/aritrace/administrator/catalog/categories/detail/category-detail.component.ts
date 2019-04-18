import { Component, Injector, ViewChild, ViewChildren, QueryList, AfterViewInit } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { AppUrlConsts, CommonConsts, ObjectTypes, Genders } from 'src/aritrace/common/app-constants';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from 'src/aritrace/common/services/common.service';
import { CategoryService } from 'src/aritrace/common/services/category.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { ConfigService } from 'src/core/common/config.service';
import { Language } from 'src/core/common/language.service';
import { Category } from 'src/aritrace/common/models/category.model';
import { ParamUrlKeys } from 'src/core/constant/AppConsts';
import { CaptionEmbedComponent } from 'src/aritrace/administrator/configuration/language-settings/captions/embed/caption-embed.component';
import { FuncHelper } from 'src/core/helpers/function-helper';

@Component({
  selector: 'category-detail',
  templateUrl: './category-detail.component.html',
  styleUrls: ['./category-detail.component.css']
})
export class CategoryDetailComponent extends AppBaseComponent implements AfterViewInit {
  @ViewChildren(CaptionEmbedComponent) viewChildren !: QueryList<CaptionEmbedComponent>;
  category: Category;
  id: string;
  returnUrl: string;
  public caption: CaptionEmbedComponent;

  idLoadDataLocation: boolean = false;
  allLanguages: Language[];

  constructor(
    public injector: Injector,
    public activatedRoute: ActivatedRoute,
    public commonSvc: CommonService,
    public categorySvc: CategoryService,
    public configSvc: ConfigService
  ) {
    super(injector);

    this.category = new Category();
    this.saveCategory = this.saveCategory.bind(this);
  }

  ngAfterViewInit() {
    this.caption = this.viewChildren.first;
    this.activatedRoute.queryParams.subscribe(params => {
      this.id = params[ParamUrlKeys.id];
      this.returnUrl = decodeURIComponent(params[ParamUrlKeys.returnurl]);

      if (FuncHelper.isNull(this.returnUrl))
        this.returnUrl = AppUrlConsts.urlCategory;

      if (FuncHelper.isNull(this.id)) {
        this.loadNewCategory()
      }
      else {
        this.loadCategoryById();
      }
    })
  }

  async loadNewCategory() {
    this.category = new Category({
      isUsed: true
    });

    this.caption.initialize();
    this.getDataInit();
  }

  async loadCategoryById() {
    let rs = await this.categorySvc.getById(this.id);
    if (rs.result < ResultCode.Success) {
      this.alertSvc.alertByHttpResult(rs);
      this.cancel();
      return;
    }

    this.category = rs.data;
    this.caption.initialize(FuncHelper.isNull(this.category.captionNameId) ? 0 : this.category.captionNameId);
    this.getDataInit();
  }

  public async getDataInit() {
    this.allLanguages = (await this.commonSvc.getLanguages()).data;
  }

  save() {
    this.caption.save(this.saveCategory);
  }

  async saveCategory(captionId: number) {
    if (captionId == 0) return;

    this.category.captionNameId = captionId;
    if (this.category.id == 0 || this.category.id == null) {
      let rs = await this.categorySvc.insert(this.category);
      this.alertSvc.alertByHttpResult(rs);
      if (rs.result == ResultCode.Success) {
        this.router.navigate([AppUrlConsts.urlCategoryDetail], { queryParams: { id: rs.data['id'], returnurl: encodeURIComponent(this.returnUrl) } });
      }
    }
    else {
      let rs = await this.categorySvc.update(this.category);
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

  }
}
