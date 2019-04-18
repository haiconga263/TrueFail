export class Account {
  id: number;
  userName: string;
  password: string;
  email: string;
  isExternalUser: boolean;
  isSuperadmin: boolean;
  partnerId: number;
  isActived: boolean;
  isUsed: boolean;
  createdBy: number;
  createdDate: Date;
  modifiedBy: number;
  modifiedDate: Date;

  public constructor(init?: Partial<Account>) {
    Object.assign(this, init);
  }
}

