import { PagingDTO } from "src/app/shared/models/paging-dto";

export class AdvertismentSearchDTO extends PagingDTO {
    media:string;
    mediaBase64:string;
}