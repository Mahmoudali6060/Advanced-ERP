import { RepresentiveTypeEnum } from "src/app/shared/enums/representive-type.enum";
import { BaseDTO } from "src/app/shared/models/base-dto.model";

export class RepresentiveDTO extends BaseDTO {
    fullName: string;
    addressDetails: string;
    mobile: string;
    notes: string;
    representiveTypeId: RepresentiveTypeEnum;
}