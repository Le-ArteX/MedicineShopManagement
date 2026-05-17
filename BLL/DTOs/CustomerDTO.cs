using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }

        public string Name { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Address { get; set; } = null!;

    }
}
