import { UserTypeEnum } from "../../user/models/user-type-enum";

export class RegisterModel {
    email: string | undefined;
    username: string | undefined;
    password: string | undefined;
    confirmPassword: string;
    userTypeId: UserTypeEnum;
}