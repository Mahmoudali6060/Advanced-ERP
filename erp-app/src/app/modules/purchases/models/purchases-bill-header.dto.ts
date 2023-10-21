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
    date: string | undefined;
    vendorId: number | null;
    vendorName: string;
    notes: string;
    purchasesBillDetailList: Array<PurchasesBillDetailsDTO> = Array<PurchasesBillDetailsDTO>();
    constructor() {
        this.discount = 0;
        this.transfer = 0;
        this.totalDiscount = 0;

    }
}