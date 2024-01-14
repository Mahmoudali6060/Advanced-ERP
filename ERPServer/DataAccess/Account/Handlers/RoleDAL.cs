﻿using Data.Contexts;
using Data.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Shared.Entities.Shared;
using System.Linq;
using System.Threading.Tasks;

namespace Account.DataAccessLayer
{
    public class RoleDAL : IRoleDAL
    {
        private readonly AppDbContext _appDbContext;
        public RoleDAL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IQueryable<RoleGroup>> GetAllAsync()
        {
            return _appDbContext.RoleGroups.Include(x => x.RolePrivileges).AsQueryable();
        }

        public async Task<IQueryable<RoleGroup>> GetAllLiteAsync()
        {
            return _appDbContext.RoleGroups.AsQueryable();
        }

        public async Task<RoleGroup> GetByIdAsync(long id)
        {
            var Role = _appDbContext.RoleGroups.Include(x => x.RolePrivileges).SingleOrDefaultAsync(x => x.Id == id);
            return await Role;
        }


        public async Task<long> AddAsync(RoleGroup entity)
        {
            await _appDbContext.AddAsync(entity);
            return entity.Id;
        }

        public async Task<long> UpdateAsync(RoleGroup entity)
        {
            var privileges = _appDbContext.RolePrivileges.Where(x => x.RoleGroupId == entity.Id).ToList();
            if (privileges != null && privileges.Count()>0) _appDbContext.RolePrivileges.RemoveRange(privileges);
            _appDbContext.RolePrivileges.AddRange(entity.RolePrivileges);
            _appDbContext.RoleGroups.Update(entity);
            return entity.Id;
        }

        public long UpdateSync(RoleGroup entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(RoleGroup roleGroup)
        {
            _appDbContext.RoleGroups.Remove(roleGroup);
            return true;
        }


    }
}
