namespace PlaceBooking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ordersdetail")]
    public partial class Ordersdetail
    {
        [Key]
        public int ID { get; set; }
        public int Orderid { get; set; }
        public int TicketId { get; set; }
    }
}
