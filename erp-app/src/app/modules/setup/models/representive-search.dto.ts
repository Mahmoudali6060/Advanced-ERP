import { RepresentiveTypeEnum } from "src/app/shared/enums/representive-type.enum";
import { PagingDTO } from "src/app/shared/models/paging-dto";

export class RepresentiveSearchDTO extends PagingDTO {
    isActive: boolean;
    fullName: string;
    addressDetails: string;
    mobile: string;
    notes: string;
    representiveTypeId: RepresentiveTypeEnum | null;
}