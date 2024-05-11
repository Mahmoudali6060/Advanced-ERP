import { PagingDTO } from "src/app/shared/models/paging-dto";
import { ClientVendorTypeEnum } from "./client-vendor.dto";

export class ClientVendorSearchCriteriaDTO extends PagingDTO {
    id: number;
    dateFrom: string | undefined;
    dateTo: string | undefined;
    code: string;
    isActive: boolean;
    fullName: string;
    address: string;
    phone: string;
    idNumber: string;
    typeId: ClientVendorTypeEnum;

}