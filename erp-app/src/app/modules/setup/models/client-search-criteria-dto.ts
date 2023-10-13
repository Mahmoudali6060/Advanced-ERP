import { DataSourceModel } from "src/app/shared/models/data-source.model";

export class ClientSearchCriteriaDTO extends DataSourceModel{
    id: number;
    code: string;
    isActive: boolean;
    fullName: string;
    address: string;
    phone: string;
    idNumber:string;
}