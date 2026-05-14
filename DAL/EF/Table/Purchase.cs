using System;
using System.Collections.Generic;

namespace DAL.EF.Table;

public partial class Purchase
{
    public int PurchaseId { get; set; }

    public DateOnly PurchaseDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string InvoiceNo { get; set; } = null!;

    public int SupplierId { get; set; }

    public virtual Supplier Supplier { get; set; } = null!;
}
