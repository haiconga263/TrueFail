import { Contact } from "./contact.model";
import { Address } from "./address.model";

export class Company {
  id: number;
  name: string;
  taxCode: string;
  website: string;
  contactId: number;
  addressId: number;
  logoPath: string;
  description: string;
  companyTypeId: number;
  isPartner: boolean;
  gS1Code: number;
  isDeleted: boolean;
  isUsed: boolean;
  createdBy: number;
  createdDate: Date;
  modifiedBy: number;
  modifiedDate: Date;

  public constructor(init?: Partial<Company>) {
    Object.assign(this, init);
  }
}

export class CompanyViewModel extends Company {

  contact: Contact;
  address: Address;
  imageData: any;
  isChangedImage: boolean;

  public constructor(init?: Partial<CompanyViewModel>) {
    super(init);
    Object.assign(this, init);
  }
}
