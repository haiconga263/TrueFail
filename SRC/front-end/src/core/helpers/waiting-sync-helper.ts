export enum WaitingSyncKeys {
  initLanguage,
  initAppBaseComponent
}

export class WaitingSyncHelper {
  private static _lastRequest: any = {};

  public static canExcute(key: WaitingSyncKeys, timeout: number = 1000): boolean {
    let now = new Date();
    if (WaitingSyncHelper._lastRequest[key] == null
      || (WaitingSyncHelper._lastRequest[key] != null && now.getTime() - WaitingSyncHelper._lastRequest[key].getTime() > 1000)) {
      WaitingSyncHelper._lastRequest[key] = now;
      return true;
    }
    else return false;
  }
}
