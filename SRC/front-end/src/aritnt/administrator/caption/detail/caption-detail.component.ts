import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { CaptionService, Caption, CaptionLanguage } from '../caption.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';
import { appUrl } from '../../app-url';
import { ActivatedRoute, Params } from '@angular/router';
import { ResultModel } from 'src/core/models/http.model';
import { Language } from 'src/core/common/language.service';

@Component({
  selector: 'caption-detail',
  templateUrl: './caption-detail.component.html',
  styleUrls: ['./caption-detail.component.css']
})
export class CaptionDetailComponent extends AppBaseComponent {
  private params: Params;
  private id: number = 0;

  private caption: Caption = new Caption();
  private languages: Language[] = [];
  private types: any[] = [];

  constructor(
    injector: Injector,
    private captionSvc: CaptionService,
    private activatedRoute: ActivatedRoute
  ) {
    super(injector);
      this.activatedRoute.queryParams.subscribe((params: Params) => {
        this.params = params;
        if(this.params["id"] === null)
        {
          this.router.navigate([appUrl.captionList]);
        }
        this.id = this.params["id"];
      });
      this.types = this.captionSvc.getTypes();
      this.Init();
  }

  async Init()
  {
    var langRs = await this.languageSvc.getLanguages().toPromise();
    if(langRs.result == ResultCode.Success)
    {
      this.languages = langRs.data;
    }

    await this.loadDatasource();
  }

  private async loadDatasource() {

    var rs = await this.captionSvc.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      if(rs.data === null)
      {
        this.router.navigate([appUrl.captionList]);
      }

      this.caption = rs.data;

      if(this.caption.languages === null)
      {
        this.caption.languages = [];
      }

      this.languages.forEach(lang => {
        let language = this.caption.languages.find(l => l.languageId == lang.id);
        if(language == null)
        {
          this.caption.languages.push({
            id: 0,
            captionId: this.caption.id,
            languageId: lang.id,
            caption: ''
          });
        }
      });
    }
    else
    {
      this.router.navigate([appUrl.captionList]);
    }
  }

  private return() {
    console.log('return');
    this.router.navigate([appUrl.captionList]);
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    console.log('save');
    console.log(this.caption);

    this.captionSvc.update(this.caption).subscribe((result: ResultModel<any>) => {
      if(result.result == ResultCode.Success)
      {
        //alert
        this.showSuccess(this.lang.instant('Common.UpdateSuccess'));
        this.return();
      }
      else
      {
        //alert
        this.showError(this.lang.instant('Common.UpdateFail'));
      }
    });
  }
}
