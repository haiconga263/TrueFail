import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';

export class Employee {
  id: number = 0;
  code: string = '';
  fullName: string = '';
  userId: number = 0;
  reportTo: number = 0;
  reportToCode: string = '';
  email: string = '';
  phone: string = '';
  birthday: string = null;
  gender: string = '';
  jobTitle: string = '';
  imageURL: string = '';
  isUsed: boolean = true;

  //mapping
  imageData: string = '';
}


@Injectable()
export class EmployeeService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlEmployees: string = '/api/Employee/gets';
  private urlEmployee: string = '/api/Employee/get';
  private urlAddEmployee: string = '/api/Employee/add';
  private urlUpdateEmployee: string = '/api/Employee/update';
  private urlDeleteEmployee: string = '/api/Employee/delete';

  gets(): Observable<ResultModel<Employee[]>> {
    return this.http.get<ResultModel<Employee[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlEmployees}`, this.authenticSvc.getHttpHeader());
  }
  get(id: number): Observable<ResultModel<Employee>> {
    return this.http.get<ResultModel<Employee>>(`${AppConsts.remoteServiceBaseUrl}${this.urlEmployee}?employeeId=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(employee: Employee) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateEmployee}`, { employee }, this.authenticSvc.getHttpHeader());
  }
  add(employee: Employee) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddEmployee}`, { employee }, this.authenticSvc.getHttpHeader());
  }

  delete(employeeId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteEmployee}`, { employeeId }, this.authenticSvc.getHttpHeader());
  }

  getGenders()
  {
    return[
      {
        id: 'M',
        name: 'Male'
      },
      {
        id: 'F',
        name: 'Female'
      },
      {
        id: 'O',
        name: 'Other'
      }
    ];
  }
}
