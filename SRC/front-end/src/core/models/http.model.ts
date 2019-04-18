export class ResultModel<T> {
  public data: T;
  public result: number;
  public errorMessage: string;

  public constructor(init?: Partial<ResultModel<T>>) {
    Object.assign(this, init);
  }
}
