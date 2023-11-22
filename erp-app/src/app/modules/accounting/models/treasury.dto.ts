import { AccountTypeEnum } from "src/app/shared/enums/account-type.enum";
import { PaymentMethodEnum } from "src/app/shared/enums/payment-method.enum";
import { TransactionTypeEnum } from "src/app/shared/enums/transaction-type.enum";
import { BaseDTO } from "src/app/shared/models/base-dto.model";

export class TreasuryDTO extends BaseDTO {

    date: string;
    accountTypeId: AccountTypeEnum;
    accountId: string;
    beneficiaryName: string;
    transactionTypeId: TransactionTypeEnum;
    paymentMethodId: PaymentMethodEnum;
    amount: number;
    refNo: string;
    bankAccountNo: string;
    checkNo: string;
    notes: string;
}