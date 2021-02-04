using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace nu3Task.Entities
{
    public partial class Inventory
    {
        public int Id { get; set; }
        public string Handle { get; set; }
        public string Location { get; set; }
        public double Amount { get; set; }

        [NotMapped]
        public string Title { get; set; }
    }
}
