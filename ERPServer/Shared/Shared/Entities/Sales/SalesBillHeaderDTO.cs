﻿using Data.Entities.Shared;
using Newtonsoft.Json;
using Shared.Entities.Shared;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities.Sales
{
    public class SalesBillHeaderDTO : AuditEntityDTO
    {
        public bool IsActive { get; set; }
        public string Number { get; set; }
        public string Date { get; set; }
        public decimal Total { get; set; }
        public decimal? Profit { get; set; }
        public decimal Discount { get; set; }
        public decimal OtherExpenses { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalAfterVAT { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Paid { get; set; }
        public decimal Remaining { get; set; }
        public bool IsTax { get; set; }
        public bool IsTemp { get; set; }
        public string Notes { get; set; }
        public long? RepresentiveId { get; set; }
        public long ClientVendorId { get; set; }
        public string ClientVendorName { get; set; }
        public long? CompanyId { get; set; }
        public long? AccountStatementId { get; set; }
        public long? TreasuryId { get; set; }
        public bool IsCancel { get; set; }
        public bool IsReturned { get; set; }
        public bool IsNewReturned { get; set; }
        public long? ParentId { get; set; }
        public PaymentMethodEnum? PaymentMethodId { get; set; }
        public string RefNo { get; set; }
        public virtual List<SalesBillDetailDTO> SalesBillDetailList { get; set; }
        public virtual List<SalesBillDetailDTO> RemovedSalesBillDetailList { get; set; }

    }
}
