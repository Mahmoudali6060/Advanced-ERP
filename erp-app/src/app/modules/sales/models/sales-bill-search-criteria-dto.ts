import { DataSourceModel } from "src/app/shared/models/data-source.model";

export class SalesBillSearchCriteriaDTO extends DataSourceModel {
    number: string;
    vendorId: string;
    date: string;
    isActive: boolean;
}