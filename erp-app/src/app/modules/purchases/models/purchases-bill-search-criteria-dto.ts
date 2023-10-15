import { DataSourceModel } from "src/app/shared/models/data-source.model";

export class PurchasesBillSearchCriteriaDTO extends DataSourceModel {
    number: string;
    vendorId: string;
    date: string;
    isActive: boolean;
}