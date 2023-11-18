
export class ClientVendorBalanceDTO {
    date: string;
    debit: number | 0 = 0;
    credit: number | 0 = 0;
    details: string;
    number: string;
    refId: number;
    clientVendorId: number;
}