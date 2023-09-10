using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace PlaceBooking.Models
{
    [Table("role")]
    public partial class Role
    {
        [Key]
        public int ID { get; set; }
        public int ParentId { get; set; }
        public string AccessName { get; set; }
        public string Description { get; set; }
        public string GropID { get; set; }
    }
}
