export class Setting {
  public id: number;
  public name: string;
  public value: string;
  public description: string;
  public createdBy: number;
  public createdDate: Date;
  public modifiedBy: number;
  public modifiedDate: Date;

  public constructor(init?: Partial<Setting>) {
    Object.assign(this, init);
  }
}
