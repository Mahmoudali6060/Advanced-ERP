import { FilterModel } from "./filter.model";

export class PagingDTO {
    public page: number = 1;
    public pageSize: number = 20;
    companyId: number;
    public filter: Array<FilterModel> = new Array<FilterModel>();
}