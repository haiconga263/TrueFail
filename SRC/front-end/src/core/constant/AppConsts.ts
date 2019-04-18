import { Modes } from "./AppEnums";

export class AppConsts {
  static appName: string = 'AriTNT';
  static remoteServiceBaseUrl: string;
  static appBaseUrl: string;
  static appAccountUrl: string;
  static languageUrl: string = '/assets/lang/';
  static imageDataUrl: string;
  static defaultLang: string = 'en';
  static rolesAllowed: string[] = [];
  static localeMappings: any = [];
  static appMappings: any = [];

  static isLoaded: boolean = false;
  static configUrl = 'assets/appconfig.json';
  static logoDefaultUrl = 'assets/img/logo-default.png';
  static imageDefaultUrl = 'assets/img/image-default.png';
  static userNamePattern = /^[a-zA-Z0-9@.]+$/;
  static nonSpecialCharPattern = /^[a-zA-Z0-9]+$/;
  static numberPattern = /^[0-9]+$/;
  static nonSpecialCharVietnamesePattern = /^[a-z0-9A-Z_ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ ]+$/;
  static vietnamPhonePattern = /^(03|04|05|09|08|07)[0-9]{8}$/;
  static emailPattern = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  static expiryTime = 1800000; // milisecond
  static mode = Modes.development;
  static usernameDev = "admin";
  static passwordDev = "Abcd1234";
}

export class CookieKeys {
  static userinfo: string = 'userinfo';
  static lang: string = 'lang';
}

export class UrlConsts {
  static urlHome = '';
  static urlHomepage = 'homepage';
  static urlDemo = 'urlDemo';
  static urlLogin = 'login';
  static urlLogout = 'logout';
  static urlRegister = 'register';
  static url404Page = 'not-found';
  static url403Page = 'forbidden';
  static urlAuthenticate = 'authenticate';
}

export class ParamUrlKeys {
  static accesstoken = 'acceasstoken';
  static lang = 'lang';
  static id = 'id';
  static returnurl = 'returnurl';
}