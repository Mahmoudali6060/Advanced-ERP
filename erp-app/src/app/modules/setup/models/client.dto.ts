
export class ClientDTO {
    id: number;
    code: string;
    isActive: boolean;
    fullName: string;
    address: string;
    imageUrl: string;
    phoneNumber1: string;
    phoneNumber2: string;
    balance: number//سعر البيع
    notes: string;
    idNumber: string;
    imageBase64: string;
    vendorId: number;
    isVendor: boolean = false;
}