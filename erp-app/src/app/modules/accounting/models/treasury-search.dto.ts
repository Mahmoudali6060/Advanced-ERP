import { PagingDTO } from "src/app/shared/models/paging-dto";

export class TreasurySearchDTO extends PagingDTO {
    date: string;
    accountTypeId: string;
    accountId: string;
    transactionTypeId: number;
    paymentMethodId: number;
    amount: number;
    refNo: string;
    bankAccountNo: string;
    checkNo: string;
}