using Account.DataAccessLayer;
using Data.Entities.Setup;
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
        IClientVendorDAL ClientVendorDAL { get; }
        #endregion

        #region Purchase
        IPurchasesBillHeaderDAL PurchasesBillHeaderDAL { get; }
        IPurchasesBillDetailDAL PurchasesBillDetailDAL { get; }
        #endregion

        #region Sales
        ISalesBillHeaderDAL SalesBillHeaderDAL { get; }
        ISalesBillDetailDAL SalesBillDetailDAL { get; }
        #endregion
        Task CompleteAsync();
    }
}
