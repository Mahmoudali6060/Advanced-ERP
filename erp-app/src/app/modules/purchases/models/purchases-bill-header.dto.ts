import { AuditDTO } from "src/app/shared/models/audit-dto.model";
import { PurchasesBillDetailsDTO } from "./purchases-bill-details.dto";
import { PaymentMethodEnum } from "src/app/shared/enums/payment-method.enum";

export class PurchasesBillHeaderDTO extends AuditDTO {
    id: number | null;
    number: string;
    isActive: boolean;
    date: string | undefined;
    clientVendorId: number | null;
    clientVendorName: string;
    notes: string;
    companyId: number;
    representiveId: number | null;
    discount: number = 0 | 0;
    otherExpenses: number = 0 | 0;
    total: number = 0 | 0;
    totalAfterDiscount: number = 0 | 0;
    vatAmount: number = 0 | 0;
    totalAfterVAT: number = 0 | 0;
    taxPercentage: number = 0 | 0;
    taxAmount: number = 0 | 0;
    totalAmount: number = 0 | 0;
    paid: number;
    remaining: number = 0 | 0;
    isTax: boolean = false;
    purchasesBillDetailList: Array<PurchasesBillDetailsDTO> = Array<PurchasesBillDetailsDTO>();
    removedPurchasesBillDetailList: Array<PurchasesBillDetailsDTO> = Array<PurchasesBillDetailsDTO>();
    isTemp: boolean = false;
    isReturned: boolean = false;
    isNewReturned: boolean = false;
    paymentMethodId: PaymentMethodEnum;
    refNo: string;
}