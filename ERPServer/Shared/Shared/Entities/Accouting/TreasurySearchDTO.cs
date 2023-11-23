using Shared.Entities.Shared;
using Shared.Enums;
using System;

namespace Shared.Entities.Accouting
{
    public class TreasurySearchDTO : Paging
    {
        public string Date { get; set; }
        public AccountTypeEnum? AccountTypeId { get; set; }
        public long? ClientVendorId { get; set; }
        public TransactionTypeEnum? TransactionTypeId { get; set; }
        public PaymentMethodEnum? PaymentMethodId { get; set; }
        public string RefNo { get; set; }
        public string BankAccountNo { get; set; }
        public string CheckNo { get; set; }
        public string Notes { get; set; }
    }
}
