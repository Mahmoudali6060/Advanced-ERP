import { CompanyDTO } from "./company-dto";
import { RoleDTO } from "./role.dto";
import { UserTypeEnum } from "./user-type-enum";

export class UserProfileDTO {
    id: number;
    isActive: boolean;
    firstName: string;
    lastName: string;
    mobile: string;
    email: string;
    userName: string;
    password: string;
    roleId: number;
    roleName: string;
    defaultLanguage: string = '';
    token: string;
    imageBase64: string;
    imageUrl: string;
    isFirstLogin: boolean;
    companyId: number;
    companyDTO: CompanyDTO;
    isHide: boolean = false;
    roleGroupDTO: RoleDTO;

}