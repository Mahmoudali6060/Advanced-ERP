using Account.DataAccessLayer;
using Data.Contexts;
using Data.Entities.UserManagement;
using DataAccess.Setup.Contracts;
using DataAccess.Setup.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Purchases.DataAccessLayer;
using Sales.DataAccessLayer;
using Setup.DataAccessLayer;

using System;
using System.Data.Common;
using System.Threading.Tasks;
using UnitOfWork.Contracts;

namespace UnitOfWork.Handlers
{
    public class UnitofWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ILogger _logger;

        #region User Management
        public IUserProfileDAL UserProfileDAL { get; private set; }
        public IAccountDAL AccountDAL { get; private set; }
        public IRoleDAL RoleDAL { get; private set; }
        #endregion

        #region Setup
        public ICountryDAL CountryDAL { get; private set; }
        public IStateDAL StateDAL { get; private set; }
        public ICityDAL CityDAL { get; private set; }
        public IContactUsDAL ContactUsDAL { get; private set; }
        public IAboutUsDAL AboutUsDAL { get; private set; }
        public ICategoryDAL CategoryDAL { get; private set; }
        public IProductDAL ProductDAL { get; private set; }
        public IClientVendorDAL ClientVendorDAL { get; private set; }
        public IAdvertismentDAL AdvertismentDAL { get; private set; }
        public ICompanyDAL CompanyDAL { get; private set; }
        public IRepresentiveDAL RepresentiveDAL { get; private set; }
        public IUnitOfMeasurementDAL UnitOfMeasurementDAL { get; private set; }
        #endregion

        #region Purchases
        public IPurchasesBillHeaderDAL PurchasesBillHeaderDAL { get; private set; }
        public IPurchasesBillDetailDAL PurchasesBillDetailDAL { get; set; }
        #endregion

        #region Sales
        public ISalesBillHeaderDAL SalesBillHeaderDAL { get; private set; }
        public ISalesBillDetailDAL SalesBillDetailDAL { get; set; }
        #endregion

        public UnitofWork(AppDbContext context,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            ILoggerFactory loggerFactory)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = loggerFactory.CreateLogger("logs");
            #region User Management
            UserProfileDAL = new UserProfileDAL(_context);
            RoleDAL = new RoleDAL(_context);
            AccountDAL = new AccountDAL(_signInManager, _userManager, _context, _roleManager);
            #endregion

            #region Setup
            CountryDAL = new CountryDAL(_context);
            StateDAL = new StateDAL(_context);
            CityDAL = new CityDAL(_context);
            ContactUsDAL = new ContactUsDAL(_context);
            AboutUsDAL = new AboutUsDAL(_context);
            AdvertismentDAL = new AdvertismentDAL(_context);
            CategoryDAL = new CategoryDAL(_context);
            ProductDAL = new ProductDAL(_context);
            ClientVendorDAL = new ClientVendorDAL(_context);
            CompanyDAL = new CompanyDAL(_context);
            RepresentiveDAL = new RepresentiveDAL(_context);
            UnitOfMeasurementDAL=new UnitOfMeasurementDAL(_context);
            #endregion

            #region Purchases
            PurchasesBillHeaderDAL = new PurchasesBillHeaderDAL(_context);
            PurchasesBillDetailDAL = new PurchasesBillDetailDAL(_context);
            #endregion

            #region Sales
            SalesBillHeaderDAL = new SalesBillHeaderDAL(_context);
            SalesBillDetailDAL = new SalesBillDetailDAL(_context);
            #endregion
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync(true);
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
