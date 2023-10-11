import { DataSourceModel } from "src/app/shared/models/data-source.model";

export class CategorySearchCriteriaDTO extends DataSourceModel {
    id: number;
    code: string;
    name: string;
    isActive: boolean;
}