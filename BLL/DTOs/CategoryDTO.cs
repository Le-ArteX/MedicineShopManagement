using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class CategoryDTO
    {

        public int CategoryId { get; set; }

        public string Name { get; set; } = null!;

        public string Describe { get; set; } = null!;

    }
}
