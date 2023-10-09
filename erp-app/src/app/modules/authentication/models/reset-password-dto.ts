export interface ResetPasswordDTO {
   // oldPassword:string
    password: string;
    confirmPassword: string;
    email: string;
    token: string;
}