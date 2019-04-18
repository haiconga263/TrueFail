import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from "rxjs";
import { ResultModel } from "src/core/models/http.model";
import { AppUrlConsts } from "../app-constants";
import { AppConsts } from "src/core/constant/AppConsts";
import { AuthenticService } from "src/core/Authentication/authentic.service";
import { Material, MaterialHistory, MaterialHistoryInformation } from "../models/material.model";


@Injectable({
  providedIn: 'root'
})
export class MaterialService {
  constructor(private http: HttpClient,
    private authenticSvc: AuthenticService
  ) {
  }

  getById(id: any): Promise<ResultModel<Material>> {
    return this.http.post<ResultModel<Material>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiMaterialById}`,{ Model: id }, this.authenticSvc.getHttpHeader()).toPromise();
  }

  insert(material: Material): Promise<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiMaterial}/insert`, { Model: material }, this.authenticSvc.getHttpHeader()).toPromise();
  }

  update(material: Material): Promise<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiMaterial}/update`, { Model: material }, this.authenticSvc.getHttpHeader()).toPromise();
  }

  generatecode(): Promise<ResultModel<string>> {
    return this.http.post<ResultModel<string>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiMaterial}/generate-code`, {}, this.authenticSvc.getHttpHeader()).toPromise();
  }

  getHistoriesById(materialId: any): Promise<ResultModel<MaterialHistoryInformation[]>> {
    return this.http.post<ResultModel<MaterialHistoryInformation[]>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiMaterialHistory}/all`, { Model: materialId }, this.authenticSvc.getHttpHeader()).toPromise();
  }

  insertHistory(materialHistory: MaterialHistory): Promise<ResultModel<number>> {
    return this.http.post<ResultModel<number>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiMaterialHistory}/insert`, { Model: materialHistory }, this.authenticSvc.getHttpHeader()).toPromise();
  }
}
