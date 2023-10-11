import { CompanyDTO } from "../../user/models/company-dto";
import { RegisterModel } from "./register.model";

export class RegisterRequestModel {
    public registerDTO: RegisterModel = new RegisterModel();
    public companyDTO: CompanyDTO = new CompanyDTO();
}