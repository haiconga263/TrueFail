export class Country {
  id: number = 0;
  code: string = '';
  name: string = '';
  phoneCode: string = '';
  isUsed: boolean = true;
  createdBy: number;
  createdDate: Date;
  modifiedBy: number;
  modifiedDate: Date;

  public constructor(init?: Partial<Country>) {
    Object.assign(this, init);
  }
}
