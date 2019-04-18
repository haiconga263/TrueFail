import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from '../../app-url';
import { ResultModel } from 'src/core/models/http.model';
import { Language } from 'src/core/common/language.service';
import { ParamUrlKeys } from 'src/core/constant/AppConsts';
import { FertilizerCategory, FertilizerCategoryService } from 'src/aritnt/common/services/fertilizer-category.service';
import { Injector, Component } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'fertilizerCategory-detail',
  templateUrl: './fertilizer-category-detail.component.html',
  styleUrls: ['./fertilizer-category-detail.component.css']
})
export class FertilizerCategoryDetailComponent extends AppBaseComponent {
  private id: number = 0;
  private fertilizerCategory: FertilizerCategory = new FertilizerCategory();
  private allFertilizerCategories: FertilizerCategory[] = [];
  private fertilizerCategories: FertilizerCategory[] = [];

  private isNameValid: boolean = false;

  constructor(
    injector: Injector,
    private fertilizerCategoryService: FertilizerCategoryService,
    private activatedRoute: ActivatedRoute
  ) {
    super(injector);
    this.activatedRoute.queryParams.subscribe((params: Params) => {
      this.id = params["id"];
    });
    this.Init();
  }

  async Init() {
    var fertilizerCategoriesRs = await this.fertilizerCategoryService.gets().toPromise();
    if (fertilizerCategoriesRs.result == ResultCode.Success) {
      this.allFertilizerCategories = fertilizerCategoriesRs.data;
    }

    if (this.id != null && this.id > 0) {
      await this.loadDatasource();
    }
    else {
      this.fertilizerCategories = this.allFertilizerCategories;
      this.fertilizerCategories.unshift(new FertilizerCategory({ id: null, name: '- none -' }));
    }
  }

  private async loadDatasource() {
    this.fertilizerCategories = this.fertilizerCategoryService.removeChildren(this.id, this.allFertilizerCategories);
    this.fertilizerCategories.unshift(new FertilizerCategory({ id: null, name: '- none -' }));

    var rs = await this.fertilizerCategoryService.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.fertilizerCategory = rs.data;
    }
  }

  private return() {
    this.router.navigate([appUrl.fertilizerCategoryList]);
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    if (!this.checkValid()) {
      return;
    }

    if (this.id != null && this.id > 0) {
      this.fertilizerCategoryService.update(this.fertilizerCategory).subscribe((result: ResultModel<any>) => {
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
      this.fertilizerCategoryService.add(this.fertilizerCategory).subscribe((result: ResultModel<any>) => {
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
