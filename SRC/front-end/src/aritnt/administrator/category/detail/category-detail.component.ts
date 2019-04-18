import { Component, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { CategoryService, Category } from '../category.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from '../../app-url';
import { ActivatedRoute, Params } from '@angular/router';
import { ResultModel } from 'src/core/models/http.model';
import { Language } from 'src/core/common/language.service';
import { ParamUrlKeys } from 'src/core/constant/AppConsts';

@Component({
  selector: 'category-detail',
  templateUrl: './category-detail.component.html',
  styleUrls: ['./category-detail.component.css']
})
export class CategoryDetailComponent extends AppBaseComponent {
  private id: number = 0;
  private category: Category = new Category();
  private allCategories: Category[] = [];
  private categories: Category[] = [];
  private languages: Language[] = [];

  private isNameValid: boolean = false;

  //Language
  private modifyLanguages: any[] = [];

  constructor(
    injector: Injector,
    private categoryService: CategoryService,
    private activatedRoute: ActivatedRoute
  ) {
    super(injector);
    this.activatedRoute.queryParams.subscribe((params: Params) => {
      this.id = params["id"];
    });
    this.Init();
  }

  async Init() {
    var langRs = await this.languageSvc.getLanguages().toPromise();
    if (langRs.result == ResultCode.Success) {
      this.languages = langRs.data;
    }

    var categoriesRs = await this.categoryService.gets().toPromise();
    if (categoriesRs.result == ResultCode.Success) {
      this.allCategories = categoriesRs.data;
    }

    if (this.id != null && this.id > 0) {
      await this.loadDatasource();
    }
    else {
      this.categories = this.allCategories;
      this.categories.unshift(new Category({ id: null, name: '- none -' }));
      this.modifyLanguages = [];
      this.languages.forEach((language) => {
        this.modifyLanguages.push({
          categoryLanguageId: 0,
          languageId: language.id,
          langShowName: language.code + " - " + language.name,
          name: '',
        });
      });
    }
  }

  private async loadDatasource() {
    this.categories = this.categoryService.removeChildren(this.id, this.allCategories);
    this.categories.unshift(new Category({ id: null, name: '- none -' }));

    var rs = await this.categoryService.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.category = rs.data;

      this.modifyLanguages = [];
      this.languages.forEach((language) => {

        var categoryLanguage = this.category.languages.find(l => l.languageId == language.id);
        if (categoryLanguage == null) {
          this.modifyLanguages.push({
            categoryLanguageId: 0,
            languageId: language.id,
            langShowName: language.code + " - " + language.name,
            name: '',
          });
        }
        else {
          this.modifyLanguages.push({
            categoryLanguageId: categoryLanguage.id,
            languageId: language.id,
            langShowName: language.code + " - " + language.name,
            name: categoryLanguage.name,
          });
        }
      });
    }
  }

  private return() {
    this.router.navigate([appUrl.categoryList]);
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    if (!this.checkValid()) {
      return;
    }

    this.category.languages = [];
    this.modifyLanguages.forEach((item) => {
      this.category.languages.push({
        id: item.categoryLanguageId,
        categoryId: 0,
        languageId: item.languageId,
        name: item.name,
      });
    });

    if (this.id != null && this.id > 0) {
      this.categoryService.update(this.category).subscribe((result: ResultModel<any>) => {
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
      this.categoryService.add(this.category).subscribe((result: ResultModel<any>) => {
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
    return this.isNameValid;
  }

  ngOnInit() {
  }
}
