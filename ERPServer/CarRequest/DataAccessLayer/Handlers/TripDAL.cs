using Data.Contexts;
using Data.Entities.FleetManagement;
using Data.Entities.Shared;
using FleetManagement.DataAccessLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FleetManagement.DataAccessLayer.Handlers
{
    public class TripDAL : ITripDAL
    {
        private readonly AppDbContext _appDbContext;
        public TripDAL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<long> Add(Trip entity)
        {
            if (entity.CarRequest.CarRequestStatusId != Shared.Enums.CarRequestStatusEnum.Closed || entity.CarRequest.CarRequestStatusId != Shared.Enums.CarRequestStatusEnum.InProgress)
            {
                using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        if (entity.TripStatusId == Shared.Enums.TripStatusEnum.Canceled)
                        {
                            entity.ActualStartTime = DateTime.Now;
                            entity.ActualEndTime = DateTime.Now;

                        }
                        var request = _appDbContext.CarRequests.Where(c => c.Id == entity.CarRequestId).FirstOrDefault();
                        request.CarRequestStatusId = Shared.Enums.CarRequestStatusEnum.Closed;
                        _appDbContext.Entry(request).State = EntityState.Modified;
                        _appDbContext.Entry(entity).State = EntityState.Added;
                        await _appDbContext.SaveChangesAsync();
                        ts.Complete();
                    }
                    catch
                    {
                        ts.Dispose();
                        throw new Exception("Errors.InvalidData");
                    }

                }
                return entity.Id;
            }
            else
            {
                throw new Exception("Errors.InvalidData");
            }

        }

        public Task<bool> Delete(Trip entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<Trip>> GetAll()
        {
            return _appDbContext.Trips.Include(t=>t.CarRequest).ThenInclude(t=>t.Driver).OrderByDescending(x => x.Id).AsQueryable();
        }

        public Task<IQueryable<Trip>> GetAllLite()
        {
            throw new NotImplementedException();
        }

        public Task<Trip> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<long> Update(Trip entity)
        {
            throw new NotImplementedException();
        }
    }
}
