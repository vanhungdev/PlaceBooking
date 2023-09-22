namespace PlaceBooking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExtendsBooking")]
    public partial class ExtendsBooking
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        public string Details { get; set; }

        public int? RoomId { get; set; }

        public int? Type { get; set; }

        public int? Status { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }
        public double Price { get; set; } = 0;
    }
}
