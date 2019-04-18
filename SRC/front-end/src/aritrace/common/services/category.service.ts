import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from "rxjs";
import { ResultModel } from "src/core/models/http.model";
import { AppUrlConsts } from "../app-constants";
import { AppConsts } from "src/core/constant/AppConsts";
import { AuthenticService } from "src/core/Authentication/authentic.service";
import { Category } from "../models/category.model";


@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  constructor(private http: HttpClient,
    private authenticSvc: AuthenticService
  ) {
  }

  getById(id: any): Promise<ResultModel<Category>> {
    return this.http.get<ResultModel<Category>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiCategoryById}?id=${id}`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  insert(category: Category): Promise<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiCategory}/insert`, { Model: category }, this.authenticSvc.getHttpHeader()).toPromise();
  }

  update(category: Category): Promise<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiCategory}/update`, { Model: category }, this.authenticSvc.getHttpHeader()).toPromise();
  }
}
