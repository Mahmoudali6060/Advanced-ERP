import { PurchasesBillDetailsDTO } from "./purchases-bill-details.dto";

export class PurchasesBillHeaderDTO {
    id: number;
    number: string;
    isActive: boolean;
    discount: number | 0;
    transfer: number | 0;
    total: number;
    totalDiscount: number | 0;
    totalAfterDiscount: number;
    paid: number;
    remaining: number|0;
    date: string | undefined;
    clientVendorId: number | null;
    clientVendorName: string;
    notes: string;
    purchasesBillDetailList: Array<PurchasesBillDetailsDTO> = Array<PurchasesBillDetailsDTO>();
    constructor() {
        this.discount = 0;
        this.transfer = 0;
        this.totalDiscount = 0;

    }
}