using EventBooking.Modal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBooking.Data
{
    public class AuthenticationContext : DbContext
    {
        public AuthenticationContext()
        {

        }

        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
        {

        }

        public virtual DbSet<DBBookingstatusResponse> BookingstatusResponse { get; set; }
        public virtual DbSet<AddEvent> AddEvents { get; set; }

        public virtual DbSet<PendingApproval> PendingApprovals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=DESKTOP-OOV8U7V;Database=TicketBooking;Trusted_Connection=True;");
                optionsBuilder.UseSqlServer("Data Source=tcp:eventbookingdb.database.windows.net,1433;Initial Catalog=EventBooking_db;User Id=karthik@eventbookingdb;Password=Chaitra@1894");
            }
        }
    }
}
