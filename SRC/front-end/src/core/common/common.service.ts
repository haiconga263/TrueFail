import { Injectable } from '@angular/core';
import { CookieService } from './cookie.service';

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  constructor(private cookieSvc: CookieService) {

  }
}

export async function delay(ms: number) {
  return new Promise(resolve => setTimeout(resolve, ms));
}
