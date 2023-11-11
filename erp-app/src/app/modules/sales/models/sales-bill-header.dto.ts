import { BaseDTO } from "src/app/shared/models/base-dto.model";
import { SalesBillDetailsDTO } from "./sales-bill-details.dto";

export class SalesBillHeaderDTO extends BaseDTO {
    id: number;
    number: string;
    isActive: boolean;
    discount: number | 0 = 0;
    transfer: number | 0 = 0;
    total: number;
    totalDiscount: number | 0 = 0;
    totalAfterDiscount: number = 0;
    paid: number = 0;
    remaining: number | 0;
    date: string | undefined;
    clientVendorId: number | null;
    clientVendorName: string;
    notes: string;
    companyId: number;
    salesBillDetailList: Array<SalesBillDetailsDTO> = Array<SalesBillDetailsDTO>();
}