
export class ClientVendorDTO {
    id: number;
    code: string;
    isActive: boolean;
    fullName: string;
    address: string;
    imageUrl: string;
    phoneNumber1: string;
    phoneNumber2: string;
    debit: number//سعر 
    credit: number//سعر 
    notes: string;
    idNumber: string;
    imageBase64: string;
    typeId: ClientVendorTypeEnum;
    companyId: number;
    constructor() {
        this.isActive = true;
    }

}


export enum ClientVendorTypeEnum {
    Client = 1,
    Vendor = 2,
    All = 3,
}