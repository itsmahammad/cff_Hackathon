namespace CffHackathon.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

     DbSet<MenuItem> MenuItems { get; set; }
     DbSet<Category> Categories { get; set; }
     DbSet<Order> Orders { get; set; }
     DbSet<OrderItem> OrderItems { get; set; }
     DbSet<Table> Tables { get; set; }
     DbSet<Payment> Payments { get; set; }
     DbSet<Reservation> Reservations { get; set; }
}