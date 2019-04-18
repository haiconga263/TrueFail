export class Region {
  public id: number;
  public code: string;
  public name: string;
  public countryId: number;
  public isUsed: boolean;
  public createdBy: number;
  public createdDate: Date;
  public modifiedBy: number;
  public modifiedDate: Date;

  public constructor(init?: Partial<Region>) {
    Object.assign(this, init);
  }
}
