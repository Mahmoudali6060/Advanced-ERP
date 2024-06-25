import { AccountTypeEnum } from "src/app/shared/enums/account-type.enum";
import { PaymentMethodEnum } from "src/app/shared/enums/payment-method.enum";
import { PagingDTO } from "src/app/shared/models/paging-dto";

export class AccountStatementSearchDTO extends PagingDTO {
    number: string;
    date: string;
    dateFrom: string | undefined;
    dateTo: string | undefined;
    clientVendorId: number | null;
    phone: string| null;
    beneficiaryName: string;
    paymentMethodId: PaymentMethodEnum | null;
    refNo: string;
    bankAccountNo: string;
    checkNo: string;
    representiveId: number | null;
}