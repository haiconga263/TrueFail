import { Component, Injector, ViewChild, ViewChildren, QueryList, AfterViewInit } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { AppUrlConsts, CommonConsts, ObjectTypes, Genders } from 'src/aritrace/common/app-constants';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from 'src/aritrace/common/services/common.service';
import { CultureFieldService } from 'src/aritrace/common/services/culture-field.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { ConfigService } from 'src/core/common/config.service';
import { Language } from 'src/core/common/language.service';
import { CultureField, CULTURE_FIELD_TYPES } from 'src/aritrace/common/models/culture-field.model';
import { ParamUrlKeys } from 'src/core/constant/AppConsts';
import { CaptionEmbedComponent } from 'src/aritrace/administrator/configuration/language-settings/captions/embed/caption-embed.component';
import { FuncHelper } from 'src/core/helpers/function-helper';

@Component({
  selector: 'culture-field-detail',
  templateUrl: './culture-field-detail.component.html',
  styleUrls: ['./culture-field-detail.component.css']
})
export class CultureFieldDetailComponent extends AppBaseComponent implements AfterViewInit {
  cultureField: CultureField;
  id: string;
  returnUrl: string;

  dataTypes: string[];
  isList: boolean;
  isRange: boolean;
  rangeType: string = 'dxTextBox';
  dtType: string = '';

  // idLoadDataLocation: boolean = false;
  // allLanguages: Language[];

  constructor(
    public injector: Injector,
    public activatedRoute: ActivatedRoute,
    public commonSvc: CommonService,
    public cultureFieldSvc: CultureFieldService,
    public configSvc: ConfigService
  ) {
    super(injector);

    this.cultureField = new CultureField();
    this.dataTypes = CULTURE_FIELD_TYPES;
    this.isRange = true;
    this.rangeType = 'dxTextBox';
    this.dataTypeChanged = this.dataTypeChanged.bind(this);
    this.updateByDataType();
  }

  ngAfterViewInit() {
    // this.caption = this.viewChildren.first;

  }

  dataTypeChanged() {
    //this.cultureField.minimum = '';
    //this.cultureField.maximum = '';
    //this.cultureField.source = '';
    this.updateByDataType();
  }

  updateByDataType() {
    this.rangeType = 'dxTextBox';
    this.dtType = '';
    this.isRange = false;
    this.isList = false;
    switch (this.cultureField.dataType) {
      case 'number':
        this.rangeType = 'dxNumberBox';
        this.isRange = true;
        break;
      case 'date':
        this.rangeType = 'dxDateBox';
        this.dtType = 'date';
        this.isRange = true;
        break;
      case 'time':
        this.rangeType = 'dxDateBox';
        this.dtType = 'time';
        this.isRange = true;
        break;
      case 'datetime':
        this.rangeType = 'dxDateBox';
        this.dtType = 'datetime';
        this.isRange = true;
        break;
      case 'list':
        this.isList = true;
        break;

    }
  }

  async loadNewCultureField() {
    this.cultureField = new CultureField({
      isUsed: true
    });

    //this.caption.initialize();
    this.getDataInit();
  }

  async loadCultureFieldById() {
    let rs = await this.cultureFieldSvc.getById(this.id);
    if (rs.result < ResultCode.Success) {
      this.alertSvc.alertByHttpResult(rs);
      this.cancel();
      return;
    }

    this.cultureField = rs.data;
    this.getDataInit();
  }

  public async getDataInit() {
  }

  async save() {
    if (this.cultureField.id == 0 || this.cultureField.id == null) {
      let rs = await this.cultureFieldSvc.insert(this.cultureField);
      this.alertSvc.alertByHttpResult(rs);
      if (rs.result == ResultCode.Success) {
        this.router.navigate([AppUrlConsts.urlCultureFieldDetail], { queryParams: { id: rs.data['id'], returnurl: encodeURIComponent(this.returnUrl) } });
      }
    }
    else {
      let rs = await this.cultureFieldSvc.update(this.cultureField);
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

    this.activatedRoute.queryParams.subscribe(params => {
      this.id = params[ParamUrlKeys.id];
      this.returnUrl = decodeURIComponent(params[ParamUrlKeys.returnurl]);

      if (FuncHelper.isNull(this.returnUrl))
        this.returnUrl = AppUrlConsts.urlCultureFields;

      if (FuncHelper.isNull(this.id)) {
        this.loadNewCultureField()
      }
      else {
        this.loadCultureFieldById();
      }
    })
  }
}
