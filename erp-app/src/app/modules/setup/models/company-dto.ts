import { CompanyCategoryEnum } from "src/app/shared/enums/company-category.enum";
import { CompanyTypeEnum } from "src/app/shared/enums/company-type.enum";

export class CompanyDTO {
    id: number;
    companyNameEn: string;
    companyNameAr: string;
    email: string;
    userName: string;
    password: string;
    role: string;
    defaultLanguage: string = '';
    companyLogoBase64: string;
    companyLogoUrl: string;
    userTypeId: number;
    countryId: number;
    countryName: string;
    stateId: number;
    stateName: string;
    cityId: number;
    cityName: string;
    addressDetails: string;
    websiteLink: string;
    contactPerson: string;
    contactTelephone: string;
    companyCategoryId: CompanyCategoryEnum;
    companyTypeId: CompanyTypeEnum;
    zipCode: string;
}