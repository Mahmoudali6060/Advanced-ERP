import { DataSourceModel } from "src/app/shared/models/data-source.model";

export class ProductSearchCriteriaDTO extends DataSourceModel {
    code: string;
    isActive: boolean;
    name: string;
    barCode: string;
    imageUrl: string;
    averageCost: number;
    lastPurchasedPrice: number;
    actualQuantity: number;
    lowQuantity: number;
    highQuantity: number;
    categoryId: number;
}