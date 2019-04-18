
// import { Component, OnInit, Injector, ViewChild } from '@angular/core';
// import { AppBaseComponent } from 'src/core/basecommon/app-base.component';

// import { ResultCode } from 'src/core/constant/AppEnums';
// import { DxDataGridComponent } from 'devextreme-angular';
// import ArrayStore from 'devextreme/data/array_store'
// import { FuncHelper } from 'src/core/helpers/function-helper';
// import { appUrl } from '../../app-url';
// import { ExcelService } from 'src/core/services/excel.service';
// import { ImageService, Image } from '../image.service';

// @Component({
//   selector: 'image',
//   templateUrl: './image.component.html',
//   styleUrls: ['./image.component.css']
// })
// export class ImageComponent extends AppBaseComponent {
//   @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
//   dataSource: ArrayStore;
//   selectedRows: number[];
//   private images: Image[] = [];
//   abc: object[];

//   constructor(
//     injector: Injector,
//     private imageSvc: ImageService,
//     private excelSvc: ExcelService
//   ) {
//     super(injector);
//     this.loadDatasource();
//   }

//   async loadDatasource(callback: () => void = null) {
//     let rs = await this.imageSvc.getsOnly("images").toPromise()
//     if (rs.result == ResultCode.Success) {
//       this.dataSource = new ArrayStore({
//         key: "id",
//         data: rs.data
//       });
//       // this.images = rs.data;

//       console.log(this.images);
//       if (FuncHelper.isFunction(callback))
//         callback();
//     }
//   }
//   customizeSizeText(e) {
//     if (e.value !== null) {
//       return Math.ceil(e.value / 1024) + " KB";
//     }
//   }
//   // grid = {
//   //   delete: async () => {
//   //     console.log('delete');
//   //     if(this.selectedRows.length == 1) {
//   //       this.imageSvc.delete(this.selectedRows[0]).subscribe((result) => {
//   //         if(result.result == ResultCode.Success)
//   //         {
//   //           //alert
//   //           this.showSuccess(this.lang.instant('Common.DeleteSuccess'));
//   //           this.grid.refresh();
//   //         }
//   //         else
//   //         {
//   //           //alert
//   //           this.showError(this.lang.instant(result.errorMessage));
//   //         }
//   //       });
//   //     }
//   //   },
//   //   refresh: () => {
//   //     this.loadDatasource(() => {
//   //       this.dataGrid.instance.refresh();
//   //     });
//   //   },
//   //   update: () => {
//   //     console.log('update');
//   //     if(this.selectedRows.length == 1)
//   //     {
//   //       this.router.navigate([appUrl.imageDetail],
//   //       {
//   //         queryParams: {
//   //           type: 'update',
//   //           id: this.selectedRows[0]
//   //         }
//   //       });
//   //     }
//   //   },
//   //   create: () => {
//   //     console.log('create');
//   //     this.router.navigate([appUrl.imageDetail],
//   //     {
//   //       queryParams: {
//   //         type: 'add'
//   //       }
//   //     });
//   //   }
//   // };

//   // itemClick(data: any) {
//   //   console.log(data);
//   //   if (data.itemData.action == 'image') {
//   //     // this.abivinSvc.getVehicleType().subscribe(rs => {
//   //     //   if(rs.result == ResultCode.Success) {
//   //     //     this.excelSvc.exportAsExcelFile(rs.data, "Image");
//   //     //   }
//   //     //   else {
//   //     //     this.showError(rs.errorMessage);
//   //     //   }
//   //     // });
//   //   }
//   // }

//   ngOnInit() {
//   }
// }
