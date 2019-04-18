import { Component, OnInit, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { BaseImplementorComponent } from 'src/core/build-implementor/base-implementor.component';
import { appUrl } from 'src/aritnt/production/app-url';
import { HttpConfig, MethodTypes } from 'src/core/build-implementor/implementor.service';
import { Cultivation, CultivationService } from 'src/aritnt/common/services/cultivation.service';
import { Seed, SeedService } from 'src/aritnt/common/services/seed.service';
import { Plot, PlotService } from 'src/aritnt/common/services/plot.service';

@Component({
  selector: 'cultivation',
  templateUrl: '../../../../core/build-implementor/base-implementor.component.html',
  styleUrls: ['../../../../core/build-implementor/base-implementor.component.css']
})
export class CultivationComponent extends BaseImplementorComponent {
  constructor(
    injector: Injector,
    private cultivationService: CultivationService,
    private seedService: SeedService,
    private plotService: PlotService,
  ) {
    super(injector);
    this.getAllHttp = new HttpConfig({ name: 'gets', method: MethodTypes.get });
    this.deleteHttp = new HttpConfig({ name: 'delete', method: MethodTypes.post, nameField: 'id' });
    this.setUrlApiRoot(appUrl.apiCultivation);
    this.setUrlDetail(appUrl.cultivationDetail);

  }

  seeds: Seed[];
  plots: Plot[];

  async init() {
    this.seeds = (await this.seedService.gets().toPromise()).data;
    this.plots = (await this.plotService.gets().toPromise()).data;

    this.configColumns(
      { dataField: 'id', dataType: 'string', alignment: 'center', width: 100, },
      { dataField: 'code', dataType: 'string' },
      {
        dataField: 'seedId',
        caption: 'Seed',
        lookup: {
          dataSource: this.seeds,
          displayExpr: 'code',
          valueExpr: 'id',
        },
      },
      {
        dataField: 'plotId',
        caption: 'Plot',
        lookup: {
          dataSource: this.plots,
          displayExpr: 'code',
          valueExpr: 'id',
        },
      },
      { dataField: 'description', caption: 'Description', dataType: 'string' },
      { dataField: 'isUsed', dataType: 'boolean' },
    );

    this.initialize();
  }

  ngOnInit() {
    super.ngOnInit();
    this.init();
  }
}
