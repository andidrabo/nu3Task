using System;
using System.Collections.Generic;

#nullable disable

namespace nu3Task.Entities
{
    public partial class Image
    {
        public int Id { get; set; }
        public long ImageId { get; set; }
        public long ProductId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Src { get; set; }
    }
}
