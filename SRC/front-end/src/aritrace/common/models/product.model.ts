import { FuncHelper } from "src/core/helpers/function-helper";

export class Product {
  id: number;
  imagePath: string;
  code: string;
  defaultName: string;
  defaultDecription: string;
  categoryId: number;
  isUsed: boolean;
  createdBy: number;
  createdDate: Date;
  modifiedBy: number;
  modifiedDate: Date;

  public constructor(init?: Partial<Product>) {
    Object.assign(this, init);
  }
}

export class ProductLanguage {
  id: number;
  name: string;
  description: string;
  productId: number;
  languageId: number;
  langName: string;
  langClassIcon: string;

  public constructor(init?: Partial<ProductLanguage>) {
    Object.assign(this, init);
  }
}

export class ProductMultipleLanguage extends Product {
  productLanguages: ProductLanguage[];
  imageData: any;
  isChangedImage: boolean;

  public constructor(init?: Partial<ProductMultipleLanguage>) {
    super(init);
    Object.assign(this, init);

    if (FuncHelper.isNull(this.productLanguages))
      this.productLanguages = [];
  }
}
