using Shared.Entities.Shared;
using Shared.Enums;
using System;

namespace Shared.Entities.Accouting
{
    public class AccountStatementSearchDTO : Paging
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Date { get; set; }
        public long? ClientVendorId { get; set; }
        public TransactionTypeEnum? TransactionTypeId { get; set; }
        public PaymentMethodEnum? PaymentMethodId { get; set; }
        public string RefNo { get; set; }
        public string BankAccountNo { get; set; }
        public string CheckNo { get; set; }
        public string Notes { get; set; }
        public long? RepresentiveId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
