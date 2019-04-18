export const CULTURE_FIELD_TYPES = ['text', 'number', 'boolean', 'date', 'time', 'datetime', 'list']

export class CultureField {
  id: number;
  codeName: string;
  name: number;
  description: string;
  dataType: string;
  source: string;
  minimum: string;
  maximum: string;
  isRequired: number;
  isUsed: boolean;
  createdBy: number;
  createdDate: Date;
  modifiedBy: number;
  modifiedDate: Date;

  public constructor(init?: Partial<CultureField>) {
    Object.assign(this, init);
  }
}

