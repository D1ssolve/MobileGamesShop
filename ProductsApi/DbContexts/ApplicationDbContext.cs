using Microsoft.EntityFrameworkCore;
using ProductsApi.Models;

namespace ProductsApi.DbContexts;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
           
        modelBuilder.Entity<Product>().HasData(new Product
        {
            ProductId = 1,
            Name = "Grand Theft Auto V",
            Price = 15,
            Description = "Grand Theft Auto V is the world no. 1 mobile game and reimagines the open-world game in a number of ways.",
            CategoryName = "Action"
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            ProductId = 2,
            Name = "Five Nights at Freddy's",
            Price = 13.99,
            Description = "Even though there are successful and the best mobile games 2022 in all verticals, Five Nights at Freddy's: Security Breach dominates it all. It really, really dominates the dopamine rush. This indie game series has the most horrifying and extremely complicated timelines of Freddy.",
            CategoryName = "Horror"
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            ProductId = 3,
            Name = "Genshin Impact",
            Price = 10.99,
            Description = "This most popular game is a large, free exploration RPG that can be played anywhere and on any device. So, you would see a pair of magical twins roaming across universes for no apparent reason (to find each other) when an unknown and powerful deity thinks they are too full of themselves to leap between realities as they like and that this must come to an end.",
            CategoryName = "Action"
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            ProductId = 4,
            Name = "GRID Autosport",
            Price = 15,
            Description = "GRID Autosport is a renowned brand, also named as powerful Funny Cars because they are ordinary and are at the peak of GRID Autosport's drag racing. The sensation of driving one is like an all-out attack on the senses, as drivers attempt to subdue eight thousand horsepower while being pushed at speeds of over 300 miles per hour.",
            CategoryName = "Racing"
        });
    }
}