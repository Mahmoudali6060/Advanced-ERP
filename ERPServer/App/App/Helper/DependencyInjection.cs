using Account.DataAccessLayer;
using Account.DataServiceLayer;
using Account.DataServiceLayer.Contracts;
using Account.DataServiceLayer.Handlers;
using Accout.DataServiceLayer;
using DataAccess.Setup.Contracts;
using DataAccess.Setup.Handlers;
using DataService.Setup.Contracts;
using DataService.Setup.Handlers;

//using Data.Backup;
using Infrastructure.Contracts;
using Infrastructure.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Setting.DataAccessLayer;
using Setting.DataServiceLayer;
using Setup.DataAccessLayer;
using Setup.DataServiceLayer;
using UnitOfWork.Contracts;
using UnitOfWork.Handlers;

namespace App.Helper
{
    public class DependencyInjection
    {
        public static void AddTransient(IServiceCollection services)
        {
            #region Settings
            services.AddTransient<ISettingDSL, SettingDSL>();
            services.AddTransient<ISettingDAL, SettingDAL>();
            //services.AddTransient<IDatabaseBackupDSL, DatabaseBackupDSL>();
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

            #endregion

            #region User Management
            services.AddTransient<IAccountDSL, AccountDSL>();
            services.AddTransient<IAccountDAL, AccountDAL>();

            services.AddTransient<IUserProfileDAL, UserProfileDAL>();
            services.AddTransient<IUserProfileDSL, UserProfileDSL>();

            services.AddTransient<IEmailSender, EmailSender>();

            #endregion

            #region Unit Of Work
            services.AddScoped<IUnitOfWork, UnitofWork>();
            #endregion

        }
    }
}
