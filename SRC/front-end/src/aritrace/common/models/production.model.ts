import { ProductionImage } from "./production-image.model";
import { GTIN, GTINInformation } from "./gtin.model";
import { FuncHelper } from "src/core/helpers/function-helper";

export class Production {
  public id: number;
  public name: string;
  public partnerId: number;
  public productId: number;
  public gtinId: string;
  public growingMethodId: number;
  public countryOfOrigin: string;
  public trademark: string;
  public commercialClaim: string;
  public productSize: string;
  public grade: string;
  public colour: string;
  public shape: string;
  public variety: string;
  public commercialType: string;
  public colourOfFlesh: string;
  public postHarvestTreatment: string;
  public postHarvestProcessing: string;
  public cookingType: string;
  public seedProperties: string;
  public tradePackageContentQuantity: string;
  public tradeUnitPackageType: string;
  public consumerUnitContentQuantity: string;
  public tradeUnit: string;
  public comsumerUnitPackageType: string;
  public comsumerUnit: string;
  public productionImageId: number;
  public isUsed: boolean;
  public createdBy: number;
  public createdDate: Date;
  public modifiedBy: number;
  public modifiedDate: Date;

  public constructor(init?: Partial<Production>) {
    Object.assign(this, init);
  }
}


export class ProductionInformation extends Production {
  productionImage: ProductionImage;
  gtin: GTINInformation;

  public constructor(init?: Partial<ProductionInformation>) {
    super(init);
    Object.assign(this, init);

    if (FuncHelper.isNull(this.productionImage))
      this.productionImage = new ProductionImage();

    if (FuncHelper.isNull(this.gtin))
      this.gtin = new GTINInformation();
  }
}
