using Data.Entities.Setup;
using DataService.Setup.Contracts;
using Shared.Entities.Setup;
using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Contracts;

namespace DataService.Setup.Handlers
{
    public class AboutUsDSL : IAboutUsDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        public AboutUsDSL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<long> Add(AboutUs entity)
        {
            return await _unitOfWork.AboutUsDAL.Add(entity);
        }

        public async Task<bool> Delete(long id)
        {
            AboutUs AboutUs = await _unitOfWork.AboutUsDAL.GetById(id);
            return await _unitOfWork.AboutUsDAL.Delete(AboutUs);
        }

        public async Task<ResponseEntityList<AboutUs>> GetAll(AboutUs searchCrieria)
        {
            var aboutUsList = await _unitOfWork.AboutUsDAL.GetAll();
            int total = aboutUsList.Count();

            #region Return List
            return new ResponseEntityList<AboutUs>
            {
                List = aboutUsList,
                Total = total
            };
            #endregion
        }

        public async Task<ResponseEntityList<AboutUs>> GetAllLite()
        {
            return new ResponseEntityList<AboutUs>()
            {
                List = _unitOfWork.AboutUsDAL.GetAllLite().Result,
                Total = _unitOfWork.AboutUsDAL.GetAllLite().Result.Count()
            };
        }

        public async Task<AboutUs> GetById(long id)
        {
            return await _unitOfWork.AboutUsDAL.GetById(id);
        }

        public async Task<long> Update(AboutUs entity)
        {
            return await _unitOfWork.AboutUsDAL.Update(entity);
        }
    }
}