using Data.Constants;
using Data.Contexts;
using Data.Entities;
using Data.Entities.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.Entities;
using Shared.Entities.Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Setting.DataAccessLayer
{
    public class SettingDAL : ISettingDAL
    {
        private AppDbContext _context;
        private DbSet<Settings> _entity;
        public SettingDAL(AppDbContext context)
        {
            _context = context;
            _entity = _context.Set<Settings>();
        }

        public IEnumerable<Settings> GetSettings()
        {
            return _context.Settings;
        }


    }
}
