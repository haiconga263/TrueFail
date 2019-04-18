import { ProductionInformation } from "./production.model";

export class Process {
    public id: number;
    public code: string;
    public partnerId: number;
    public pointId: number;
    public productionId: number;
    public productId: number;
    public farmerId: number;
    public companyCultivationId: number;
    public companyCollectionId: number;
    public companyFulfillmentId: number;
    public companyDistributionId: number;
    public companyRetailerId: number;
    public collectionDate: string;
    public fulfillmentDate: string;
    public distributionDate: string;
    public retailerDate: string;
    public expiryDate: string;
    public manufacturingDate: string;
    public growingMethodId: number;
    public standardExpiryDate: string;
    public description: string;
    public quantity: string;
    public uom: string;
    public isNew: boolean;
    public isUsed: boolean;
    public createdBy: number;
    public createdDate: Date;
    public modifiedBy: number;
    public modifiedDate: Date;


    public constructor(init?: Partial<Process>) {
        Object.assign(this, init);
    }
}

export class ProcessInformation extends Process {
    production: ProductionInformation

    public constructor(init?: Partial<ProcessInformation>) {
        super(init);
        Object.assign(this, init);
    }
}