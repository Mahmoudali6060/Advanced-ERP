import { PurchasesBillDetailsDTO } from "./purchases-bill-details.dto";

export class PurchasesBillHeaderDTO {
    id: number;
    number: string;
    isActive: boolean;
    discount: number;
    transfer: number;
    total: number;
    totalDiscount: number;
    totalAfterDiscount: number;
    date: string | undefined;
    vendorId: number;
    vendorName: string;
    notes: string;
    purchasesBillDetailList: Array<PurchasesBillDetailsDTO> = Array<PurchasesBillDetailsDTO>();
}