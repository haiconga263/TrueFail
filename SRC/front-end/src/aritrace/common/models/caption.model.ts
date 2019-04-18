import { CaptionLanguage } from 'src/aritrace/common/models/caption-language.model';
import { FuncHelper } from 'src/core/helpers/function-helper';

export class Caption {
  id: number;
  name: string;
  type: number;
  defaultCaption: string;
  isCommon: boolean;
  isUsed: boolean;
  createdBy: number;
  createdDate: Date;
  modifiedBy: number;
  modifiedDate: Date;

  public constructor(init?: Partial<Caption>) {
    Object.assign(this, init);
  }
}

export class CaptionMultipleLanguage extends Caption {
  languages: CaptionLanguage[];

  public constructor(init?: Partial<CaptionMultipleLanguage>) {
    super(init);
    Object.assign(this, init);

    if (FuncHelper.isNull(this.languages))
      this.languages = [];
  }
}
