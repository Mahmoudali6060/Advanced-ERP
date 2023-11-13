import { PagingDTO } from "src/app/shared/models/paging-dto"

export class ContactUssearch extends PagingDTO{
    name: string
    email: string
    mobile: string
    location: string
    notes: string
}