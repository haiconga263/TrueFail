export class Ward {
  public id: number;
  public code: string;
  public name: string;
  public countryId: number;
  public provinceId: number;
  public districtId: number;
  public isUsed: boolean;
  public createdBy: number;
  public createdDate: Date;
  public modifiedBy: number;
  public modifiedDate: Date;

  public constructor(init?: Partial<Ward>) {
    Object.assign(this, init);
  }
}
