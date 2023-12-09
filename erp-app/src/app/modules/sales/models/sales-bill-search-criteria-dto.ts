import { PagingDTO } from "src/app/shared/models/paging-dto";

export class SalesBillSearchCriteriaDTO extends PagingDTO {
    number: string;
    vendorId: string;
    date: string;
    isActive: boolean;
    isTemp: boolean = false;
}