namespace PlaceBooking.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PlaceBookingDbContext : DbContext
    {
        public PlaceBookingDbContext()
            : base("name=ChuoiKn")
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<BillOrder> BillOrders { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Ordersdetail> Ordersdetails { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<ExtendsBooking> ExtendsBookings { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

    }
}
