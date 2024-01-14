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
    public class ContactUsDSL : IContactUsDSL
    {
        private readonly IUnitOfWork _unitOfWork;
        public ContactUsDSL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<long> Add(ContactUs entity)
        {
            return await _unitOfWork.ContactUsDAL.AddAsync(entity);
        }

        public async Task<bool> Delete(long id)
        {
            ContactUs contactUs = await _unitOfWork.ContactUsDAL.GetByIdAsync(id);
            return await _unitOfWork.ContactUsDAL.DeleteAsync(contactUs);
        }

        public async Task<ResponseEntityList<ContactUs>> GetAll(ContactUsSearch searchCrieria)
        {
            var ContactUsList = await _unitOfWork.ContactUsDAL.GetAllAsync();
            int total = ContactUsList.Count();

            #region Apply Filters
            ContactUsList = ApplyFilert(ContactUsList, searchCrieria);
            #endregion

            #region Apply Pagination
            ContactUsList = ContactUsList.Skip((searchCrieria.Page - 1) * searchCrieria.PageSize).Take(searchCrieria.PageSize);
            #endregion

            #region Mapping and Return List
            return new ResponseEntityList<ContactUs>
            {
                List = ContactUsList,
                Total = total
            };
            #endregion
        }


        private IQueryable<ContactUs> ApplyFilert(IQueryable<ContactUs> ContactUsList, ContactUsSearch searchCriteriaDTO)
        {
            //Filter
            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Name))
            {
                ContactUsList = ContactUsList.Where(x => x.Name.Contains(searchCriteriaDTO.Name));
            }
            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Mobile))
            {
                ContactUsList = ContactUsList.Where(x => x.Mobile.Contains(searchCriteriaDTO.Mobile));
            }
            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Email))
            {
                ContactUsList = ContactUsList.Where(x => x.Email.Contains(searchCriteriaDTO.Email));
            }
            if (!string.IsNullOrWhiteSpace(searchCriteriaDTO.Location))
            {
                ContactUsList = ContactUsList.Where(x => x.Location.Contains(searchCriteriaDTO.Location));
            }
            return ContactUsList;
        }

        public async Task<ResponseEntityList<ContactUs>> GetAllLite()
        {
            return new ResponseEntityList<ContactUs>()
            {
                List = _unitOfWork.ContactUsDAL.GetAllLiteAsync().Result,
                Total = _unitOfWork.ContactUsDAL.GetAllLiteAsync().Result.Count()
            };
        }

        public async Task<ContactUs> GetById(long id)
        {
            return await _unitOfWork.ContactUsDAL.GetByIdAsync(id);
        }

        public async Task<long> Update(ContactUs entity)
        {
            return await _unitOfWork.ContactUsDAL.UpdateAsync(entity);
        }
    }
}