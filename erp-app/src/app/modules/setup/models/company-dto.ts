import { SettingDTO } from "./settings-dto";

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
    settingDTO: SettingDTO=new SettingDTO();
}