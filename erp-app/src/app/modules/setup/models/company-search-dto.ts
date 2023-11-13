import { PagingDTO } from "src/app/shared/models/paging-dto";

export class CompanySearchDTO extends PagingDTO {
    id: number;
    isActive:boolean;
    name: string;
    contactPerson: string;
    contactTelephone: string;
    websiteLink: string;
    addressDetails: string;
}