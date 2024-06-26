﻿using Account.DataAccessLayer;
using Data.Entities.Setup;
using DataAccess.Accounting.Contracts;
using DataAccess.Setup.Contracts;
using Purchases.DataAccessLayer;
using Sales.DataAccessLayer;
using Setup.DataAccessLayer;
using System.Threading.Tasks;

namespace UnitOfWork.Contracts
{
    public interface IUnitOfWork
    {
        #region User Management
        IUserProfileDAL UserProfileDAL { get; }
        IAccountDAL AccountDAL { get; }
        IRoleDAL RoleDAL { get; }
        #endregion

        #region Setup
        ICountryDAL CountryDAL { get; }
        IStateDAL StateDAL { get; }
        ICityDAL CityDAL { get; }
        IContactUsDAL ContactUsDAL { get; }
        IAboutUsDAL AboutUsDAL { get; }
        IAdvertismentDAL AdvertismentDAL { get; }
        ICategoryDAL CategoryDAL { get; }
        IProductDAL ProductDAL { get; }
        IProductTrackingDAL ProductTrackingDAL { get; }

        IClientVendorDAL ClientVendorDAL { get; }
        ICompanyDAL CompanyDAL { get; }
        IRepresentiveDAL RepresentiveDAL { get; }
        IUnitOfMeasurementDAL UnitOfMeasurementDAL { get; }
        #endregion

        #region Purchase
        IPurchasesBillHeaderDAL PurchasesBillHeaderDAL { get; }
        IPurchasesBillDetailDAL PurchasesBillDetailDAL { get; }
        #endregion

        #region Sales
        ISalesBillHeaderDAL SalesBillHeaderDAL { get; }
        ISalesBillDetailDAL SalesBillDetailDAL { get; }
        #endregion

        #region Accounting
        ITreasuryDAL TreasuryDAL { get; }
        IAccountStatementDAL AccountStatementDAL { get; }
        #endregion
        Task CompleteAsync();
        public void Complete();

    }
}
