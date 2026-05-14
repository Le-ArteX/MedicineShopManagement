using System;
using System.Collections.Generic;

namespace DAL.EF.Table;

public partial class PurchaseItem
{
    public int PurchaseItemId { get; set; }

    public int PurchaseId { get; set; }

    public int MedicineId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitCost { get; set; }

    public virtual Medicine Medicine { get; set; } = null!;
}
