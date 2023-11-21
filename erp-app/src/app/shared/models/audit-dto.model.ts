import { BaseDTO } from "./base-dto.model";

export class AuditDTO  extends BaseDTO{
    createdByProfileId: number;
    modifiedByProfileId: number;
    createdByUsername:string;
    modifiedByUsername:string;
}
