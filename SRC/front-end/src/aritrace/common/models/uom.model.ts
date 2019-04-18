export class UOM {
  Id: number;
  Code: string;
  IsUsed: boolean;

  public constructor(init?: Partial<UOM>) {
    Object.assign(this, init);
  }
}
