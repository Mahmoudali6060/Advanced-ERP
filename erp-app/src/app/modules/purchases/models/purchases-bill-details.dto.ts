
export class PurchasesBillDetailsDTO {
    id: number;
    productId: number;
    quantity: number;
    price: number;
    discount: number;
    priceAfterDiscount: number;
    subTotal: number;
    notes: string;
    index: number;

}