namespace MyMotorDealership.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(MotorDealershipDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categoriesToSeed = new List<Category>()
            {
                new Category() { Name = "Saloon"},
                new Category() { Name = "Estate Motor"},
                new Category() { Name = "SUV/Crossover/Off-road Motor"},
                new Category() { Name = "Small Motor" },
                new Category() { Name = "Sports Motor/Coupe" },
                new Category() { Name = "Cabriolet/Roadster" },
                new Category() { Name = "Van/Minibus" },
                new Category() { Name = "Other" },
            };

            await dbContext.Categories.AddRangeAsync(categoriesToSeed);
            await dbContext.SaveChangesAsync();
        }
    }
}