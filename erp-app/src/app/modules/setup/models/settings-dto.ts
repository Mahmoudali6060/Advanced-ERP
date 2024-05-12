
export class SettingDTO {
    id: number;
    companyId: number;
    salesBillInstructions: string = '';
    purchasesBillInstructions: string = '';
    changeProductPriceFromSales: boolean;
    changeProductPriceFromPurchases: boolean;

}