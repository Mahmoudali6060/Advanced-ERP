import { AccountTypeEnum } from "src/app/shared/enums/account-type.enum";
import { PaymentMethodEnum } from "src/app/shared/enums/payment-method.enum";
import { PagingDTO } from "src/app/shared/models/paging-dto";

export class TreasurySearchDTO extends PagingDTO {
    date: string;
    accountTypeId: AccountTypeEnum;
    clientVendorId: string;
    transactionTypeId: AccountTypeEnum;
    paymentMethodId: PaymentMethodEnum;
    amount: number;
    refNo: string;
    bankAccountNo: string;
    checkNo: string;
}