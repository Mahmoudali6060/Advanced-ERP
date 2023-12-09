import { SalesBillDetailsDTO } from "./sales-bill-details.dto";
import { AuditDTO } from "src/app/shared/models/audit-dto.model";

export class SalesBillHeaderDTO extends AuditDTO {
    id: number;
    number: string;
    isActive: boolean;
    date: string | undefined;
    clientVendorId: number | null;
    clientVendorName: string;
    notes: string;
    companyId: number;
    createdByProfileName: string;
    modifiedByProfileName: string;
    representiveId: number;
    discount: number = 0 | 0;
    otherExpenses: number = 0 | 0;
    total: number = 0 | 0;
    totalAfterDiscount: number = 0 | 0;
    vatAmount: number = 0 | 0;
    totalAfterVAT: number = 0 | 0;
    taxPercentage: number = 0 | 0;
    taxAmount: number = 0 | 0;
    totalAmount: number = 0 | 0;
    paid: number = 0 | 0;
    remaining: number = 0 | 0;
    profit: number = 0 | 0;
    isTax: boolean = false;
    salesBillDetailList: Array<SalesBillDetailsDTO> = Array<SalesBillDetailsDTO>();


}