import { UserTypeEnum } from "../../user-management/models/user-type-enum";

export class RegisterModel {
    email: string | undefined;
    username: string | undefined;
    password: string | undefined;
    confirmPassword: string;
    userTypeId: UserTypeEnum;
}