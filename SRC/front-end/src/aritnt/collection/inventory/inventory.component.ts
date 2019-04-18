import { Component, ElementRef, Injector, ViewChild } from '@angular/core';
import { InventoryService, Inventory } from './inventory.service';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { ResultCode } from 'src/core/constant/AppEnums';
import { ProductService, Product } from '../common/services/product.service';
import { UoMService, UoM } from '../common/services/uom.service';
import { Collection } from 'src/aritnt/administrator/collection/collection.service';
import { ReceivingService } from '../receiving/receiving.service';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { AppConsts } from 'src/core/constant/AppConsts';

@Component({
    selector: 'inventory',
    templateUrl: './inventory.component.html',
    styleUrls: ['./inventory.component.css']
})

export class InventoryComponent extends AppBaseComponent {
    @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
    dataSource: ArrayStore;

    private products: Product[] = [];
    private uoms: UoM[] = [];

    private collections: Collection[] = [];

    private numberPattern = AppConsts.numberPattern;

    constructor(
        injector: Injector,
        private recSvc: ReceivingService,
        private invService: InventoryService,
        private proSvc: ProductService,
        private uomSvc: UoMService) {
        super(injector);

        this.loadMDMSrouce();
    }

    private loadMDMSrouce() {
        this.proSvc.getsOnly().subscribe(result => {
            if (result.result == ResultCode.Success) {
                this.products = result.data.filter(p => p.isUsed == true);
            }
        });

        this.uomSvc.gets().subscribe(result => {
            if (result.result == ResultCode.Success) {
                this.uoms = result.data.filter(u => u.isUsed == true);
            }
        });

        this.recSvc.gets().subscribe(rs => {
            if (rs.result == ResultCode.Success) {
                this.collections = rs.data;
            }
        });
    }

    private loadInventorySource(collectionId: number) {
        this.invService.getBySKUs(collectionId).subscribe((result) => {
            if (result.result == ResultCode.Success) {
                this.dataSource = new ArrayStore({
                    key: ["productId", "uoMId"],
                    data: result.data
                });
                this.dataGrid.instance.refresh();
            }
        });
    }

    private collectionChanged(event: any) {
        if (event.value != null) {
            this.loadInventorySource(event.value.id);
        }
    }

    private isPopupVisible: boolean = false;
    private releaseInventory: Inventory = null;
    private isReleaseQuantityValid: boolean = true;
    private showRelease(selectedRows: any) {
        this.releaseInventory = FuncHelper.clone(selectedRows[0]);
        this.releaseInventory.releaseQuantity = 1;
        this.isReleaseQuantityValid = true;
        this.isPopupVisible = true;
    }

    private async release() {
        if(this.releaseInventory == null) {
            return;
        }
        if(this.releaseInventory.releaseQuantity == null) {
            this.isReleaseQuantityValid = false;
            this.showError(this.lang.instant('Validation.Required'));
            return;
        }
        if(this.releaseInventory.releaseQuantity < 1 || this.releaseInventory.releaseQuantity > this.releaseInventory.quantity) {
            this.isReleaseQuantityValid = false;
            return;
        }

        if(this.releaseInventory.releaseReason == null || this.releaseInventory.releaseReason == "") {
            return;
        }

        let collectionId = this.releaseInventory.collectionId;
        let rs = await this.invService.releaseCorruptedGoods(this.releaseInventory.collectionId, this.releaseInventory.traceCode, this.releaseInventory.releaseQuantity, this.releaseInventory.releaseReason).toPromise();
        if(rs.result == ResultCode.Success) {
            this.showSuccess("Common.UpdateSuccess");
            this.loadInventorySource(collectionId);
            this.isPopupVisible = false;
        }
        else {
            this.showError(rs.errorMessage);
        }

    }
}
