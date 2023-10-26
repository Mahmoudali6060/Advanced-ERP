import { PrivilegeDTO } from "./privilege-dto";

export class RoleDTO {
    isActive: boolean;
    id: number;
    name: string;
    description: string;
    privileges: Array<PrivilegeDTO>
}