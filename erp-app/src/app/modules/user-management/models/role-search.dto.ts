import { DataSourceModel } from "src/app/shared/models/data-source.model";

export class RoleSearchDTO extends DataSourceModel {
    id: number;
    isActive: boolean;
    name: string;
    description: string;
}