import { DataSourceModel } from "src/app/shared/models/data-source.model";

export class TrucksProviderSearchDTO extends DataSourceModel{
    providerNameEn : string
    providerNameAr : string
    countryId : number
    cityId : number
    stateId : number
    addressDetails : string
    contactPerson : string
    contactTelephone : string
}