import { SalesBillDetailsDTO } from "./sales-bill-details.dto";
import { AuditDTO } from "src/app/shared/models/audit-dto.model";

export class SalesBillHeaderDTO extends AuditDTO {
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
    createdByProfileName: string;
    modifiedByProfileName: string;
    representiveId:number;
    salesBillDetailList: Array<SalesBillDetailsDTO> = Array<SalesBillDetailsDTO>();
}