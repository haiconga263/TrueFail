import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from '../../app-url';
import { ResultModel } from 'src/core/models/http.model';
import { Language } from 'src/core/common/language.service';
import { ParamUrlKeys } from 'src/core/constant/AppConsts';
import { Fertilizer, FertilizerService } from 'src/aritnt/common/services/fertilizer.service';
import { Injector, Component } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { FertilizerCategoryService, FertilizerCategory } from 'src/aritnt/common/services/fertilizer-category.service';

@Component({
  selector: 'fertilizer-detail',
  templateUrl: './fertilizer-detail.component.html',
  styleUrls: ['./fertilizer-detail.component.css']
})
export class FertilizerDetailComponent extends AppBaseComponent {
  private id: number = 0;
  private fertilizer: Fertilizer = new Fertilizer();
  private categories: FertilizerCategory[] = [];

  private isNameValid: boolean = false;

  constructor(
    injector: Injector,
    private fertilizerService: FertilizerService,
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
    var categoriesRs = await this.fertilizerCategoryService.gets().toPromise();
    if (categoriesRs.result == ResultCode.Success) {
      this.categories = categoriesRs.data;
      this.categories.unshift(new FertilizerCategory({ id: null, name: '- none -' }));
    }

    if (this.id != null && this.id > 0) {
      await this.loadDatasource();
    }
  }

  private async loadDatasource() {
    var rs = await this.fertilizerService.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.fertilizer = rs.data;
    }
  }

  private return() {
    this.router.navigate([appUrl.fertilizerList]);
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    if (!this.checkValid()) {
      return;
    }

    if (this.id != null && this.id > 0) {
      this.fertilizerService.update(this.fertilizer).subscribe((result: ResultModel<any>) => {
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
      this.fertilizerService.add(this.fertilizer).subscribe((result: ResultModel<any>) => {
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
