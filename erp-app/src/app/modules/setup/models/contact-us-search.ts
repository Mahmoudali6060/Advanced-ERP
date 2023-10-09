import { DataSourceModel } from "src/app/shared/models/data-source.model"

export class ContactUssearch extends DataSourceModel{
    name: string
    email: string
    mobile: string
    location: string
    notes: string
}