export class AppUrlConsts {
  static urlApiCommon = '/api/common';

  // Administrator
  static urlApiAccount = '/api/account';
  static urlApiRole = '/api/role';

  static urlApiUOM = '/api/uom';

  static urlApiRegion = '/api/region';
  static urlApiCountry = '/api/country';
  static urlApiProvince = '/api/province';
  static urlApiDistrict = '/api/district';
  static urlApiWard = '/api/ward';

  static urlApiLanguage = '/api/language';

  static urlApiSetting = '/api/setting';

  static urlProduct = '/products';
  static urlProductDetail = '/products/detail';
  static urlApiProduct = '/api/product';
  static urlApiProductById = '/api/product/getbyid';

  static urlCategory = '/categories';
  static urlCategoryDetail = '/categories/detail';
  static urlApiCategory = '/api/category';
  static urlApiCategoryById = '/api/category/getbyid';

  static urlCompanyType = '/company-types';
  static urlCompanyTypeDetail = '/company-types/detail';
  static urlApiCompanyType = '/api/company-type';
  static urlApiCompanyTypeById = '/api/company-type/getbyid';

  static urlGrowingMethods = '/growing-methods';
  static urlGrowingMethodDetail = '/growing-methods/detail';
  static urlApiGrowingMethod = '/api/growing-method';
  static urlApiGrowingMethodById = '/api/growing-method/getbyid';

  static urlCultureFields = '/culture-fields';
  static urlCultureFieldDetail = '/culture-fields/detail';
  static urlApiCultureField = '/api/culture-field';
  static urlApiCultureFieldById = '/api/culture-field/getbyid';

  static urlCaption = 'captions';
  static urlCaptionDetail = 'captions/detail';
  static urlApiCaption = '/api/caption';
  static urlApiCaptionById = '/api/caption/getbyid';

  static urlCompany = 'companies';
  static urlCompanyDetail = 'companies/detail';
  static urlApiCompany = '/api/company';
  static urlApiCompanyById = '/api/company/getbyid';
  static urlApiCompanyByUserId = '/api/company/getbyuserid';

  // Partner
  static urlProduction = 'productions';
  static urlProductionDetail = 'productions/detail';
  static urlApiProduction = '/api/production';
  static urlApiProductionById = '/api/production/getbyid';

  static urlProcess = 'processes';
  static urlProcessDetail = 'processes/detail';
  static urlApiProcess = '/api/process';
  static urlApiProcessById = '/api/process/getbyid';

  static urlApiAccountPartner = '/api/account-partner';

  static urlMaterials = '/materials';
  static urlMaterialDetail = '/materials/detail';
  static urlApiMaterial = '/api/material';
  static urlApiMaterialById = '/api/material/getbyid';

  static urlApiMaterialHistory = '/api/material-history';

  static urlApiGTIN = '/api/gtin';

  static urlApiGLN = '/api/gln';
  static urlApiGLNDetail = '/glns/detail';
  static urlApiGLNById = '/api/gln/getbyid';
  static urlGLNDetail = '/gln/detail';

  static urlCMS = '/cms';
  static urlCMSDetail = '/cms/detail';
  static urlApiCMS = '/api/cms';
  static urlApiCMSById = '/api/cms/getbyid';


}

export class Genders {
  public static readonly male: string = 'male';
  public static readonly female: string = 'female';
  public static readonly other: string = 'other';
}

export class ObjectTypes {
  public static readonly farmer: string = 'farmer';
  public static readonly company: string = 'company';
}

export class CommonConsts {
  public static readonly Genders: string[] = ['male', 'female', 'other'];
}





