import { Component, OnInit, Input } from '@angular/core';
import { AuthenticService } from 'src/core/Authentication/authentic.service';

@Component({
  selector: 'app-sidebar-left-inner',
  templateUrl: './sidebar-left-inner.component.html'
})
export class SidebarLeftInnerComponent implements OnInit {
  @Input() Username: string;
  constructor(
    private _authenticSvc: AuthenticService
  ) {
    this.Username = this._authenticSvc.getSession().userName;
  }

  ngOnInit() {
  }
}
