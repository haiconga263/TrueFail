import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService, Session } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';

export class User {
  id: number = 0;
  userName: string = '';
  email: string = '';
  securityPassword: string = '';
  passwordResetCode: string = '';
  password: string = '';
  comfirmPassword: string = '';
  isActived: boolean = true;
  createdBy: number = 0;
  isExternalUser: boolean = false;
  createdDate: Date;

  roles: Role[] = [];
}

export class NewUser {
  userName: string = '';
  email: string = '';
  password: string = '';
}

export class Role {
  id: number = 0;
  name: string = '';
}


@Injectable()
export class UserService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlUsers: string = '/api/User/gets/userwithrole';

  getUsers(type: number = 0): Observable<ResultModel<User[]>> {
    return this.http.get<ResultModel<User[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUsers}?userType=` + type, this.authenticSvc.getHttpHeader());
  }
}
