using AutoMapper;
using FleetManagement.DataAccessLayer.Contracts;
using FleetManagement.DataServiceLayer.Contracts;
using FleetManagement.Entities;
using Infrastructure.Contracts;
using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Entities.FleetManagement;
using System.Linq;

namespace FleetManagement.DataServiceLayer.Handlers
{
    public class TripDSL : ITripDSL
    {
        private readonly ITripDAL _tripDAL;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        public TripDSL(ITripDAL tripDAL, IFileManager fileManager, IMapper mapper)
        {
            _tripDAL = tripDAL;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        public async Task<long> Add(TripDTO entity)
        {
            //if (entity.TripClosingReasonId == 0)
            //{
            //    if(entity.ActualStartTime == null || entity.ActualEndTime == null || entity.PreviousKiloMeterCounter == 0
            //       || entity.CurrentKiloMeterCounter == 0 || entity.TripStatusId == 0)
            //    throw new Exception("Errors.InvalidData");
            //}
            //else
            //{
            //    entity.TripStatusId = 0;
            //}
            var Trip =  await _tripDAL.Add(_mapper.Map<Trip>(entity));
            //if (Trip != 0)
            //{
            //    UploadImage(entity);
            //}
            return Trip;
        }

        public Task<bool> Delete(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseEntityList<TripDTO>> GetAll(TripSearchCriteriaDTO searchCrieria)
        {
            TripDTO tripDTO = new TripDTO();
            var tripList = await _tripDAL.GetAll();

            #region Apply Filters
            tripList = ApplyFilert(tripList, searchCrieria);
            #endregion

            int total = tripList.Count();

            #region Order
            tripList = tripList.OrderByDescending(x => x.Id);
            #endregion

            #region Apply Pagination
            tripList = tripList.Skip((searchCrieria.Page - 1) * searchCrieria.PageSize).Take(searchCrieria.PageSize);
            #endregion


            #region Mapping and Return List
            List<TripDTO> tripDTOList = _mapper.Map<List<TripDTO>>(tripList);
            return new ResponseEntityList<TripDTO>
            {
                List = tripDTOList,
                Total = total
            };
            #endregion
        }
        private IQueryable<Trip> ApplyFilert(IQueryable<Trip> tripList, TripSearchCriteriaDTO searchCrieria)
        {
            //Filter by UserProfileId
            if (!string.IsNullOrWhiteSpace(searchCrieria.SequenceNumber ))
            {
                tripList = tripList.Where(x => x.CarRequest.SequenceNumber == searchCrieria.SequenceNumber);
            }
            if (searchCrieria.DriverId > 0)
            {
                tripList = tripList.Where(x => x.CarRequest.DriverId == searchCrieria.DriverId);
            }
            if (searchCrieria.TripStatusId > 0)
            {
                tripList = tripList.Where(x => x.TripStatusId == searchCrieria.TripStatusId);
            }
            if (searchCrieria.PreviousKiloMeterCounter > 0)
            {
                tripList = tripList.Where(x => x.PreviousKiloMeterCounter == searchCrieria.PreviousKiloMeterCounter);
            }
            if (searchCrieria.CurrentKiloMeterCounter > 0)
            {
                tripList = tripList.Where(x => x.CurrentKiloMeterCounter == searchCrieria.CurrentKiloMeterCounter);
            }
            if (!string.IsNullOrWhiteSpace(searchCrieria.ActualStartTime))
            {
                tripList = tripList.Where(x => x.ActualStartTime >= DateTime.Parse(searchCrieria.ActualStartTime));
            }

            if (!string.IsNullOrWhiteSpace(searchCrieria.ActualEndTime))
            {
                tripList = tripList.Where(x => x.ActualEndTime <= DateTime.Parse(searchCrieria.ActualEndTime));
            }

            if (searchCrieria.TripClosingReasonId > 0)
            {
                tripList = tripList.Where(x => x.TripClosingReasonId == searchCrieria.TripClosingReasonId);
            }


            return tripList;
        }

        public Task<ResponseEntityList<TripDTO>> GetAllLite()
        {
            throw new NotImplementedException();
        }

        public Task<TripDTO> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<long> Update(TripDTO entity)
        {
            throw new NotImplementedException();
        }
        #region Helper Methods
        //private bool UploadImage(TripDTO entity)
        //{
        //    if (entity.ImageBase64 != null)
        //    {
        //        entity.PictureUrl = string.IsNullOrWhiteSpace(entity.ImageBase64) ? null : entity.CarRequestId + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_ss") + ".jpg";
        //        return _fileManager.UploadImageBase64("wwwroot/Images/Trips/" + entity.PictureUrl, entity.ImageBase64);
        //    }
        //    return true;
        //}
        #endregion
    }
}
