﻿using Data.Entities.Setup;
using Data.Entities.Shared;
using Data.Entities.UserManagement;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Accouting
{
    public class Treasury : AuditEntity
    {
        public DateTime Date { get; set; }
        public AccountTypeEnum AccountTypeId { get; set; }
        public long? ClientVendorId { get; set; }
        public string BeneficiaryName { get; set; }
        //public TransactionTypeEnum TransactionTypeId { get; set; }
        public PaymentMethodEnum? PaymentMethodId { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string RefNo { get; set; }
        public string Notes { get; set; }
        public bool IsCancel { get; set; }
        public bool IsBilled { get; set; }
        public virtual ClientVendor ClientVendor { get; set; }

    }
}
