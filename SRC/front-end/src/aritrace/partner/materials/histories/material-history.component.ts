import { Component, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { AppUrlConsts, CommonConsts, ObjectTypes, Genders } from 'src/aritrace/common/app-constants';
import { MaterialHistory, MaterialHistoryInformation } from 'src/aritrace/common/models/material.model';
import { ActivatedRoute } from '@angular/router';
import { AppConsts, UrlConsts, ParamUrlKeys } from 'src/core/constant/AppConsts';
import { CommonService } from 'src/aritrace/common/services/common.service';
import { MaterialService } from 'src/aritrace/common/services/material.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { ConfigService } from 'src/core/common/config.service';
import { Language } from 'src/core/common/language.service';
import { Category } from 'src/aritrace/common/models/category.model';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { Product } from 'src/aritrace/common/models/product.model';
import { CultureField } from 'src/aritrace/common/models/culture-field.model';
import { CollectionNestedOption } from 'devextreme-angular';
import { DatePipe } from '@angular/common'

@Component({
  selector: 'material-history',
  templateUrl: './material-history.component.html',
  styleUrls: ['./material-history.component.css']
})
export class MaterialHistoryComponent extends AppBaseComponent {
  histories: MaterialHistoryInformation[];
  id: string;

  cultureFields: CultureField[];
  //selectedCulFieldId: number;
  selectedCulField: CultureField;
  sourceList: string[];

  historyNew: MaterialHistory;
  isPopupNew: boolean = false;

  constructor(
    injector: Injector,
    public activatedRoute: ActivatedRoute,
    public commonSvc: CommonService,
    public materialSvc: MaterialService,
    public configSvc: ConfigService,
    //public datepipe: DatePipe
  ) {
    super(injector);

    this.histories = [];
  }

  //formatDateTime(dt: Date, format: string): any {
  //  return this.datepipe.transform(dt, format);
  //}

  popupAdd() {
    this.historyNew = new MaterialHistory({ materialId: +this.id });
    this.selectedCulField = new CultureField();
    this.isPopupNew = true;
  }

  async addHistory() {
    this.historyNew.cultureFieldId = this.selectedCulField.id;
    let rs = await this.materialSvc.insertHistory(this.historyNew);
    this.alertSvc.alertByHttpResult(rs);
    this.loadMaterialHistoryById();
    //this.isPopupNew = false;
  }

  selectedCulFieldChanged(e) {
    this.selectedCulField = this.cultureFields.find(x => x.id == e.value);
    this.historyNew.value = null;
    if (!FuncHelper.isNull(this.selectedCulField) && this.selectedCulField.dataType == 'list')
      this.sourceList = this.getSourceList(this.selectedCulField.source);
    else this.sourceList = [];
  }

  getSourceList(source: string): string[] {
    if (FuncHelper.isNull(source)) source = '';
    source = source.replace(/(\r\n|\n|\r)/gm, "")
    return source.split(";").sort();
  }

  async loadMaterialHistoryById() {
    let rs = await this.materialSvc.getHistoriesById(this.id);
    if (rs.result < ResultCode.Success) {
      this.alertSvc.alertByHttpResult(rs);
      return;
    }

    this.histories = rs.data.sort((x, y) => { return FuncHelper.getTime(y.createdDate) - FuncHelper.getTime(x.createdDate); });

    this.getDataInit();
  }

  public async getDataInit() {
    this.cultureFields = (await this.commonSvc.getCultureFields()).data;
  }

  ngOnInit() {
    super.ngOnInit();

    this.activatedRoute.queryParams.subscribe(params => {
      this.id = params[ParamUrlKeys.id];

      if (!FuncHelper.isNull(this.id)) {
        this.loadMaterialHistoryById();
      }
    })
  }
}
