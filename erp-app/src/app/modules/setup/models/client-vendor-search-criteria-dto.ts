import { DataSourceModel } from "src/app/shared/models/data-source.model";
import { ClientVendorTypeEnum } from "./client-vendor.dto";

export class ClientVendorSearchCriteriaDTO extends DataSourceModel{
    id: number;
    code: string;
    isActive: boolean;
    fullName: string;
    address: string;
    phone: string;
    idNumber:string;
    typeId: ClientVendorTypeEnum;

}