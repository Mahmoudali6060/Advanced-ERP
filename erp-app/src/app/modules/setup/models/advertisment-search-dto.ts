import { DataSourceModel } from "src/app/shared/models/data-source.model";

export class AdvertismentSearchDTO extends DataSourceModel {
    media:string;
    mediaBase64:string;
}