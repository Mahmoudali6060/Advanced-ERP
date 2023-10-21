import { SalesBillDetailsDTO } from "./sales-bill-details.dto";

export class SalesBillHeaderDTO {
    id: number;
    number: string;
    isActive: boolean;
    discount: number | 0;
    transfer: number | 0;
    total: number;
    totalDiscount: number | 0;
    totalAfterDiscount: number;
    date: string | undefined;
    clientId: number | null;
    clientName: string;
    notes: string;
    salesBillDetailList: Array<SalesBillDetailsDTO> = Array<SalesBillDetailsDTO>();
    constructor() {
        this.discount = 0;
        this.transfer = 0;
        this.totalDiscount = 0;

    }
}