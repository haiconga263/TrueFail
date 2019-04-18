import { Component, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { ResultCode } from 'src/core/constant/AppEnums';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { Caption, CaptionService } from '../caption.service';
import { appUrl } from '../../app-url';
import { Language } from 'src/core/common/language.service';

@Component({
  selector: 'captionlanguage',
  templateUrl: './caption.component.html',
  styleUrls: ['./caption.component.css']
})
export class CaptionComponent extends AppBaseComponent {
  private captions: Caption[] = [];
  private types: any[] = [];
  selectedRows: number[];

  private languages: Language[] = [];

  constructor(
    injector: Injector,
    private captionSvc: CaptionService
  ) {
    super(injector);

    this.types = this.captionSvc.getTypes();
    this.languageSvc.getLanguages().subscribe(langRs => {
      if(langRs.result == ResultCode.Success)
      {
        this.languages = langRs.data;
      }
    });
    this.loadDatasource();
  }

  loadDatasource(callback: () => void = null) {
    this.captionSvc.gets().subscribe((result) => {
      if (result.result == ResultCode.Success) {

        this.captions = result.data;

        if (FuncHelper.isFunction(callback))
          callback();
      }
    });
  }

  private refresh() {
    this.loadDatasource();
  }

  private update() {
    this.router.navigate([appUrl.captionDetail],
      {
        queryParams: {
          id: this.selectedRows[0]
        }
      });
  }
}
