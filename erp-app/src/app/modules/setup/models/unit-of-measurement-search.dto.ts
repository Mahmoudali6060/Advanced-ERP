import { PagingDTO } from "src/app/shared/models/paging-dto";

export class UnitOfMeasurementSearchDTO extends PagingDTO {
    id: number;
    isActive: boolean = true;
    name: string;
    description: string;
}