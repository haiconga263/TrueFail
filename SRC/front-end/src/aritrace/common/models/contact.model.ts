export class Contact {
  id: number;
  objectType: string;
  objectId: number;
  name: string;
  phone: string;
  email: string;
  gender: string;
  isDeleted: boolean;
  createdBy: number;
  createdDate: Date;
  modifiedBy: number;
  modifiedDate: Date;

  public constructor(init?: Partial<Contact>) {
    Object.assign(this, init);
  }
}
