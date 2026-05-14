using System;
using System.Collections.Generic;

namespace DAL.EF.Table;

public partial class Medicine
{
    public int MedicineId { get; set; }

    public string Name { get; set; } = null!;

    public string GenericName { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public int StockQuantity { get; set; }

    public int MinStockLevel { get; set; }

    public DateOnly ExpiryDate { get; set; }

    public int CategoryId { get; set; }

    public int SupplierId { get; set; }

    public virtual ICollection<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();

    public virtual ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    public virtual Supplier Supplier { get; set; } = null!;
}
