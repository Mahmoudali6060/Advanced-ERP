import { Privileges } from "src/app/shared/enums/privileges.enum";
import { SideMenuLevelEnum, SideMenuMainEnum, SideMenuPageEnum } from "./side-menu-main.enum";

export const PrivilegeDATA = [
  {
    id: SideMenuMainEnum.Dashboard,
    name: 'Dashboard',
    checked: false,
    level: SideMenuLevelEnum.MainCategory,
    children: [
      {
        id: SideMenuPageEnum.DashboardPage,
        name: 'DashboardPage',
        checked: false,
        categoryId: SideMenuMainEnum.Dashboard,
        level: SideMenuLevelEnum.Page,
        children: [
          {
            id: Privileges.Dashboard.DashboardPage.View,
            name: 'View',
            checked: false,
            categoryId: SideMenuMainEnum.Dashboard,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Dashboard.DashboardPage.Add,
            name: 'Add',
            checked: false,
            categoryId: SideMenuMainEnum.Dashboard,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Dashboard.DashboardPage.Edit,
            name: 'Edit',
            checked: false,
            categoryId: SideMenuMainEnum.Dashboard,
            level: SideMenuLevelEnum.Action,
            children: []

          },
          {
            id: Privileges.Dashboard.DashboardPage.Delete,
            name: 'Delete',
            checked: false,
            categoryId: SideMenuMainEnum.Dashboard,
            level: SideMenuLevelEnum.Action,
            children: []
          }
        ]
      }

    ]
  },
  {
    id: SideMenuMainEnum.Setup,
    name: 'Setup',
    checked: false,
    level: SideMenuLevelEnum.MainCategory,
    children: [
      {
        id: SideMenuPageEnum.Categories,
        name: 'Categories',
        checked: false,
        categoryId: SideMenuMainEnum.Setup,
        level: SideMenuLevelEnum.Page,
        children: [
          {
            id: Privileges.Setup.Categories.View,
            name: 'View',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Setup.Categories.Add,
            name: 'Add',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Setup.Categories.Edit,
            name: 'Edit',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []

          },
          {
            id: Privileges.Setup.Categories.Delete,
            name: 'Delete',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []
          }
        ]
      },
      {
        id: SideMenuPageEnum.Products,
        name: 'Products',
        checked: false,
        categoryId: SideMenuMainEnum.Setup,
        level: SideMenuLevelEnum.Page,
        children: [
          {
            id: Privileges.Setup.Products.View,
            name: 'View',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Setup.Products.Add,
            name: 'Add',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Setup.Products.Edit,
            name: 'Edit',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []

          },
          {
            id: Privileges.Setup.Categories.Delete,
            name: 'Delete',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Setup.Products.ChangePrice,
            name: 'ChangePrice',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Setup.Products.ChangeQuantity,
            name: 'ChangeQuantity',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Setup.Products.ViewProductTracking,
            name: 'ViewProductTracking',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []
          }

        ]
      },
      {
        id: SideMenuPageEnum.UnitOfMeasurements,
        name: 'UnitOfMeasurements',
        checked: false,
        categoryId: SideMenuMainEnum.Setup,
        level: SideMenuLevelEnum.Page,
        children: [
          {
            id: Privileges.Setup.UnitOfMeasurements.View,
            name: 'View',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Setup.UnitOfMeasurements.Add,
            name: 'Add',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Setup.UnitOfMeasurements.Edit,
            name: 'Edit',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []

          },
          {
            id: Privileges.Setup.UnitOfMeasurements.Delete,
            name: 'Delete',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []
          }
        ]
      },
      {
        id: SideMenuPageEnum.Clients,
        name: 'Clients',
        checked: false,
        categoryId: SideMenuMainEnum.Setup,
        level: SideMenuLevelEnum.Page,
        children: [
          {
            id: Privileges.Setup.Clients.View,
            name: 'View',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Setup.Clients.Add,
            name: 'Add',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Setup.Clients.Edit,
            name: 'Edit',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []

          },
          {
            id: Privileges.Setup.Clients.Delete,
            name: 'Delete',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []
          }
        ]
      },
      {
        id: SideMenuPageEnum.Vendors,
        name: 'Vendors',
        checked: false,
        categoryId: SideMenuMainEnum.Setup,
        level: SideMenuLevelEnum.Page,
        children: [
          {
            id: Privileges.Setup.Vendors.View,
            name: 'View',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Setup.Vendors.Add,
            name: 'Add',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Setup.Vendors.Edit,
            name: 'Edit',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []

          },
          {
            id: Privileges.Setup.Vendors.Delete,
            name: 'Delete',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []
          }
        ]
      },
      {
        id: SideMenuPageEnum.Representives,
        name: 'Representives',
        checked: false,
        categoryId: SideMenuMainEnum.Setup,
        level: SideMenuLevelEnum.Page,
        children: [
          {
            id: Privileges.Setup.Representives.View,
            name: 'View',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Setup.Representives.Add,
            name: 'Add',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Setup.Representives.Edit,
            name: 'Edit',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []

          },
          {
            id: Privileges.Setup.Representives.Delete,
            name: 'Delete',
            checked: false,
            categoryId: SideMenuMainEnum.Setup,
            level: SideMenuLevelEnum.Action,
            children: []
          }
        ]
      }

    ]
  },

  {
    id: SideMenuMainEnum.Purchases,
    name: 'Purchases',
    checked: false,
    level: SideMenuLevelEnum.MainCategory,
    children: [
      {
        id: SideMenuPageEnum.PurchasesBills,
        name: 'PurchasesBills',
        checked: false,
        categoryId: SideMenuMainEnum.Purchases,
        level: SideMenuLevelEnum.Page,
        children: [
          {
            id: Privileges.Purchases.PurchasesBills.View,
            name: 'View',
            checked: false,
            categoryId: SideMenuMainEnum.Purchases,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Purchases.PurchasesBills.Add,
            name: 'Add',
            checked: false,
            categoryId: SideMenuMainEnum.Purchases,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Purchases.PurchasesBills.Edit,
            name: 'Edit',
            checked: false,
            categoryId: SideMenuMainEnum.Purchases,
            level: SideMenuLevelEnum.Action,
            children: []

          },
          {
            id: Privileges.Purchases.PurchasesBills.Delete,
            name: 'Delete',
            checked: false,
            categoryId: SideMenuMainEnum.Purchases,
            level: SideMenuLevelEnum.Action,
            children: []
          }
        ]
      },
      {
        id: SideMenuPageEnum.TempPurchasesBills,
        name: 'TempPurchasesBills',
        checked: false,
        categoryId: SideMenuMainEnum.Purchases,
        level: SideMenuLevelEnum.Page,
        children: [
          {
            id: Privileges.Purchases.TempPurchasesBills.View,
            name: 'View',
            checked: false,
            categoryId: SideMenuMainEnum.Purchases,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Purchases.TempPurchasesBills.Add,
            name: 'Add',
            checked: false,
            categoryId: SideMenuMainEnum.Purchases,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Purchases.TempPurchasesBills.Edit,
            name: 'Edit',
            checked: false,
            categoryId: SideMenuMainEnum.Purchases,
            level: SideMenuLevelEnum.Action,
            children: []

          },
          {
            id: Privileges.Purchases.TempPurchasesBills.Delete,
            name: 'Delete',
            checked: false,
            categoryId: SideMenuMainEnum.Purchases,
            level: SideMenuLevelEnum.Action,
            children: []
          }
        ]
      },
      {
        id: SideMenuPageEnum.ReturnedPurchasesBills,
        name: 'ReturnedPurchasesBills',
        checked: false,
        categoryId: SideMenuMainEnum.Purchases,
        level: SideMenuLevelEnum.Page,
        children: [
          {
            id: Privileges.Purchases.ReturnedPurchasesBills.View,
            name: 'View',
            checked: false,
            categoryId: SideMenuMainEnum.Purchases,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Purchases.ReturnedPurchasesBills.Add,
            name: 'Add',
            checked: false,
            categoryId: SideMenuMainEnum.Purchases,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Purchases.ReturnedPurchasesBills.Edit,
            name: 'Edit',
            checked: false,
            categoryId: SideMenuMainEnum.Purchases,
            level: SideMenuLevelEnum.Action,
            children: []

          },
          {
            id: Privileges.Purchases.ReturnedPurchasesBills.Delete,
            name: 'Delete',
            checked: false,
            categoryId: SideMenuMainEnum.Purchases,
            level: SideMenuLevelEnum.Action,
            children: []
          }
        ]
      },
   

    ]
  },
  {
    id: SideMenuMainEnum.Sales,
    name: 'Sales',
    checked: false,
    level: SideMenuLevelEnum.MainCategory,
    children: [
      {
        id: SideMenuPageEnum.SalesBills,
        name: 'SalesBills',
        checked: false,
        categoryId: SideMenuMainEnum.Sales,
        level: SideMenuLevelEnum.Page,
        children: [
          {
            id: Privileges.Sales.SalesBills.View,
            name: 'View',
            checked: false,
            categoryId: SideMenuMainEnum.Sales,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Sales.SalesBills.Add,
            name: 'Add',
            checked: false,
            categoryId: SideMenuMainEnum.Sales,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Sales.SalesBills.Edit,
            name: 'Edit',
            checked: false,
            categoryId: SideMenuMainEnum.Sales,
            level: SideMenuLevelEnum.Action,
            children: []

          },
          {
            id: Privileges.Sales.SalesBills.Delete,
            name: 'Delete',
            checked: false,
            categoryId: SideMenuMainEnum.Sales,
            level: SideMenuLevelEnum.Action,
            children: []
          }
        ]
      },
      {
        id: SideMenuPageEnum.TempSalesBills,
        name: 'TempSalesBills',
        checked: false,
        categoryId: SideMenuMainEnum.Sales,
        level: SideMenuLevelEnum.Page,
        children: [
          {
            id: Privileges.Sales.TempSalesBills.View,
            name: 'View',
            checked: false,
            categoryId: SideMenuMainEnum.Sales,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Sales.TempSalesBills.Add,
            name: 'Add',
            checked: false,
            categoryId: SideMenuMainEnum.Sales,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Sales.TempSalesBills.Edit,
            name: 'Edit',
            checked: false,
            categoryId: SideMenuMainEnum.Sales,
            level: SideMenuLevelEnum.Action,
            children: []

          },
          {
            id: Privileges.Sales.TempSalesBills.Delete,
            name: 'Delete',
            checked: false,
            categoryId: SideMenuMainEnum.Purchases,
            level: SideMenuLevelEnum.Action,
            children: []
          }
        ]
      },
      {
        id: SideMenuPageEnum.ReturnedSalesBills,
        name: 'ReturnedSalesBills',
        checked: false,
        categoryId: SideMenuMainEnum.Sales,
        level: SideMenuLevelEnum.Page,
        children: [
          {
            id: Privileges.Sales.ReturnedSalesBills.View,
            name: 'View',
            checked: false,
            categoryId: SideMenuMainEnum.Sales,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Sales.ReturnedSalesBills.Add,
            name: 'Add',
            checked: false,
            categoryId: SideMenuMainEnum.Sales,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Sales.ReturnedSalesBills.Edit,
            name: 'Edit',
            checked: false,
            categoryId: SideMenuMainEnum.Sales,
            level: SideMenuLevelEnum.Action,
            children: []

          },
          {
            id: Privileges.Sales.ReturnedSalesBills.Delete,
            name: 'Delete',
            checked: false,
            categoryId: SideMenuMainEnum.Purchases,
            level: SideMenuLevelEnum.Action,
            children: []
          }
        ]
      }

    ]
  },
  {
    id: SideMenuMainEnum.UserManagement,
    name: 'UserManagement',
    checked: false,
    level: SideMenuLevelEnum.MainCategory,
    children: [
      {
        id: SideMenuPageEnum.Users,
        name: 'Users',
        checked: false,
        categoryId: SideMenuMainEnum.UserManagement,
        level: SideMenuLevelEnum.Page,
        children: [
          {
            id: Privileges.UserManagement.Users.View,
            name: 'View',
            checked: false,
            categoryId: SideMenuMainEnum.UserManagement,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.UserManagement.Users.Add,
            name: 'Add',
            checked: false,
            categoryId: SideMenuMainEnum.UserManagement,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.UserManagement.Users.Edit,
            name: 'Edit',
            checked: false,
            categoryId: SideMenuMainEnum.UserManagement,
            level: SideMenuLevelEnum.Action,
            children: []

          },
          {
            id: Privileges.UserManagement.Users.Delete,
            name: 'Delete',
            checked: false,
            categoryId: SideMenuMainEnum.UserManagement,
            level: SideMenuLevelEnum.Action,
            children: []
          }
        ]
      },
      {
        id: SideMenuPageEnum.Roles,
        name: 'Roles',
        checked: false,
        categoryId: SideMenuMainEnum.UserManagement,
        level: SideMenuLevelEnum.Page,
        children: [
          {
            id: Privileges.UserManagement.Roles.View,
            name: 'View',
            checked: false,
            categoryId: SideMenuMainEnum.UserManagement,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.UserManagement.Roles.Add,
            name: 'Add',
            checked: false,
            categoryId: SideMenuMainEnum.UserManagement,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.UserManagement.Roles.Edit,
            name: 'Edit',
            checked: false,
            categoryId: SideMenuMainEnum.UserManagement,
            level: SideMenuLevelEnum.Action,
            children: []

          },
          {
            id: Privileges.UserManagement.Roles.Delete,
            name: 'Delete',
            checked: false,
            categoryId: SideMenuMainEnum.UserManagement,
            level: SideMenuLevelEnum.Action,
            children: []
          }
        ]
      }
    ]
  },

  {
    id: SideMenuMainEnum.Accounting,
    name: 'Accounting',
    checked: false,
    level: SideMenuLevelEnum.MainCategory,
    children: [
      {
        id: SideMenuPageEnum.Treasuries,
        name: 'Treasuries',
        checked: false,
        categoryId: SideMenuMainEnum.Accounting,
        level: SideMenuLevelEnum.Page,
        children: [
          {
            id: Privileges.Accounting.Treasuries.View,
            name: 'View',
            checked: false,
            categoryId: SideMenuMainEnum.Accounting,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Accounting.Treasuries.Add,
            name: 'Add',
            checked: false,
            categoryId: SideMenuMainEnum.Accounting,
            level: SideMenuLevelEnum.Action,
            children: []
          },
          {
            id: Privileges.Accounting.Treasuries.Edit,
            name: 'Edit',
            checked: false,
            categoryId: SideMenuMainEnum.Accounting,
            level: SideMenuLevelEnum.Action,
            children: []

          },
          {
            id: Privileges.Accounting.Treasuries.Delete,
            name: 'Delete',
            checked: false,
            categoryId: SideMenuMainEnum.Accounting,
            level: SideMenuLevelEnum.Action,
            children: []
          }
        ]
      }
    ]
  },

  {
    id: SideMenuMainEnum.Reports,
    name: 'Reports',
    checked: false,
    level: SideMenuLevelEnum.MainCategory,
    children: [
      {
        id: SideMenuPageEnum.AccountStatementAllClients,
        name: 'AccountStatementAllClients',
        checked: false,
        categoryId: SideMenuMainEnum.Reports,
        level: SideMenuLevelEnum.Page,
        children: [
          {
            id: Privileges.Reports.AccountStatementAllClients.View,
            name: 'View',
            checked: false,
            categoryId: SideMenuMainEnum.Reports,
            level: SideMenuLevelEnum.Action,
            children: []
          }
        ]
      },
      {
        id: SideMenuPageEnum.AccountStatementSingleClient,
        name: 'AccountStatementSingleClient',
        checked: false,
        categoryId: SideMenuMainEnum.Reports,
        level: SideMenuLevelEnum.Page,
        children: [
          {
            id: Privileges.Reports.AccountStatementSingleClient.View,
            name: 'View',
            checked: false,
            categoryId: SideMenuMainEnum.Reports,
            level: SideMenuLevelEnum.Action,
            children: []
          }
        ]
      },
      {
        id: SideMenuPageEnum.AccountStatementAllVendors,
        name: 'AccountStatementAllVendors',
        checked: false,
        categoryId: SideMenuMainEnum.Reports,
        level: SideMenuLevelEnum.Page,
        children: [
          {
            id: Privileges.Reports.AccountStatementAllVendors.View,
            name: 'View',
            checked: false,
            categoryId: SideMenuMainEnum.Reports,
            level: SideMenuLevelEnum.Action,
            children: []
          }
        ]
      },
      {
        id: SideMenuPageEnum.AccountStatementSingleVendor,
        name: 'AccountStatementSingleVendor',
        checked: false,
        categoryId: SideMenuMainEnum.Reports,
        level: SideMenuLevelEnum.Page,
        children: [
          {
            id: Privileges.Reports.AccountStatementSingleVendor.View,
            name: 'View',
            checked: false,
            categoryId: SideMenuMainEnum.Reports,
            level: SideMenuLevelEnum.Action,
            children: []
          }
        ]
      },
      {
        id: SideMenuPageEnum.ProductMinusQuantity,
        name: 'ProductMinusQuantity',
        checked: false,
        categoryId: SideMenuMainEnum.Reports,
        level: SideMenuLevelEnum.Page,
        children: [
          {
            id: Privileges.Reports.ProductMinusQuantity.View,
            name: 'View',
            checked: false,
            categoryId: SideMenuMainEnum.Reports,
            level: SideMenuLevelEnum.Action,
            children: []
          }
        ]
      },
      {
        id: SideMenuPageEnum.ProductLowQuantity,
        name: 'ProductLowQuantity',
        checked: false,
        categoryId: SideMenuMainEnum.Reports,
        level: SideMenuLevelEnum.Page,
        children: [
          {
            id: Privileges.Reports.ProductLowQuantity.View,
            name: 'View',
            checked: false,
            categoryId: SideMenuMainEnum.Reports,
            level: SideMenuLevelEnum.Action,
            children: []
          }
        ]
      }
    ]
  },
]