export class Role {
  public id: number;
  public name: string;
  public description: string;
  public isUsed: boolean;
  public createdBy: number;
  public createdDate: Date;
  public modifiedBy: number;
  public modifiedDate: Date;

  public constructor(init?: Partial<Role>) {
    Object.assign(this, init);
  }
}
