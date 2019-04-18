export class SelectListGroup {
  public name: string;
  public selected: string;

  public constructor(init?: Partial<SelectListGroup>) {
    Object.assign(this, init);
  }
}

export class SelectListItem {
  public value: any;
  public text: string;
  public disabled: boolean;
  public selected: string;
  public group: SelectListGroup;

  public constructor(init?: Partial<SelectListItem>) {
    Object.assign(this, init);
  }
}

