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
using Shared.Enums;

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
            var productList = await _unitOfWork.ProductDAL.GetAllWithIncludes(null, x => x.Category);

            #region Apply Filters
            productList = productList.OrderBy(x => x.Id);
            productList = ApplyFilert(productList, searchCriteriaDTO);
            int total = productList.Count();
            #endregion

            #region Apply Pagination
            productList = productList.Skip((searchCriteriaDTO.Page - 1) * searchCriteriaDTO.PageSize).Take(searchCriteriaDTO.PageSize);
            #endregion

            #region Mapping and Return List
            var userProfileDTOList = _mapper.Map<IEnumerable<ProductDTO>>(productList);
            return new ResponseEntityList<ProductDTO>
            {
                List = userProfileDTOList,
                Total = total
            };
            #endregion

        }

        public async Task<ResponseEntityList<ProductTrackingDTO>> GetProductTrackingByProductId(ProductTrackingSearchDTO searchCriteriaDTO)
        {
            var productTrackingList = await _unitOfWork.ProductTrackingDAL.GetProductTrackingByProductId(searchCriteriaDTO.ProductId);

            int total = productTrackingList.Count();

            if (searchCriteriaDTO.ProductProcessTypeId.HasValue)
            {
                productTrackingList = productTrackingList.Where(x => x.ProductProcessTypeId == searchCriteriaDTO.ProductProcessTypeId);
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Date))
            {
                productTrackingList = productTrackingList.Where(x => x.Date.Date == DateTime.Parse(searchCriteriaDTO.Date).Date);
            }

            #region Apply Pagination
            productTrackingList = productTrackingList.Skip((searchCriteriaDTO.Page - 1) * searchCriteriaDTO.PageSize).Take(searchCriteriaDTO.PageSize);
            #endregion

            #region Mapping and Return List
            var productTrackingDTOList = _mapper.Map<IEnumerable<ProductTrackingDTO>>(productTrackingList);
            return new ResponseEntityList<ProductTrackingDTO>
            {
                List = productTrackingDTOList,
                Total = total
            };
            #endregion

        }

        public async Task<ProductDTO> GetById(long id)
        {
            var test = _mapper.Map<ProductDTO>(await _unitOfWork.ProductDAL.GetByIdAsync(id));
            return _mapper.Map<ProductDTO>(await _unitOfWork.ProductDAL.GetByIdAsync(id));
        }

        public async Task<ResponseEntityList<ProductDTO>> GetAllLite()
        {
            return new ResponseEntityList<ProductDTO>()
            {
                List = _mapper.Map<IEnumerable<ProductDTO>>(_unitOfWork.ProductDAL.GetAllLiteAsync().Result),
                Total = _unitOfWork.ProductDAL.GetAllLiteAsync().Result.Count()
            };
        }

        public async Task<ResponseEntityList<ProductDTO>> GetAllLiteByCategoryId(long categoryId)
        {
            return new ResponseEntityList<ProductDTO>()
            {
                List = _mapper.Map<IEnumerable<ProductDTO>>(_unitOfWork.ProductDAL.GetAllLiteByCategoryId(categoryId).Result),
                Total = _unitOfWork.ProductDAL.GetAllLiteAsync().Result.Count()
            };
        }
        #endregion

        #region Command
        public async Task<long> Add(ProductDTO entityDTO)
        {
            UploadImage(entityDTO);
            var entity = _mapper.Map<Product>(entityDTO);
            entity.Code = GenerateSequenceNumber();
            entity.ProductTrackings = new List<ProductTracking>();
            entity.ProductTrackings.Add(GenerateProductTrackingList(entityDTO));
            var result = await _unitOfWork.ProductDAL.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return entity.Id;
        }

        private ProductTracking GenerateProductTrackingList(ProductDTO entityDTO)
        {
            return new ProductTracking()
            {
                Date = DateTime.Now,
                OldData = "",
                NewData = entityDTO.Code + " " + entityDTO.Name,
                ProductProcessTypeId = ProductProcessTypeEnum.Created
            };
        }

        public async Task<long> Update(ProductDTO entity)
        {
            UploadImage(entity);
            SetProductTracking(entity);
            var result = await _unitOfWork.ProductDAL.UpdateAsync(_mapper.Map<Product>(entity));
            await _unitOfWork.CompleteAsync();
            return result;
        }

        private void SetProductTracking(ProductDTO newProduct)
        {
            var oldProduct = _unitOfWork.ProductDAL.GetByIdAsync(newProduct.Id).Result;
            if (oldProduct == null)
            {
                ProductTracking productTracking = new ProductTracking()
                {
                    ProductId = oldProduct.Id,
                    Date = DateTime.Now,
                    OldData = oldProduct.Price.ToString(),
                    NewData = newProduct.Price.ToString(),
                    ProductProcessTypeId = ProductProcessTypeEnum.ChangePrice
                };
                _unitOfWork.ProductTrackingDAL.AddAsync(productTracking);
            }

            if (oldProduct != null)
            {
                if (oldProduct.Price != newProduct.Price)
                {
                    ProductTracking productTracking = new ProductTracking()
                    {
                        ProductId = oldProduct.Id,
                        Date = DateTime.Now,
                        OldData = oldProduct.Price.ToString(),
                        NewData = newProduct.Price.ToString(),
                        ProductProcessTypeId = ProductProcessTypeEnum.ChangePrice
                    };
                    _unitOfWork.ProductTrackingDAL.AddAsync(productTracking);
                }

                if (oldProduct.ActualQuantity != newProduct.ActualQuantity)
                {
                    ProductTracking productTracking = new ProductTracking()
                    {
                        ProductId = oldProduct.Id,
                        Date = DateTime.Now,
                        OldData = oldProduct.ActualQuantity.ToString(),
                        NewData = newProduct.ActualQuantity.ToString(),
                        ProductProcessTypeId = ProductProcessTypeEnum.ChangeQuantity
                    };
                    _unitOfWork.ProductTrackingDAL.AddAsync(productTracking);
                }
            }
        }

        public async Task<bool> UpdateAll(List<ProductDTO> entityList)
        {
            foreach (var entity in entityList)
            {
                SetProductTracking(entity);
            }
            var result = await _unitOfWork.ProductDAL.UpdateAll(_mapper.Map<List<Product>>(entityList));
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> Delete(long id)
        {
            Product entity = await _unitOfWork.ProductDAL.GetByIdAsync(id);
            var result = await _unitOfWork.ProductDAL.DeleteAsync(entity);
            await _unitOfWork.CompleteAsync();
            return result;
        }
        #endregion

        #region Helper Methods
        private IQueryable<Product> ApplyFilert(IQueryable<Product> productList, ProductSearchDTO searchCriteriaDTO)
        {
            if (searchCriteriaDTO.IsActive.HasValue)
            {
                productList = productList.Where(x => x.IsActive == searchCriteriaDTO.IsActive);
            }

            if (searchCriteriaDTO.CategoryId.HasValue)
            {
                productList = productList.Where(x => x.CategoryId == searchCriteriaDTO.CategoryId);
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Code))
            {
                productList = productList.Where(x => x.Code.Contains(searchCriteriaDTO.Code));
            }

            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Name))
            {
                productList = productList.Where(x => x.Name.Contains(searchCriteriaDTO.Name));
            }

            if (searchCriteriaDTO.IsMinusQuantity.HasValue)
            {
                productList = productList.Where(x => x.ActualQuantity < 0);
            }

            if (searchCriteriaDTO.IsLowQuantity.HasValue)
            {
                productList = productList.Where(x => x.ActualQuantity <= x.LowQuantity);
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

        private string GenerateSequenceNumber()
        {
            var lastElement = _unitOfWork.ProductDAL.GetAllAsync().Result.OrderByDescending(x => x.Id).FirstOrDefault();
            if (lastElement == null)
            {
                return "1000";
            }
            int code = int.Parse(lastElement.Code) + 1;
            return code.ToString();

        }
        #endregion
    }
}
