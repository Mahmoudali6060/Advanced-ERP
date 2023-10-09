import { DataSourceModel } from "src/app/shared/models/data-source.model";

export class UserProfileSearchCriteriaDTO extends DataSourceModel {
    firstName: string ;
    lastName: string ;
    email: string ;
    mobile: string ;
    userName: string ;
    userTypeId:number;

    countryId: number;
    stateId: number;
    cityId: number;
    contactPerson : string;
    contactTelephone : string;

    isActive:boolean;
}