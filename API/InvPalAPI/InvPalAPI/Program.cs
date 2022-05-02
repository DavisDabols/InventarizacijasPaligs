using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
var app = builder.Build();

//GET warehouses
app.MapGet("/warehouseitems/userId/{userId}", async (string userId, ApplicationDbContext db) =>
    await db.Warehouses.Where(x => x.UserId == userId).ToListAsync());

//GET workers
app.MapGet("/workeritems/email/{email}/password/{password}", async (string email, string password, ApplicationDbContext db) => 
{
    IEnumerable<Worker> workers = await db.Workers.Where(w => w.Email.Equals(email)).ToListAsync();
    List<Worker> workersList = new List<Worker>();
    foreach (Worker worker in workers)
    {
        if(BCrypt.Net.BCrypt.Verify(password, worker.Password) == true)
        {
            workersList.Add(worker);
        }
    }
    if(workersList.Count() == 0)
    {
        throw new Exception("Profile not found");
    }
    if (workersList.Count() > 1)
    {
        throw new Exception("Multiple profiles found");
    }
    return workersList.First();
});

//GET items
app.MapGet("/itemsitems/warehouseId/{warehouseId}", async (Guid warehouseId, ApplicationDbContext db) =>
    await db.Items.Where(x => x.WarehouseId == warehouseId).ToListAsync());

//POST items
app.MapPost("/itemsitems", async (Items item, ApplicationDbContext db) =>
{
    var warehouse = await db.Warehouses.FindAsync(item.WarehouseId);
    warehouse.Capacity++;
    item.Id = Guid.NewGuid();
    db.Items.Add(item);
    await db.SaveChangesAsync();

    return Results.Created($"/itemsitems/{item.Id}", item);
});

//DELETE items
app.MapDelete("/itemsitems/itemId/{Id}", async (Guid Id, ApplicationDbContext db) =>
{
    if (await db.Items.FindAsync(Id) is Items item)
    {
        var warehouse = await db.Warehouses.FindAsync(item.WarehouseId);
        warehouse.Capacity--;
        db.Items.Remove(item);
        await db.SaveChangesAsync();
        return Results.Ok(item);
    }

    return Results.NotFound();
});

//PUT items
app.MapPut("/itemsitems/itemId/{Id}", async (Guid Id, Items inputItem, ApplicationDbContext db) =>
{
    var item = await db.Items.FindAsync(Id);

    if (item is null) return Results.NotFound();

    item.Name = inputItem.Name;
    item.Description = inputItem.Description;
    if (item.WarehouseId != inputItem.WarehouseId)
    {
        var warehouseoutput = await db.Warehouses.FindAsync(item.WarehouseId);
        var warehouseinput = await db.Warehouses.FindAsync(inputItem.WarehouseId);
        warehouseinput.Capacity++;
        warehouseoutput.Capacity--;
        item.WarehouseId = inputItem.WarehouseId;
    }

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();

class Warehouse
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Address { get; set; }
    public int Capacity { get; set; }
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    public string UserId { get; set; }
}

class Worker
{
    [Key]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
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
    public string? Description { get; set; }
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