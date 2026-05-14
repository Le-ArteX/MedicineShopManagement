using System;
using System.Collections.Generic;

namespace DAL.EF.Table;

public partial class Sale
{
    public int SaleId { get; set; }

    public DateOnly SaleDate { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal Discount { get; set; }

    public string InvoiceNo { get; set; } = null!;

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
