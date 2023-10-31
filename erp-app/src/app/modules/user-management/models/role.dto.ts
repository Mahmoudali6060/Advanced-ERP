import { RolePrivilegeDTO } from "./privilege-dto";

export class RoleDTO {
    isActive: boolean;
    id: number;
    name: string;
    description: string;
    rolePrivileges: Array<RolePrivilegeDTO>
}