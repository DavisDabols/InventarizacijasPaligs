using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
var app = builder.Build();

app.MapGet("/warehouseitems", async (ApplicationDbContext db) =>
    await db.Warehouses.ToListAsync());

app.MapGet("/workeritems", async (ApplicationDbContext db) =>
    await db.Workers.ToListAsync());

app.MapGet("/itemsitems", async (ApplicationDbContext db) =>
    await db.Items.ToListAsync());

app.Run();

class Warehouse
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Address { get; set; }
    public int MaxCapacity { get; set; }
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    public string UserId { get; set; }
}

class Worker
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public string UserId { get; set; }
}

public class Items
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    public Guid WarehouseId { get; set; }
    public string UserId { get; set; }
}

class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Items> Items { get; set; }
}