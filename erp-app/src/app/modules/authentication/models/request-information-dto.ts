import { CustomerTypeEnum } from "src/app/shared/enums/customer-type.enum";
import { RequestReviewResultEnum } from "src/app/shared/enums/request-review-result.enum";
import { FileDTO } from "src/app/shared/models/file.model";
import { CompanyDTO } from "../../user/models/company-dto";

export class RequestInformationDTO {
    id: number;
    productDescription: string;
    packagingType: string;
    grossWeight: string;
    netWeight: string;
    drainWeight: string;
    noOfContainers: number;
    cutomerType: CustomerTypeEnum;
    reviewResult: RequestReviewResultEnum;
    comments: string;
    importerDataDTO: CompanyDTO = new CompanyDTO();
    exporterDataDTO: CompanyDTO = new CompanyDTO();
    draftBL: FileDTO = new FileDTO();//PDF- JPG- NPG
    originalBL: FileDTO = new FileDTO();//PDF- JPG- NPG
}