import { PagingDTO } from "src/app/shared/models/paging-dto";

export class TrucksProviderSearchDTO extends PagingDTO{
    providerNameEn : string
    providerNameAr : string
    countryId : number
    cityId : number
    stateId : number
    addressDetails : string
    contactPerson : string
    contactTelephone : string
}