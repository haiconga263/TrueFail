import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { Queue } from 'src/core/algorithms/datastructures/queue.structure';

export class Category {
  id: number = 0;
  code: string = '';
  name: string = '';
  parentId: number = null;
  isUsed: boolean = true;

  languages: CategoryLanguage[] = [];
  childs: Category[] = [];

  public constructor(init?: Partial<Category>) {
    Object.assign(this, init);
  }
}

export class CategoryLanguage {
  id: number = 0;
  categoryId: number = 0;
  languageId: number = 0;
  name: string = '';

  public constructor(init?: Partial<CategoryLanguage>) {
    Object.assign(this, init);
  }
}

@Injectable()
export class CategoryService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlCategories: string = '/api/Category/gets';
  private urlAllCategories: string = '/api/Category/gets/all';
  private urlCategoryById: string = '/api/Category/get';
  private urlAddCategory: string = '/api/Category/insert';
  private urlUpdateCategory: string = '/api/Category/update';
  private urlDeleteCategory: string = '/api/Category/delete';

  gets(): Observable<ResultModel<Category[]>> {
    return this.http.get<ResultModel<Category[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlCategories}`, this.authenticSvc.getHttpHeader());
  }

  getsAll(): Observable<ResultModel<Category[]>> {
    return this.http.get<ResultModel<Category[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAllCategories}`, this.authenticSvc.getHttpHeader());
  }

  get(id: number): Observable<ResultModel<Category>> {
    return this.http.get<ResultModel<Category>>(`${AppConsts.remoteServiceBaseUrl}${this.urlCategoryById}?id=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(category: Category): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateCategory}`, { category }, this.authenticSvc.getHttpHeader());
  }
  add(category: Category): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddCategory}`, { category }, this.authenticSvc.getHttpHeader());
  }

  delete(categoryId: number): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteCategory}`, { categoryId }, this.authenticSvc.getHttpHeader());
  }

  removeChildren(id: number, categories: Category[]) {
    let rs: Category[] = FuncHelper.clone(categories);
    let q: Queue<number> = new Queue<number>();

    q.push(id);
    while (true) {
      let cateId = q.pop();
      if (cateId == null || cateId == 0)
        break;
      for (let i = 0; i < rs.length; i++) {
        if (rs[i].parentId == cateId || rs[i].id == cateId) {
          q.push(rs[i].id);
          rs.splice(i, 1);
          i--;
        }
      }
    }
    return rs;
  }
}
