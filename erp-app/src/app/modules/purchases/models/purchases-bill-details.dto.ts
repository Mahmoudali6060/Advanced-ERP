
export class PurchasesBillDetailsDTO {
    id: number;
    productId: number | null;
    actualQuantity: number;
    quantity: number;
    price: number;
    lastSellingPrice: number;
    lastPurchasingPrice: number;
    discount: number;
    priceAfterDiscount: number;
    subTotal: number;
    notes: string;
    index: number;
    companyId: number;
    productName: string;
    productCode: string;

}