using System;
using System.Collections.Generic;

namespace DAL.EF.Table;

public partial class Stock
{
    public int StockId { get; set; }

    public int MedicineId { get; set; }

    public string AlertType { get; set; } = null!;

    public string Message { get; set; } = null!;

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Medicine Medicine { get; set; } = null!;
}
