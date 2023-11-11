import { BaseDTO } from "./base-dto.model";

export class AuditDTO  extends BaseDTO{
    createdByProfileId: number;
    modifiedByProfileId: number;
    createdByProfileName:string;
    modifiedByProfileName:string;
}
