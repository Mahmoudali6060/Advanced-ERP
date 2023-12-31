import { PagingDTO } from "src/app/shared/models/paging-dto";

export class PurchasesBillSearchCriteriaDTO extends PagingDTO {
    number: string;
    clientVendorId: string;
    date: string;
    isActive: boolean;
    isTemp: boolean = false;
    isReturned: boolean = false;
    personPhoneNumber: string;
}