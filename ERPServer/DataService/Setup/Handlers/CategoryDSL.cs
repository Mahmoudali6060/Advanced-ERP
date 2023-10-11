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

namespace DataService.Setup.Handlers
{
    public class CategoryDSL : ICategoryDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public CategoryDSL(IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        #region Query
        public async Task<ResponseEntityList<CategoryDTO>> GetAll(CategorySearchDTO searchCriteriaDTO)
        {
            var categoryList = await _unitOfWork.CategoryDAL.GetAll();
            int total = categoryList.Count();

            #region Apply Filters
            categoryList = ApplyFilert(categoryList, searchCriteriaDTO);
            #endregion

            #region Apply Pagination
            categoryList = categoryList.Skip((searchCriteriaDTO.Page - 1) * searchCriteriaDTO.PageSize).Take(searchCriteriaDTO.PageSize);
            #endregion

            #region Mapping and Return List
            var userProfileDTOList = _mapper.Map<IEnumerable<CategoryDTO>>(categoryList);
            return new ResponseEntityList<CategoryDTO>
            {
                List = userProfileDTOList,
                Total = total
            };
            #endregion

        }

        public async Task<CategoryDTO> GetById(long id)
        {
            var test = _mapper.Map<CategoryDTO>(await _unitOfWork.CategoryDAL.GetById(id));
            return _mapper.Map<CategoryDTO>(await _unitOfWork.CategoryDAL.GetById(id));
        }

        public async Task<ResponseEntityList<CategoryDTO>> GetAllLite()
        {
            return new ResponseEntityList<CategoryDTO>()
            {
                List = _mapper.Map<IEnumerable<CategoryDTO>>(_unitOfWork.CategoryDAL.GetAllLite().Result),
                Total = _unitOfWork.CategoryDAL.GetAllLite().Result.Count()
            };
        }



        #endregion

        #region Command
        public async Task<long> Add(CategoryDTO entity)
        {
            entity.Code = "C" + DateTime.Now.ToString("ddMMyyHHmmssff");//ddMMyyHHmmssff
            return await _unitOfWork.CategoryDAL.Add(_mapper.Map<Category>(entity));
        }

        public async Task<long> Update(CategoryDTO entity)
        {
            return await _unitOfWork.CategoryDAL.Update(_mapper.Map<Category>(entity));
        }

        public async Task<bool> Delete(long id)
        {
            Category entity = await _unitOfWork.CategoryDAL.GetById(id);
            return await _unitOfWork.CategoryDAL.Delete(entity);
        }
        #endregion

        #region Helper Methods
        private IQueryable<Category> ApplyFilert(IQueryable<Category> categoryList, CategorySearchDTO searchCriteriaDTO)
        {
            if (searchCriteriaDTO.IsActive.HasValue)
            {
                categoryList = categoryList.Where(x => x.IsActive == searchCriteriaDTO.IsActive);
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Code))
            {
                categoryList = categoryList.Where(x => x.Code.Contains(searchCriteriaDTO.Code));
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Name))
            {
                categoryList = categoryList.Where(x => x.Name.Contains(searchCriteriaDTO.Name));
            }
            return categoryList;
        }

        #endregion
    }
}
