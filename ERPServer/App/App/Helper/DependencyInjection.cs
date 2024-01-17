using Account.DataAccessLayer;
using Account.DataServiceLayer;
using Account.DataServiceLayer.Contracts;
using Account.DataServiceLayer.Handlers;
using Accounting.DataAccessLayer;
using Accout.DataServiceLayer;
using DataAccess.Accounting.Contracts;
using DataAccess.Setup.Contracts;
using DataAccess.Setup.Handlers;
using DataService.Accounting.Contracts;
using DataService.Accounting.Handlers;
using DataService.Purchases.Contracts;
using DataService.Sales.Contracts;
using DataService.Sales.Handlers;
using DataService.Setup.Contracts;
using DataService.Setup.Handlers;

//using Data.Backup;
using Infrastructure.Contracts;
using Infrastructure.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Purchases.DataAccessLayer;
using Sales.DataAccessLayer;
using Setup.DataAccessLayer;
using UnitOfWork.Contracts;
using UnitOfWork.Handlers;

namespace App.Helper
{
    public class DependencyInjection
    {
        public static void AddTransient(IServiceCollection services)
        {

            #region Unit Of Work
            services.AddScoped<IUnitOfWork, UnitofWork>();
            #endregion

            #region Infrastructure
            services.AddTransient<ILoggerManager, LoggerManager>();
            services.AddTransient<IFileManager, FileManager>();
            #endregion

            #region Setup
            services.AddTransient<ICountryDSL, CountryDSL>();
            services.AddTransient<ICountryDAL, CountryDAL>();

            services.AddTransient<IStateDSL, StateDSL>();
            services.AddTransient<IStateDAL, StateDAL>();

            services.AddTransient<ICityDSL, CityDSL>();
            services.AddTransient<ICityDAL, CityDAL>();

            services.AddTransient<IContactUsDSL, ContactUsDSL>();
            services.AddTransient<IContactUsDAL, ContactUsDAL>();

            services.AddTransient<IAboutUsDSL, AboutUsDSL>();
            services.AddTransient<IAboutUsDAL, AboutUsDAL>();

            services.AddTransient<IAdvertismentDAL, AdvertismentDAL>();
            services.AddTransient<IAdvertismentDSL, AdvertismentDSL>();

            services.AddTransient<ICategoryDAL, CategoryDAL>();
            services.AddTransient<ICategoryDSL, CategoryDSL>();

            services.AddTransient<IProductDAL, ProductDAL>();
            services.AddTransient<IProductDSL, ProductDSL>();

            services.AddTransient<IClientVendorDAL, ClientVendorDAL>();
            services.AddTransient<IClientVendorDSL, ClientVendorDSL>();

            services.AddTransient<ICompanyDAL, CompanyDAL>();
            services.AddTransient<ICompanyDSL, CompanyDSL>();

            services.AddTransient<IRepresentiveDAL, RepresentiveDAL>();
            services.AddTransient<IRepresentiveDSL, RepresentiveDSL>();

            services.AddTransient<IUnitOfMeasurementDAL, UnitOfMeasurementDAL>();
            services.AddTransient<IUnitOfMeasurementDSL, UnitOfMeasurementDSL>();

            #endregion

            #region User Management
            services.AddTransient<IAccountDSL, AccountDSL>();
            services.AddTransient<IAccountDAL, AccountDAL>();

            services.AddTransient<IUserProfileDAL, UserProfileDAL>();
            services.AddTransient<IUserProfileDSL, UserProfileDSL>();

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddTransient<IRoleDAL, RoleDAL>();
            services.AddTransient<IRoleDSL, RoleDSL>();

            #endregion

            #region Purchases
            services.AddTransient<IPurchasesBillHeaderDAL, PurchasesBillHeaderDAL>();
            services.AddTransient<IPurchasesBillHeaderDSL, PurchasesBillHeaderDSL>();

            services.AddTransient<IPurchasesBillDetailDAL, PurchasesBillDetailDAL>();
            services.AddTransient<IPurchasesBillDetailDSL, PurchasesBillDetailDSL>();
            #endregion

            #region Sales
            services.AddTransient<ISalesBillHeaderDAL, SalesBillHeaderDAL>();
            services.AddTransient<ISalesBillHeaderDSL, SalesBillHeaderDSL>();

            services.AddTransient<ISalesBillDetailDAL, SalesBillDetailDAL>();
            services.AddTransient<ISalesBillDetailDSL, SalesBillDetailDSL>();
            #endregion

            #region Accouting
            services.AddTransient<ITreasuryDAL, TreasuryDAL>();
            services.AddTransient<ITreasuryDSL, TreasuryDSL>();

            services.AddTransient<IAccountStatementDAL, AccountStatementDAL>();
            services.AddTransient<IAccountStatementDSL, AccountStatementDSL>();
            #endregion

        }
    }
}
