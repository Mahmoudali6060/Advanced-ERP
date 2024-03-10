import { AccountTypeEnum } from "src/app/shared/enums/account-type.enum";
import { BillTypeEnum } from "src/app/shared/enums/bill-type.enum";
import { PaymentMethodEnum } from "src/app/shared/enums/payment-method.enum";
import { TransactionTypeEnum } from "src/app/shared/enums/transaction-type.enum";
import { BaseDTO } from "src/app/shared/models/base-dto.model";

export class AccountStatementDTO extends BaseDTO {

    number: number;
    date: string | undefined;
    clientVendorId: number | null;
    beneficiaryName: string | undefined;
    //transactionTypeId: TransactionTypeEnum;
    paymentMethodId: PaymentMethodEnum;
    debit: number = 0;
    credit: number = 0;
    refNo: string;
    notes: string;
    isBilled: boolean;
    billId: number;
    billType: BillTypeEnum;
}