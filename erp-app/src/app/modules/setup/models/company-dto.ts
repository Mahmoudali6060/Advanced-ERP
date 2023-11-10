import { CompanyCategoryEnum } from "src/app/shared/enums/company-category.enum";
import { CompanyTypeEnum } from "src/app/shared/enums/company-type.enum";

export class CompanyDTO {
    id: number;
    isActive: boolean = true;
    imageUrl: string;
    imageBase64: string;
    name: string;
    addressDetails: string;
    contactPerson: string;
    contactTelephone: string;
    websiteLink: string;
}