import { CompanyDTO } from "./company-dto";
import { UserTypeEnum } from "./user-type-enum";

export class UserProfileDTO {
    id: number;
    isActive:boolean;
    firstName: string;
    lastName: string;
    mobile: string;
    email: string;
    userName: string;
    password: string;
    role: string;
    defaultLanguage: string = '';
    token: string;
    imageBase64: string;
    imageUrl: string;
    userTypeId: UserTypeEnum;
    userType: string;
    isFirstLogin : boolean;
    companyId : number;
    companyDTO : CompanyDTO;
    isHide : boolean = false;

}