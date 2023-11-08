
export class CategoryDTO {
    id: number;
    code: string;
    name: string;
    isActive: boolean;
    constructor() {
        this.isActive = true;
    }
}