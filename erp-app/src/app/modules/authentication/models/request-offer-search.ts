import { ContainerPortEnum } from "src/app/shared/enums/container-port.enum";

export class RequestOfferSearch{
    shippingOfferId : number;
    companyId : number;
    noOfContainers: number;
    containerPortId: ContainerPortEnum;
    requestId : number;

}