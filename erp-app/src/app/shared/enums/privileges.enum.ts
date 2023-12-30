export module Privileges {

    export module Dashboard {//1

        export enum DashboardPage { // id = 2
            View = 3,
            Add = 4,
            Edit = 5,
            Delete = 6
        }

    }

    export module Setup {//7

        export enum Categories { // id = 8
            View = 9,
            Add = 10,
            Edit = 11,
            Delete = 12
        }

        export enum Products { // id = 13
            View = 14,
            Add = 15,
            Edit = 16,
            Delete = 17
        }

        export enum UnitOfMeasurements { // id = 18
            View = 19,
            Add = 20,
            Edit = 21,
            Delete = 22
        }

        export enum Clients { // id = 23
            View = 24,
            Add = 25,
            Edit = 26,
            Delete = 27
        }

        export enum Vendors { // id = 28
            View = 30,
            Add = 31,
            Edit = 32,
            Delete = 33
        }

        export enum Representives { // id = 34
            View = 35,
            Add = 36,
            Edit = 37,
            Delete = 38
        }

    }

    export module Sales {//39

        export enum SalesBills { // id = 40
            View = 41,
            Add = 42,
            Edit = 43,
            Delete = 44
        }

    }

    export module Purchases {//45

        export enum PurchasesBills { // id = 46
            View = 47,
            Add = 48,
            Edit = 49,
            Delete = 50
        }

    }

    export module UserManagement {//51

        export enum Users { // id = 52
            View = 53,
            Add = 54,
            Edit = 55,
            Delete = 56
        }

        export enum Roles { // id = 57
            View = 58,
            Add = 59,
            Edit = 60,
            Delete = 61
        }


    }

    export module Reports {//62

        export enum AccountStatementAllClients { // id = 63
            View = 64,
        }

        export enum AccountStatementSingleClient { // id = 65
            View = 66,
        }

        export enum AccountStatementAllVendors { // id = 67
            View = 68,
        }

        export enum AccountStatementSingleVendor { // id = 69
            View = 70,
        }

        export enum ProductLowQuantity { // id = 71
            View = 72,
        }

        export enum ProductMinusQuantity { // id = 73
            View = 74,
        }

    }
}