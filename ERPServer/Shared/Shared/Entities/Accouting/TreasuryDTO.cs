using Shared.Entities.Shared;
using Shared.Enums;

namespace Shared.Entities.Accouting
{
    public class TreasuryDTO : BaseDTO
    {
        public string Date { get; set; }
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

    }
}
