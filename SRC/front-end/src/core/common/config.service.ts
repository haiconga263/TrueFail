import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppConsts } from '../constant/AppConsts';
import { ResultCode } from '../constant/AppEnums';
import { Config } from 'protractor';
import { FuncHelper } from '../helpers/function-helper';
import { Queue } from '../algorithms/datastructures/queue.structure';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  _queue: Queue<() => void>;

  constructor(
    private httpClient: HttpClient
  ) {
    this._queue = new Queue();
  }


  public reqConfig() {
    return this.httpClient.get(`${AppConsts.configUrl}?${Math.round(new Date().getTime() / AppConsts.expiryTime)}`);
  }

  public getConfig(callback: () => void): void {
    if (AppConsts.isLoaded) {
      callback();
    }
    else {
      this.pushEvent(callback);
      this.reqConfig().subscribe((data: Config) => {
        AppConsts.isLoaded = true;

        let props = Object.getOwnPropertyNames(data);

        for (var i = 0; i < props.length; i++) {
          if (!FuncHelper.isNull(data[props[i]])) {
            try {
              AppConsts[props[i]] = FuncHelper.isNull(data[props[i]]) ? AppConsts[props[i]] : data[props[i]];
            }
            catch (e) { }
          }
        }

        while (true) {
          let cb = this._queue.pop();
          if (FuncHelper.isFunction(cb)) {
            cb();
          }
          else break;
        }
      });
    }
  }

  public pushEvent(callback: () => void) {
    if (!AppConsts.isLoaded) {
      this._queue.push(callback);
    }
    else {
      callback();
    }
  }
}

export function HttpLoaderFactory(injector: Injector) {
  let configService = injector.get(ConfigService);
  return () => configService.getConfig(() => { });
}
