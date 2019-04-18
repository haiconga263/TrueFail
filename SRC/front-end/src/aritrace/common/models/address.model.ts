export class Address {
  id: number;
  objectType: string;
  objectId: number;
  street: string;
  countryId: number;
  provinceId: number;
  districtId: number;
  wardId: number
  longitude: number;
  latitude: number
  isDeleted: boolean;
  createdBy: number;
  createdDate: Date;
  modifiedBy: number;
  modifiedDate: Date;

  public constructor(init?: Partial<Address>) {
    Object.assign(this, init);
  }
}

