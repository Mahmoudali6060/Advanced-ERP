
export class ProductDTO {
    id: number;
    code: string;
    isActive: boolean;
    name: string;
    barCode: string;
    imageUrl: string;
    price: number;
    sellingPricePercentage: number//سعر البيع
    purchasingPricePercentage: number;//سعر الشراء
    actualQuantity: number;
    lowQuantity: number;
    highQuantity: number;
    categoryId: number;
    categoryName: string;
    imageBase64: string;
}