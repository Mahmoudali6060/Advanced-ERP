import { PagingDTO } from "src/app/shared/models/paging-dto";
import { ClientVendorTypeEnum } from "./client-vendor.dto";
import { AccountStatusEnum } from "src/app/shared/enums/account-status.enum";

export class ClientVendorSearchCriteriaDTO extends PagingDTO {
    id: number;
    dateFrom: string | undefined;
    dateTo: string | undefined;
    code: string;
    isActive: boolean;
    accountStatusId: AccountStatusEnum;
    fullName: string;
    address: string;
    phone: string;
    idNumber: string;
    typeId: ClientVendorTypeEnum;

}