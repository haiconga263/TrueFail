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

  private urlUsers: string = '/api/User/get/users';
  private urlUser: string = '/api/User/get/user';
  private urlUserWithRole: string = '/api/User/get/userwithrole';
  private urlUsersWithRole: string = '/api/User/gets/userwithrole';
  private urlUserNotAssign: string = '/api/User/get/users/not-assign';
  private urlUserNotAssignBy: string = '/api/User/get/users/not-assign/by';
  private urlAddUser: string = '/api/User/add';
  private urlUpdateUser: string = '/api/User/update';
  private urlRemoveUser: string = '/api/User/remove';
  private urResetPassword: string = '/api/User/reset-password';

  private urlRoles: string = '/api/User/get/roles';
  private urlSaveChangeRoles: string = '/api/User/changeroles';

  getUsers(): Observable<ResultModel<User[]>> {
    return this.http.get<ResultModel<User[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUsers}`, this.authenticSvc.getHttpHeader());
  }

  getUser(userId: number): Observable<ResultModel<User>> {
    return this.http.get<ResultModel<User>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUser}?userId=${userId}`, this.authenticSvc.getHttpHeader());
  }

  getUsersWithRole(userType: number): Observable<ResultModel<User[]>> {
    return this.http.get<ResultModel<User[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUsersWithRole}?userType=${userType}`, this.authenticSvc.getHttpHeader());
  }

  getUserWithRole(userId: number): Observable<ResultModel<User>> {
    return this.http.get<ResultModel<User>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUserWithRole}?userId=${userId}`, this.authenticSvc.getHttpHeader());
  }

  addUser(model: NewUser): Observable<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddUser}`, model, this.authenticSvc.getHttpHeader());
  }

  updateUser(user: User): Observable<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateUser}`, { user }, this.authenticSvc.getHttpHeader());
  }

  removeUser(userId: number): Observable<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${this.urlRemoveUser}`, { userId }, this.authenticSvc.getHttpHeader());
  }

  resetPassword(userId: number): Observable<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${this.urResetPassword}`, { userId }, this.authenticSvc.getHttpHeader());
  }

  saveRoles(userId: number, roleIds: number[]): Observable<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${this.urlSaveChangeRoles}`, { userId, roleIds }, this.authenticSvc.getHttpHeader());
  }

  getUsersNotAssign(): Observable<ResultModel<User[]>> {
    return this.http.get<ResultModel<User[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUserNotAssign}`, this.authenticSvc.getHttpHeader());
  }

  getUsersNotAssignBy(isExternalUser: boolean, roleName: string): Observable<ResultModel<User[]>> {
    let param = '?isExternalUser=' + isExternalUser;
    if(roleName != null && roleName != '')
    {
      param += '&roleName=' + roleName;
    }
    return this.http.get<ResultModel<User[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUserNotAssignBy}` + param, this.authenticSvc.getHttpHeader());
  }

  getRoles(type: number = 0): Observable<ResultModel<Role[]>> {
    // 1: external role. 0: internal role. 2: all
    let param = '?type=' + type;
    return this.http.get<ResultModel<Role[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlRoles}` + param, this.authenticSvc.getHttpHeader());
  }
}
