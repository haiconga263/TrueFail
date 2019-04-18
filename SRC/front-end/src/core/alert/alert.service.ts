import { Injectable } from '@angular/core';
import { FuncHelper } from '../helpers/function-helper';
import { ResultModel } from '../models/http.model';
import { ResultCode } from '../constant/AppEnums';

export enum AlertTypes {
  success,
  error,
  warning,
  info,
  question
}

export class Alert {
  public title?: string;
  public titleText?: string;
  public text?: string;
  public type: AlertTypes;
  public html?: string | HTMLElement | JQuery;
  public footer?: string | JQuery;
  public backdrop?: boolean | string;
  public toast?: boolean;
  public target?: string;
  public input?:
    'text' | 'email' | 'password' | 'number' | 'tel' | 'range' | 'textarea' | 'select' | 'radio' | 'checkbox' |
    'file' | 'url';
  public width?: number | string;
  public padding?: number;
  public background?: string;
  public position?:
    'top' | 'top-start' | 'top-end' | 'top-left' | 'top-right' |
    'center' | 'center-start' | 'center-end' | 'center-left' | 'center-right' |
    'bottom' | 'bottom-start' | 'bottom-end' | 'bottom-left' | 'bottom-right';
  public grow?: 'row' | 'column' | 'fullscreen' | false;
  public customClass?: string;
  public customContainerClass?: string;
  public timer?: number;
  public animation?: boolean;
  public heightAuto?: boolean;
  public allowOutsideClick?: boolean;
  public allowEscapeKey?: boolean;
  public allowEnterKey?: boolean;
  public stopKeydownPropagation?: boolean;
  public keydownListenerCapture?: boolean;
  public showConfirmButton: boolean = true;
  public confirmButtonText: string;
  public showCancelButton: boolean = false;
  public cancelButtonText?: string;
  public confirmButtonColor?: string;
  public cancelButtonColor?: string;
  public confirmButtonClass?: string;
  public cancelButtonClass?: string;
  public confirmButtonAriaLabel?: string;
  public cancelButtonAriaLabel?: string;
  public buttonsStyling?: boolean;
  public reverseButtons?: boolean;
  public focusConfirm?: boolean;
  public focusCancel?: boolean;
  public showCloseButton?: boolean;
  public closeButtonAriaLabel?: string;
  public showLoaderOnConfirm?: boolean;
  public imageUrl?: string;
  public imageWidth?: number;
  public imageHeight?: number;
  public imageAlt?: string;
  public imageClass?: string;
  public inputPlaceholder?: string;
  public inputValue?: string;
  public inputOptions?: Map<string, string> | { [inputValue: string]: string };
  public inputAutoTrim?: boolean;
  public inputAttributes?: { [attribute: string]: string; };
  public validationMessage?: string;
  public inputClass?: string;
  public progressSteps?: string[];
  public currentProgressStep?: string;
  public progressStepsDistance?: string;
  public onBeforeOpen?: (modalElement: HTMLElement) => void;
  public onAfterClose?: () => void;
  public onOpen?: (modalElement: HTMLElement) => void;
  public onClose?: (modalElement: HTMLElement) => void;
  public expectRejections?: boolean;
  public onConfirm?: () => void;
  public onCancel?: () => void;

  public constructor(init?: Partial<Alert>) {
    Object.assign(this, init);
  }
}

export class SuccessAlert extends Alert {

  public constructor(init?: Partial<Alert>) {
    super(init);

    this.showConfirmButton = FuncHelper.isNull(this.showConfirmButton) ? true : this.showConfirmButton;
    this.showCancelButton = FuncHelper.isNull(this.showCancelButton) ? false : this.showCancelButton;
    this.type = AlertTypes.success;
  }
}

export class ErrorAlert extends Alert {

  public constructor(init?: Partial<Alert>) {
    super(init);

    this.showConfirmButton = FuncHelper.isNull(this.showConfirmButton) ? true : this.showConfirmButton;
    this.showCancelButton = FuncHelper.isNull(this.showCancelButton) ? false : this.showCancelButton;
    this.type = AlertTypes.error;
  }
}

export class WarningAlert extends Alert {

  public constructor(init?: Partial<Alert>) {
    super(init);

    this.showConfirmButton = FuncHelper.isNull(this.showConfirmButton) ? true : this.showConfirmButton;
    this.showCancelButton = FuncHelper.isNull(this.showCancelButton) ? true : this.showCancelButton;
    this.type = AlertTypes.warning;
  }
}

export class InforAlert extends Alert {

  public constructor(init?: Partial<Alert>) {
    super(init);

    this.showConfirmButton = FuncHelper.isNull(this.showConfirmButton) ? true : this.showConfirmButton;
    this.showCancelButton = FuncHelper.isNull(this.showCancelButton) ? false : this.showCancelButton;
    this.type = AlertTypes.info;
  }
}

export class QuestionAlert extends Alert {

  public constructor(init?: Partial<Alert>) {
    super(init);

    this.showConfirmButton = FuncHelper.isNull(this.showConfirmButton) ? true : this.showConfirmButton;
    this.showCancelButton = FuncHelper.isNull(this.showCancelButton) ? true : this.showCancelButton;
    this.type = AlertTypes.question;
  }
}

export class Message {
  public content: string;
  public type: string;

  public constructor(init?: Partial<Message>) {
    Object.assign(this, init);
  }
}

@Injectable({ providedIn: 'root' })
export class AlertService {
  message: Message[] = [];

  onAlert: (alert: Alert) => Promise<any>;

  constructor() {
    this.message = [];
  }

  addMsg(message: Message) {
    this.message.push(message);
  }

  alertByHttpResult(result: ResultModel<any>) {
    let alert: Alert;
    switch (result.result) {
      case ResultCode.Fail:
      case ResultCode.FailMsg:
        alert = new ErrorAlert({
          title: 'Alert.TitleError',
          text: FuncHelper.isNull(result.errorMessage) ? 'Alert.TextError' : result.errorMessage,
        });
        break;
      case ResultCode.Success:
        alert = new SuccessAlert({
          title: 'Alert.TitleSuccess',
          text: FuncHelper.isNull(result.errorMessage) ? 'Alert.TextSuccess' : result.errorMessage
        });
        break;
      case ResultCode.Warning:
        alert = new WarningAlert({
          title: 'Alert.TitleWarning',
          text: FuncHelper.isNull(result.errorMessage) ? 'Alert.TextWarning' : result.errorMessage,
          showCancelButton: false,
        });
        break;
      default:
        alert = new WarningAlert({
          title: 'Alert.TitleInfo',
          text: FuncHelper.isNull(result.errorMessage) ? 'Alert.TextInfo' : result.errorMessage,
          showCancelButton: false,
        });
        break;
    }
    this.alert(alert);
  }

  alert(alert: Alert): Promise<any> {
    if (FuncHelper.isFunction(this.onAlert))
      return this.onAlert(alert);
    return Promise.resolve();
  }

  clearMsgs() {
    this.message = [];
  }
}
