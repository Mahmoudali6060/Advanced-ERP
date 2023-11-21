import { ProductProcessTypeEnum } from "src/app/shared/enums/product-process-type.enum";

export class ProductTrackingDTO {
    productId: number;
    date: string;
    oldData: string;
    newData: string;
    createdByUsername: string;
    productProcessTypeId: ProductProcessTypeEnum;
}