export class Category {
  id: number;
  code: string;
  name: string;
  captionNameId: number;
  isUsed: boolean;
  createdBy: number;
  createdDate: Date;
  modifiedBy: number;
  modifiedDate: Date;

  public constructor(init?: Partial<Category>) {
    Object.assign(this, init);
  }
}
