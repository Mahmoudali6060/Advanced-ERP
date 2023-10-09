using AutoMapper;
using Data.Entities;
using Accouting.Shared.DataAccessLayer;
using Microsoft.AspNetCore.Identity;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shared.Entities.Shared;
using Shared.Classes;
using Shared.Enums;

namespace Accouting.Shared.DataServiceLayer
{
    public class AccountingSharedDSL : IAccountingSharedDSL
    {
        bool _ISEDITMODE = false;
        IAccountingSharedDAL _accountingSharedDAL;
        private readonly IMapper _mapper;
        public AccountingSharedDSL(IAccountingSharedDAL accountingSharedDAL, IMapper mapper)
        {
            _accountingSharedDAL = accountingSharedDAL;
            _mapper = mapper;
        }

        public Task<long> UpdateAccountName(long accountId, string accountName)
        {
            return _accountingSharedDAL.UpdateAccountName(accountId, accountName);
        }
    }
}
