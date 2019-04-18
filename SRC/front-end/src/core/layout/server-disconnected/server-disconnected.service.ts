import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ServerDisconnectedService {


  constructor() {
  }

  changedStatus: (isError: boolean) => void;
}
