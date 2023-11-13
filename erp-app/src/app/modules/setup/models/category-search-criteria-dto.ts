import { PagingDTO } from "src/app/shared/models/paging-dto";

export class CategorySearchCriteriaDTO extends PagingDTO {
    id: number;
    code: string;
    name: string;
    isActive: boolean;
}