using System;
using System.Collections.Generic;

namespace DAL.EF.Table;

public partial class SaleItem
{
    public int SaleItemId { get; set; }

    public int SaleId { get; set; }

    public int MedicineId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual Medicine Medicine { get; set; } = null!;
}
