import { AccountTypeEnum } from "src/app/shared/enums/account-type.enum";
import { PaymentMethodEnum } from "src/app/shared/enums/payment-method.enum";
import { PagingDTO } from "src/app/shared/models/paging-dto";

export class AccountStatementSearchDTO extends PagingDTO {
    date: string;
    clientVendorId: number | null;
    transactionTypeId: AccountTypeEnum | null;
    paymentMethodId: PaymentMethodEnum | null;
    amount: number | null;
    refNo: string;
    bankAccountNo: string;
    checkNo: string;
    beneficiaryName:string;
}