using AutoMapper;
using Data.Entities.Setup;
using DataService.Setup.Contracts;
using IdentityModel;
using Infrastructure.Contracts;
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
    public class AdvertismentDSL : IAdvertismentDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;

        public AdvertismentDSL(IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }
        public async Task<long> Add(AdvertismentDTO entity)
        {
            return await _unitOfWork.AdvertismentDAL.Add(_mapper.Map<Advertisment>(entity));
        }

        public async Task<long> AddRang(List<AdvertismentDTO> lstAdvertisments)
        {
            List<Advertisment> advertisments = new List<Advertisment>();

            foreach (var item in lstAdvertisments)
            {
                UploadImage(item);
                Advertisment advertisment = new Advertisment();
                advertisment.Media = item.Media;
                advertisments.Add(advertisment);
            }
            return await _unitOfWork.AdvertismentDAL.AddRang(advertisments);
        }

        public async Task<bool> Delete(long id)
        {
            Advertisment Advertisment = await _unitOfWork.AdvertismentDAL.GetById(id);
            return await _unitOfWork.AdvertismentDAL.Delete(Advertisment);
        }

        public async Task<ResponseEntityList<AdvertismentDTO>> GetAll(AdvertismentSearchDTO searchCrieria)
        {
            var AdvertismentList = await _unitOfWork.AdvertismentDAL.GetAll();
            int total = AdvertismentList.Count();

            var lstAdsDTO = _mapper.Map<IEnumerable<AdvertismentDTO>>(AdvertismentList);
            #region Return List
            return new ResponseEntityList<AdvertismentDTO>
            {
                List = lstAdsDTO,
                Total = total
            };
            #endregion
        }

        public async Task<ResponseEntityList<AdvertismentDTO>> GetAllLite()
        {
            return new ResponseEntityList<AdvertismentDTO>()
            {
                List = _mapper.Map<IQueryable<AdvertismentDTO>>(_unitOfWork.AdvertismentDAL.GetAllLite().Result),
                Total = _unitOfWork.AdvertismentDAL.GetAllLite().Result.Count()
            };
        }

        public async Task<AdvertismentDTO> GetById(long id)
        {
            return _mapper.Map<AdvertismentDTO>(await _unitOfWork.AdvertismentDAL.GetById(id));
        }

        public async Task<long> Update(AdvertismentDTO entity)
        {
            UploadImage(entity);
            return await _unitOfWork.AdvertismentDAL.Update(_mapper.Map<Advertisment>(entity));
        }
        #region Helper Methods
        private bool UploadImage(AdvertismentDTO entity)
        {
            if (entity.MediaBase64 != null)
            {
                entity.Media = string.IsNullOrWhiteSpace(entity.MediaBase64) ? null : DateTime.Now.ToString("yyyy_MM_dd_HH_ss_fffffff") + ".jpg";
                return _fileManager.UploadImageBase64("wwwroot/Images/Managment/" + entity.Media, entity.MediaBase64);
            }
            return true;
        }
        #endregion
    }
}