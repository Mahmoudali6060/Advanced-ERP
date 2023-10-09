
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Accouting.Shared.DataAccessLayer
{
    public class AccountingSharedDAL : IAccountingSharedDAL
    {
        private UNDbContext _context;

        public AccountingSharedDAL(UNDbContext context)
        {
            this._context = context;
        }

        public async Task<long> UpdateAccountName(long accountId, string accountName)
        {
            try
            {
                //var safes = (from s in _context.Safes
                //             where s.AccountId == accountId
                //             select new Safe
                //             {
                //                 Id = s.Id,
                //                 Date = s.Date,
                //                 Incoming = s.Incoming,
                //                 Outcoming = s.Outcoming,
                //                 RecieptNumber = s.RecieptNumber,
                //                 AccountId = s.AccountId,
                //                 Description = s.Description,
                //                 AccountNameAr = accountName
                //             });
                //_context.Safes.UpdateRange(safes);
                await _context.SaveChangesAsync();
                return accountId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
