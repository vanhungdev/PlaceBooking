namespace PlaceBooking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        public int ID { get; set; }

        [StringLength(200)]
        public string UserName { get; set; }

        public int? UserId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [StringLength(1000)]
        public string Comment1 { get; set; }

        public int? ParentID { get; set; }

        public int? Status { get; set; }

        [StringLength(500)]
        public string Image { get; set; }

        public int? Type { get; set; }

        public int? TopicId { get; set; }
    }
}
