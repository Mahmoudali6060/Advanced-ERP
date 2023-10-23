
export class SalesBillDetailsDTO {
    id: number;
    productId: number | null;
    actualQuantity: number;
    quantity: number;
    price: number;
    discount: number;
    priceAfterDiscount: number;
    subTotal: number;
    notes: string;
    index: number;

}