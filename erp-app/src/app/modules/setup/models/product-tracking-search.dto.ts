import { ProductProcessTypeEnum } from "src/app/shared/enums/product-process-type.enum";
import { PagingDTO } from "src/app/shared/models/paging-dto";

export class ProductTrackingSearchDTO extends PagingDTO {
    productId: number;
    date: string;
    productProcessTypeId: ProductProcessTypeEnum;
}