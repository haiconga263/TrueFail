import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { LoginModel } from './login.model';
import { AuthenticService, Session } from 'src/core/Authentication/authentic.service';
import { ResultModel } from 'src/core/models/http.model';
import { AppConsts } from 'src/core/constant/AppConsts';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private urlLogin: string = '/api/User/login';
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  login(model: LoginModel): Observable<ResultModel<Session>> {
    return this.http.post<ResultModel<Session>>(`${AppConsts.remoteServiceBaseUrl}${this.urlLogin}`, model, this.authenticSvc.getHttpHeader())
    //.pipe(
    //  tap(_ => this.log('login ----')),
    //  catchError(this.handleError<ResultModel>('login fail!'))
    //);
  }

  //private handleError<T>(operation = 'operation', result?: T) {
  //  return (error: any): Observable<T> => {

  //    // TODO: send the error to remote logging infrastructure
  //    console.error(error); // log to console instead

  //    // TODO: better job of transforming error for user consumption
  //    this.log(`${operation} failed: ${error.message}`);

  //    // Let the app keep running by returning an empty result.
  //    return of(result as T);
  //  };
  //}

  ///** Log a HeroService message with the MessageService */
  //private log(message: string) {
  //  console.log(message);
  //  this.messageService.add(`LoginService: ${message}`);
  //}

}
