import { PagingDTO } from "src/app/shared/models/paging-dto";

export class ProductSearchCriteriaDTO extends PagingDTO {
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