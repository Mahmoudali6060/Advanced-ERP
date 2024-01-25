import { AccountTypeEnum } from "src/app/shared/enums/account-type.enum";
import { PaymentMethodEnum } from "src/app/shared/enums/payment-method.enum";
import { TransactionTypeEnum } from "src/app/shared/enums/transaction-type.enum";
import { BaseDTO } from "src/app/shared/models/base-dto.model";

export class TreasuryDTO extends BaseDTO {

    number: number;
    date: string ;
    accountTypeId: AccountTypeEnum;
    clientVendorId: number | null;
    beneficiaryName: string | undefined;
    //transactionTypeId: TransactionTypeEnum;
    paymentMethodId: PaymentMethodEnum;
    outComing: number = 0;
    inComing: number = 0;
    refNo: string;
    notes: string;
    isBilled: boolean;
}