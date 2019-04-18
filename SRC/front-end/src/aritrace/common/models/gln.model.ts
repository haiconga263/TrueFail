import { Country } from "./country.model";
import { Company } from "./company.model";

export class GLN {
  id: number;
  countryId: number = 0;
  code: string;
  partnerId: number;
  usedDate: Date;
  isUsed: boolean;
  isPublic: boolean;
  isDeleted: boolean;
  createdBy: number;
  createdDate: Date;
  modifiedBy: number;
  modifiedDate: Date;


  public constructor(init?: Partial<GLN>) {
    Object.assign(this, init);
  }
}

export class GLNDetailViewModel extends GLN {

  country: Country;
  company: Company;
  public constructor(init?: Partial<GLN>) {
    super(init);
    Object.assign(this, init);
  }
}
