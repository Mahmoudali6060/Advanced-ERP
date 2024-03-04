import { PagingDTO } from "src/app/shared/models/paging-dto";

export class PurchasesBillSearchCriteriaDTO extends PagingDTO {
    number: string;
    clientVendorId: string;
    representiveId: number;
    dateFrom: string | undefined;
    dateTo: string | undefined;
    isActive: boolean;
    isTemp: boolean = false;
    isReturned: boolean = false;
    personPhoneNumber: string;
}