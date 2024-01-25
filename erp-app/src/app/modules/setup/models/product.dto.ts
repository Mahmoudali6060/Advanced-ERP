
export class ProductDTO {
    id: number;
    code: string;
    isActive: boolean;
    name: string;
    barCode: string;
    imageUrl: string;
    purchasingPrice: number;
    sellingPrice: number;
    sellingPricePercentage: number//سعر البيع
    lastSellingPrice: number;
    purchasingPricePercentage: number;//سعر الشراء
    lastPurchasingPrice: number;
    actualQuantity: number;
    lowQuantity: number;
    highQuantity: number;
    categoryId: number;
    categoryName: string;
    imageBase64: string;
    companyId: number;
    description: string;
    unitOfMeasurementId: number;
    unitOfMeasurementName: string;
    isChanged: boolean = false;
    constructor() {
        this.isActive = true;
    }
}