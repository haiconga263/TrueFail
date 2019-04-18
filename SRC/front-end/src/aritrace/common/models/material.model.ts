import { CultureField } from "./culture-field.model";
import { Account } from "./account.model";

export class Material {
  id: number;
  code: string;
  name: string;
  description: string;
  productId: number;
  parnerId: number;
  isUsed: boolean;
  createdBy: number;
  createdDate: Date;
  modifiedBy: number;
  modifiedDate: Date;


  public constructor(init?: Partial<Material>) {
    Object.assign(this, init);
  }
}


export class MaterialHistory {
  id: number;
  materialId: number;
  cultureFieldId: number;
  value: string;
  comment: string;
  isDeleted: boolean;
  createdBy: number;
  createdDate: Date;
  deletedBy: number;
  deletedDate: Date;


  public constructor(init?: Partial<MaterialHistory>) {
    Object.assign(this, init);
  }
}


export class MaterialHistoryInformation extends MaterialHistory {
  cultureField: CultureField;
  userCreated: Account;
  userDeleted: Account;

  public constructor(init?: Partial<MaterialHistoryInformation>) {
    super(init);
    Object.assign(this, init);
  }
}
