import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from '../../app-url';
import { ResultModel } from 'src/core/models/http.model';
import { Language } from 'src/core/common/language.service';
import { ParamUrlKeys } from 'src/core/constant/AppConsts';
import { Pesticide, PesticideService } from 'src/aritnt/common/services/pesticide.service';
import { Injector, Component } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { PesticideCategoryService, PesticideCategory } from 'src/aritnt/common/services/pesticide-category.service';

@Component({
  selector: 'pesticide-detail',
  templateUrl: './pesticide-detail.component.html',
  styleUrls: ['./pesticide-detail.component.css']
})
export class PesticideDetailComponent extends AppBaseComponent {
  private id: number = 0;
  private pesticide: Pesticide = new Pesticide();
  private categories: PesticideCategory[] = [];

  private isNameValid: boolean = false;

  constructor(
    injector: Injector,
    private pesticideService: PesticideService,
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
    var categoriesRs = await this.pesticideCategoryService.gets().toPromise();
    if (categoriesRs.result == ResultCode.Success) {
      this.categories = categoriesRs.data;
      this.categories.unshift(new PesticideCategory({ id: null, name: '- none -' }));
    }

    if (this.id != null && this.id > 0) {
      await this.loadDatasource();
    }
  }

  private async loadDatasource() {
    var rs = await this.pesticideService.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.pesticide = rs.data;
    }
  }

  private return() {
    this.router.navigate([appUrl.pesticideList]);
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    if (!this.checkValid()) {
      return;
    }

    if (this.id != null && this.id > 0) {
      this.pesticideService.update(this.pesticide).subscribe((result: ResultModel<any>) => {
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
      this.pesticideService.add(this.pesticide).subscribe((result: ResultModel<any>) => {
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
