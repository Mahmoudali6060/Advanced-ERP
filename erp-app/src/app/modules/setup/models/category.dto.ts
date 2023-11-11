
export class CategoryDTO {
    id: number;
    code: string;
    name: string;
    isActive: boolean;
    companyId: number;
    constructor() {
        this.isActive = true;
    }
}