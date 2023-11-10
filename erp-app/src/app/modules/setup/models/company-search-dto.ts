import { DataSourceModel } from "src/app/shared/models/data-source.model";

export class CompanySearchDTO extends DataSourceModel {
    id: number;
    isActive:boolean;
    name: string;
    contactPerson: string;
    contactTelephone: string;
    websiteLink: string;
    addressDetails: string;
}