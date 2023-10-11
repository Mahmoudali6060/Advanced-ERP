import { DataSourceModel } from "src/app/shared/models/data-source.model";

export class CompanySearchCriteria extends DataSourceModel {
    companyNameEn: string;
    companyNameAr: string;
    companyCategoryId:number;
    countryId: number;
    stateId: number;
    cityId: number;
    contactPerson : string;
    contactTelephone : string;
}