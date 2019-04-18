import { Component, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { ResultCode } from 'src/core/constant/AppEnums';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { ActivatedRoute, Params } from '@angular/router';
import { FulfillmentCollection, FulfillmentService } from '../../fulfillment.service';
import { appUrl } from '../../app-url';
import { ResultModel } from 'src/core/models/http.model';


@Component({
  selector: 'detail',
  templateUrl: './fc-detail.component.html',
  styleUrls: ['./fc-detail.component.css']
})
export class FCDetailComponent extends AppBaseComponent {
  private type: string = 'add';
  private id: string = '';
  private params: Params;

  fcModel: FulfillmentCollection[] = [];
  fulCol: FulfillmentCollection = new FulfillmentCollection();
  constructor(
    injector: Injector,
    private activatedRoute: ActivatedRoute,
    private fcservice: FulfillmentService,
  ) {
    super(injector);

    this.activatedRoute.queryParams.subscribe((params: Params) => {
      this.params = params;

      if (this.params['type'] == 'get') {
        this.type = 'get';
        this.id = this.params["id"];
      }
    });
    this.loadDatasource();
  }
  loadDatasource(callback: () => void = null) {
    this.fcservice.getFulfillmentCollection().subscribe((result) => {
      if (result.result == ResultCode.Success) {
        this.fulCol = result.data.find(x => x.code == this.id);
        this.fcModel.push(this.fulCol);


      }

      if (FuncHelper.isFunction(callback))
        callback();

    });


  }
  private onSelectionChanged(event: any) {
    if (event.selectedRowsData != null && event.selectedRowsData.length == 1) {

    }
  }
  private printReceipt() {
    alert('Đang làm chưa xong');
  }
  private return() {
    console.log('return');
    debugger
    this.router.navigate([appUrl.fulfillmentColLists]);
  }
  // private save() {
  //   this.fcservice.add(this.fulCol).subscribe((result: ResultModel<any>) => {

  //     if (result.result == ResultCode.Success) {
  //       //alert
  //       this.showSuccess(this.lang.instant('Common.AddSuccess'));
  //     }
  //     else {
  //       //alert
  //       this.showError(result.errorMessage);
  //     }
  //   });
  // }
}