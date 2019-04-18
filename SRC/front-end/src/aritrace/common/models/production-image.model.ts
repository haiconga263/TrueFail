export class ProductionImage {
  public id: number;
  public front: string;
  public left: string;
  public top: string;
  public back: string;
  public right: string;
  public bottom: string;

  public constructor(init?: Partial<ProductionImage>) {
    Object.assign(this, init);
  }
}

export class ProductionImageData extends ProductionImage {
  public imageDataFront: string;
  public isImageFrontChanged: string;

  public imageDataLeft: string;
  public isImageLeftChanged: boolean;

  public imageDataTop: string;
  public isImageTopChanged: boolean;

  public imageDataBack: string;
  public isImageBackChanged: boolean;

  public imageDataRight: string;
  public isImageRightChanged: boolean;

  public imageDataBottom: string;
  public isImageBottomChanged: boolean;

  public constructor(init?: Partial<ProductionImageData>) {
    super(init);
    Object.assign(this, init);
  }
}
