import { BaseDTO } from "src/app/shared/models/base-dto.model";

export class SalesBillDetailsDTO extends BaseDTO {
    productId: number | null;
    actualQuantity: number;
    quantity: number;
    price: number;
    discount: number;
    priceAfterDiscount: number;
    subTotal: number;
    notes: string;
    index: number;
    sellingPrice: number;
    productName: string;
    productCode: string;
    companyId: number;

}