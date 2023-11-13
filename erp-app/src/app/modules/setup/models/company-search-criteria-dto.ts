import { PagingDTO } from "src/app/shared/models/paging-dto";

export class CompanySearchCriteria extends PagingDTO {
    companyNameEn: string;
    companyNameAr: string;
    companyCategoryId:number;
    countryId: number;
    stateId: number;
    cityId: number;
    contactPerson : string;
    contactTelephone : string;
}