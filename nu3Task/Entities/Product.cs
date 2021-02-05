using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace nu3Task.Entities
{
    public partial class Product
    {
        public int Id { get; set; }
        public long ProductId { get; set; }
        public string Title { get; set; }
        public string BodyHtml { get; set; }
        public string Vendor { get; set; }
        public string ProductType { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Handle { get; set; }
        public string PublishedScope { get; set; }
        public string Tags { get; set; }

        [NotMapped]
        public string ImgSrc { get; set; }
    }
}
