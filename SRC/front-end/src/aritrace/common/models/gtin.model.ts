export enum GTINTypes {
  gtin_8 = 0,
  gtin_12 = 1,
  gtin_13 = 2,
  gtin_14 = 3,
}

export class GTIN {
  id: number;
  indicatorDigit: number = 0;
  companyCode: number = 0;
  numeric: number = 0;
  checkDigit: number = 0;
  partnerId: number;
  type: GTINTypes;
  usedDate: Date;

  isUsed: boolean;
  createdBy: number;
  createdDate: Date;
  modifiedBy: number;
  modifiedDate: Date;


  public constructor(init?: Partial<GTIN>) {
    Object.assign(this, init);
  }
}

export class GTINInformation extends GTIN {
  code: string;

  public constructor(init?: Partial<GTIN>) {
    super(init);
    Object.assign(this, init);
  }
}
