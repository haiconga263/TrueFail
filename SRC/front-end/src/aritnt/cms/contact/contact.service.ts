import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService, Session } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';

export class Contact {
  id: number = 0;
  senderName: string = '';
  senderEmail: string = '';
  phoneNumber: string = '';
  messageContent: string = '';
  isdeleted: boolean = false;
  createdBy: number;
  createdDate: Date;
  modifiedBy: number;
  modifiedDate: Date;
  status: Status;
  statusStr: string = '';
}

export declare enum Status {
  New = 0,
  Read = 1,
  Send = 2
}

@Injectable()
export class ContactService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }


  private urlContactsOnly: string = '/api/ContactHomepage/get/all-contact';
  private urlContact: string = '/api/ContactHomepage/get/contact-detail';
  private urlAddContact: string = '/api/ContactHomepage/add';
  private urlUpdateContact: string = '/api/ContactHomepage/update';
  private urlDeleteContact: string = '/api/ContactHomepage/delete';

  getsOnly(): Observable<ResultModel<Contact[]>> {
    return this.http.get<ResultModel<Contact[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlContactsOnly}`, this.authenticSvc.getHttpHeader());
  }

  get(id: number): Observable<ResultModel<Contact>> {
    return this.http.get<ResultModel<Contact>>(`${AppConsts.remoteServiceBaseUrl}${this.urlContact}?contactId=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(contact: Contact): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateContact}`, { contact: contact }, this.authenticSvc.getHttpHeader());
  }
  add(contact: Contact): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddContact}`, { contact }, this.authenticSvc.getHttpHeader());
  }

  delete(contactId: number): Observable<ResultModel<any>> {
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteContact}`, { contactId }, this.authenticSvc.getHttpHeader());
  }
}
