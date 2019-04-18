export class Province {
  public id: number;
  public code: string;
  public name: string;
  public phoneCode: string;
  public countryId: number;
  public regionId: number;
  public isUsed: boolean;
  public createdBy: number;
  public createdDate: Date;
  public modifiedBy: number;
  public modifiedDate: Date;

  public constructor(init?: Partial<Province>) {
    Object.assign(this, init);
  }
}
