import { FilterModel } from "./filter.model";

export class DataSourceModel {
    public page: number = 1;
    public pageSize: number = 6;
    companyId: number;
    public filter: Array<FilterModel> = new Array<FilterModel>();
}