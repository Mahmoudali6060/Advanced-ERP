using Account.DataAccessLayer;
using Data.Entities.Setup;
using DataAccess.Setup.Contracts;
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
        IClientDAL ClientDAL { get; }
        IVendorDAL VendorDAL { get; }
        #endregion

        Task CompleteAsync();
    }
}
