import { PagingDTO } from "src/app/shared/models/paging-dto";

export class UserProfileSearchCriteriaDTO extends PagingDTO {
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