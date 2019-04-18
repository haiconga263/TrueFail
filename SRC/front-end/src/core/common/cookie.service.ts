import { Injectable } from '@angular/core';
import { CookieService as MKCookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class CookieService {
  constructor(
    private cookie: MKCookieService
  ) { }

  set(key: string, value: string, expires = CookieExpires.ShortTime) {
    var expireDate = new Date();
    expireDate.setDate(expireDate.getDate() + expires);

    this.cookie.set(key, value, expireDate, '/')
  }

  get(key: string): string {
    return this.cookie.get(key);
  }

  getAll(): any {
    return this.cookie.getAll()
  }

  check(key: string): boolean {
    return this.cookie.check(key);
  }

  delete(key: string) {
    this.cookie.delete(key, '/');
  }

  deleteAll() {
    this.cookie.deleteAll('/');
  }
}

export enum CookieExpires {
  LongTime = 15, // days
  ShortTime = 7 // days
}
