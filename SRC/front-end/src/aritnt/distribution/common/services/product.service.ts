export class Product {
    id: number = 0;
    code: string = '';
    imageURL: string = '';
    defaultName: string = '';
    defaultDescription: string = '';
    defaultBuyingPrice: number = 0;
    defaultSellingPrice: number = 0;
    isUsed: boolean = true;
    languages : ProductLanguage[] = [];
    prices: ProductPrice[] = [];
  
    //mapping
    imageData: string = '';
  }
  
  export class ProductLanguage {
    id: number = 0;
    productId: number = 0;
    languageId: number = 0;
    name: string = '';
    description: string = '';
  }
  
  export class ProductPrice {
    id: number = 0;
    productId: number = 0;
    uoMId: number = 0;
    buyingPrice: number = 0;
    sellingPrice: number = 0;
    effectivedDate: Date = new Date(Date.now());
  }