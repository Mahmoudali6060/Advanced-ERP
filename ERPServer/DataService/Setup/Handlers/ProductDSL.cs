using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Entities.Shared;
using Infrastructure.Contracts;
using UnitOfWork.Contracts;
using Shared.Entities.Setup;
using Data.Entities.Setup;
using DataService.Setup.Contracts;
using System;
using Entities.Account;

namespace DataService.Setup.Handlers
{
    public class ProductDSL : IProductDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public ProductDSL(IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        #region Query
        public async Task<ResponseEntityList<ProductDTO>> GetAll(ProductSearchDTO searchCriteriaDTO)
        {
            var userProfileList = await _unitOfWork.ProductDAL.GetAll();
            int total = userProfileList.Count();

            #region Apply Filters
            userProfileList = ApplyFilert(userProfileList, searchCriteriaDTO);
            #endregion

            #region Apply Pagination
            userProfileList = userProfileList.Skip((searchCriteriaDTO.Page - 1) * searchCriteriaDTO.PageSize).Take(searchCriteriaDTO.PageSize);
            #endregion

            #region Mapping and Return List
            var userProfileDTOList = _mapper.Map<IEnumerable<ProductDTO>>(userProfileList);
            return new ResponseEntityList<ProductDTO>
            {
                List = userProfileDTOList,
                Total = total
            };
            #endregion

        }

        public async Task<ProductDTO> GetById(long id)
        {
            var test = _mapper.Map<ProductDTO>(await _unitOfWork.ProductDAL.GetById(id));
            return _mapper.Map<ProductDTO>(await _unitOfWork.ProductDAL.GetById(id));
        }

        public async Task<ResponseEntityList<ProductDTO>> GetAllLite()
        {
            return new ResponseEntityList<ProductDTO>()
            {
                List = _mapper.Map<IEnumerable<ProductDTO>>(_unitOfWork.ProductDAL.GetAllLite().Result),
                Total = _unitOfWork.ProductDAL.GetAllLite().Result.Count()
            };
        }

        public async Task<ResponseEntityList<ProductDTO>> GetAllLiteByCategoryId(long categoryId)
        {
            return new ResponseEntityList<ProductDTO>()
            {
                List = _mapper.Map<IEnumerable<ProductDTO>>(_unitOfWork.ProductDAL.GetAllLiteByCategoryId(categoryId).Result),
                Total = _unitOfWork.ProductDAL.GetAllLite().Result.Count()
            };
        }
        #endregion

        #region Command
        public async Task<long> Add(ProductDTO entity)
        {
            UploadImage(entity);
            var result = await _unitOfWork.ProductDAL.Add(_mapper.Map<Product>(entity));
            return result;
        }

        public async Task<long> Update(ProductDTO entity)
        {
            UploadImage(entity);
            return await _unitOfWork.ProductDAL.Update(_mapper.Map<Product>(entity));
        }

        public async Task<bool> Delete(long id)
        {
            Product entity = await _unitOfWork.ProductDAL.GetById(id);
            return await _unitOfWork.ProductDAL.Delete(entity);
        }
        #endregion

        #region Helper Methods
        private IQueryable<Product> ApplyFilert(IQueryable<Product> productList, ProductSearchDTO searchCriteriaDTO)
        {
            if (searchCriteriaDTO.IsActive.HasValue)
            {
                productList = productList.Where(x => x.IsActive == searchCriteriaDTO.IsActive);
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Code))
            {
                productList = productList.Where(x => x.Code.Contains(searchCriteriaDTO.Code));
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Name))
            {
                productList = productList.Where(x => x.Name.Contains(searchCriteriaDTO.Name));
            }
            return productList;
        }

        private bool UploadImage(ProductDTO entity)
        {
            if (entity.ImageBase64 != null)
            {
                entity.ImageUrl = string.IsNullOrWhiteSpace(entity.ImageBase64) ? null : entity.Name + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_ss") + ".jpg";
                return _fileManager.UploadImageBase64("wwwroot/Images/Products/" + entity.ImageUrl, entity.ImageBase64);
            }
            return true;
        }
        #endregion
    }
}
