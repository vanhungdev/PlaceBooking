namespace PlaceBooking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("topic")]
    public partial class Topic
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Parentid { get; set; }
        public int Orders { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public int Status { get; set; }
    }
}
