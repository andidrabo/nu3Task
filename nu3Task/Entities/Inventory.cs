using System;
using System.Collections.Generic;

#nullable disable

namespace nu3Task.Entities
{
    public partial class Inventory
    {
        public int Id { get; set; }
        public string Handle { get; set; }
        public string Location { get; set; }
        public double Amount { get; set; }
    }
}
