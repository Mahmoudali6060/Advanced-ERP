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
        public TransactionTypeEnum TransactionTypeId { get; set; }
        public PaymentMethodEnum PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
        public string RefNo { get; set; }
        public string BankAccountNo { get; set; }
        public string CheckNo { get; set; }
        public string Notes { get; set; }

    }
}
