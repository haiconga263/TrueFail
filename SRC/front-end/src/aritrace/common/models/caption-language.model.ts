import { Language } from "src/core/common/language.service";
import { FuncHelper } from "src/core/helpers/function-helper";

export class CaptionLanguage {
  id: number;
  captionId: number;
  languageId: number;
  caption: string;

  langName: string;
  langClassIcon: string;

  public constructor(init?: Partial<CaptionLanguage>) {
    Object.assign(this, init);
  }
}
