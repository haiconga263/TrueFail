import { Component, OnInit, Input, Injector, ViewChild } from '@angular/core';
import { BaseComponent } from '../basecommon/base.component';
import { SwalComponent } from '@toverux/ngx-sweetalert2';
import { AlertService, Alert, AlertTypes } from './alert.service';
import { FuncHelper } from '../helpers/function-helper';
import { SweetAlertType } from 'sweetalert2';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.css']
})
export class AlertComponent extends BaseComponent {
  @ViewChild('swal') public swal: SwalComponent;

  constructor(injector: Injector,
    public alertSvc: AlertService
  ) {
    super(injector);
    this.useSweetAlert = this.useSweetAlert.bind(this);
    this.alertSvc.onAlert = this.useSweetAlert;
  }

  useSweetAlert(alert: Alert): Promise<any> {
    this.swal = new SwalComponent({
      showConfirmButton: alert.showConfirmButton,

      confirmButtonText: FuncHelper.isNull(alert.confirmButtonText) ?
        this.lang.instant('Alert.ConfirmButton') : this.lang.instant(alert.confirmButtonText),

      showCancelButton: alert.showCancelButton,
      type: this.convertToSweetAlertType(alert.type),
      title: FuncHelper.isNull(alert.title) ? this.convertToSweetAlertType(alert.type).toString() : this.lang.instant(alert.title),
      text: FuncHelper.isNull(alert.text) ? '' : this.lang.instant(alert.text),
      titleText: FuncHelper.isNull(alert.titleText) ? null : this.lang.instant(alert.titleText),
      html: FuncHelper.isNull(alert.html) ? null : alert.html,
      footer: FuncHelper.isNull(alert.footer) ? null : alert.footer,
      backdrop: FuncHelper.isNull(alert.backdrop) ? true : alert.backdrop,
      toast: FuncHelper.isNull(alert.toast) ? false : alert.toast,
      target: FuncHelper.isNull(alert.target) ? 'body' : alert.target,
      input: FuncHelper.isNull(alert.input) ? null : alert.input,
      width: FuncHelper.isNull(alert.width) ? null : alert.width,
      padding: FuncHelper.isNull(alert.padding) ? null : alert.padding,
      background: FuncHelper.isNull(alert.background) ? '#fff' : alert.background,
      position: FuncHelper.isNull(alert.position) ? 'center' : alert.position,
      grow: FuncHelper.isNull(alert.grow) ? false : alert.grow,
      customClass: FuncHelper.isNull(alert.customClass) ? '' : alert.customClass,
      customContainerClass: FuncHelper.isNull(alert.customContainerClass) ? '' : alert.customContainerClass,
      timer: FuncHelper.isNull(alert.timer) ? null : alert.timer,
      animation: FuncHelper.isNull(alert.animation) ? true : alert.animation,
      heightAuto: FuncHelper.isNull(alert.heightAuto) ? true : alert.heightAuto,
      allowOutsideClick: FuncHelper.isNull(alert.allowOutsideClick) ? true : alert.allowOutsideClick,
      allowEscapeKey: FuncHelper.isNull(alert.allowEscapeKey) ? true : alert.allowEscapeKey,
      allowEnterKey: FuncHelper.isNull(alert.allowEnterKey) ? true : alert.allowEnterKey,
      stopKeydownPropagation: FuncHelper.isNull(alert.stopKeydownPropagation) ? true : alert.stopKeydownPropagation,
      keydownListenerCapture: FuncHelper.isNull(alert.keydownListenerCapture) ? false : alert.keydownListenerCapture,
      cancelButtonText: FuncHelper.isNull(alert.cancelButtonText) ? 'Cancel' : alert.cancelButtonText,
      confirmButtonColor: FuncHelper.isNull(alert.confirmButtonColor) ? '#3085d6' : alert.confirmButtonColor,
      cancelButtonColor: FuncHelper.isNull(alert.cancelButtonColor) ? '#aaa' : alert.cancelButtonColor,
      confirmButtonClass: FuncHelper.isNull(alert.confirmButtonClass) ? null : alert.confirmButtonClass,
      cancelButtonClass: FuncHelper.isNull(alert.cancelButtonClass) ? null : alert.cancelButtonClass,
      confirmButtonAriaLabel: FuncHelper.isNull(alert.confirmButtonAriaLabel) ? '' : this.lang.instant(alert.confirmButtonAriaLabel),
      cancelButtonAriaLabel: FuncHelper.isNull(alert.cancelButtonAriaLabel) ? '' : this.lang.instant(alert.cancelButtonAriaLabel),
      buttonsStyling: FuncHelper.isNull(alert.buttonsStyling) ? true : alert.buttonsStyling,
      reverseButtons: FuncHelper.isNull(alert.reverseButtons) ? false : alert.reverseButtons,
      focusConfirm: FuncHelper.isNull(alert.focusConfirm) ? true : alert.focusConfirm,
      focusCancel: FuncHelper.isNull(alert.focusCancel) ? false : alert.focusCancel,
      showCloseButton: FuncHelper.isNull(alert.showCloseButton) ? false : alert.showCloseButton,
      closeButtonAriaLabel: FuncHelper.isNull(alert.closeButtonAriaLabel) ? 'Close this dialog' : this.lang.instant(alert.closeButtonAriaLabel),
      showLoaderOnConfirm: FuncHelper.isNull(alert.showLoaderOnConfirm) ? false : alert.showLoaderOnConfirm,
      imageUrl: FuncHelper.isNull(alert.imageUrl) ? null : alert.imageUrl,
      imageWidth: FuncHelper.isNull(alert.imageWidth) ? null : alert.imageWidth,
      imageHeight: FuncHelper.isNull(alert.imageHeight) ? null : alert.imageHeight,
      imageAlt: FuncHelper.isNull(alert.imageAlt) ? '' : alert.imageAlt,
      imageClass: FuncHelper.isNull(alert.imageClass) ? null : alert.imageClass,
      inputPlaceholder: FuncHelper.isNull(alert.inputPlaceholder) ? '' : alert.inputPlaceholder,
      inputValue: FuncHelper.isNull(alert.inputValue) ? '' : alert.inputValue,
      inputOptions: FuncHelper.isNull(alert.inputOptions) ? null : alert.inputOptions,
      inputAutoTrim: FuncHelper.isNull(alert.inputAutoTrim) ? true : alert.inputAutoTrim,
      inputAttributes: FuncHelper.isNull(alert.inputAttributes) ? null : alert.inputAttributes,
      validationMessage: FuncHelper.isNull(alert.validationMessage) ? null : alert.validationMessage,
      inputClass: FuncHelper.isNull(alert.inputClass) ? null : alert.inputClass,
      progressSteps: FuncHelper.isNull(alert.progressSteps) ? [] : alert.progressSteps,
      currentProgressStep: FuncHelper.isNull(alert.currentProgressStep) ? null : alert.currentProgressStep,
      progressStepsDistance: FuncHelper.isNull(alert.progressStepsDistance) ? null : alert.progressStepsDistance,
      onBeforeOpen: FuncHelper.isNull(alert.onBeforeOpen) ? null : alert.onBeforeOpen,
      onAfterClose: FuncHelper.isNull(alert.onAfterClose) ? null : alert.onAfterClose,
      onOpen: FuncHelper.isNull(alert.onOpen) ? null : alert.onOpen,
      onClose: FuncHelper.isNull(alert.onClose) ? null : alert.onClose,
      expectRejections: FuncHelper.isNull(alert.expectRejections) ? false : alert.expectRejections,
    });

    if (FuncHelper.isFunction(alert.onConfirm))
      this.swal.confirm.subscribe(alert.onConfirm);

    if (FuncHelper.isFunction(alert.onCancel))
      this.swal.cancel.subscribe(alert.onCancel);

    return this.swal.show();
  }

  convertToSweetAlertType(type: AlertTypes): SweetAlertType {
    switch (type) {
      case AlertTypes.success:
        return 'success';
      case AlertTypes.error:
        return 'error';
      case AlertTypes.warning:
        return 'warning';
      case AlertTypes.info:
        return 'info';
      case AlertTypes.question:
        return 'question';
      default:
        return 'info';
    }
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
