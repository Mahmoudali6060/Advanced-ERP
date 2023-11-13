import { PagingDTO } from "src/app/shared/models/paging-dto";

export class RoleSearchDTO extends PagingDTO {
    id: number;
    isActive: boolean;
    name: string;
    description: string;
}