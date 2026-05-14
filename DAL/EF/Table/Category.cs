using System;
using System.Collections.Generic;

namespace DAL.EF.Table;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string Describe { get; set; } = null!;
}
