import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from '../../app-url';
import { ResultModel } from 'src/core/models/http.model';
import { Language } from 'src/core/common/language.service';
import { ParamUrlKeys } from 'src/core/constant/AppConsts';
import { PesticideCategory, PesticideCategoryService } from 'src/aritnt/common/services/pesticide-category.service';
import { Injector, Component } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'pesticideCategory-detail',
  templateUrl: './pesticide-category-detail.component.html',
  styleUrls: ['./pesticide-category-detail.component.css']
})
export class PesticideCategoryDetailComponent extends AppBaseComponent {
  private id: number = 0;
  private pesticideCategory: PesticideCategory = new PesticideCategory();
  private allPesticideCategories: PesticideCategory[] = [];
  private pesticideCategories: PesticideCategory[] = [];

  private isNameValid: boolean = false;

  constructor(
    injector: Injector,
    private pesticideCategoryService: PesticideCategoryService,
    private activatedRoute: ActivatedRoute
  ) {
    super(injector);
    this.activatedRoute.queryParams.subscribe((params: Params) => {
      this.id = params["id"];
    });
    this.Init();
  }

  async Init() {
    var pesticideCategoriesRs = await this.pesticideCategoryService.gets().toPromise();
    if (pesticideCategoriesRs.result == ResultCode.Success) {
      this.allPesticideCategories = pesticideCategoriesRs.data;
    }

    if (this.id != null && this.id > 0) {
      await this.loadDatasource();
    }
    else {
      this.pesticideCategories = this.allPesticideCategories;
      this.pesticideCategories.unshift(new PesticideCategory({ id: null, name: '- none -' }));
    }
  }

  private async loadDatasource() {
    this.pesticideCategories = this.pesticideCategoryService.removeChildren(this.id, this.allPesticideCategories);
    this.pesticideCategories.unshift(new PesticideCategory({ id: null, name: '- none -' }));

    var rs = await this.pesticideCategoryService.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.pesticideCategory = rs.data;
    }
  }

  private return() {
    this.router.navigate([appUrl.pesticideCategoryList]);
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    if (!this.checkValid()) {
      return;
    }

    if (this.id != null && this.id > 0) {
      this.pesticideCategoryService.update(this.pesticideCategory).subscribe((result: ResultModel<any>) => {
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
      this.pesticideCategoryService.add(this.pesticideCategory).subscribe((result: ResultModel<any>) => {
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
