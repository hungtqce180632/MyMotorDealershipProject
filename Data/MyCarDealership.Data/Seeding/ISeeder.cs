﻿namespace MyMotorDealership.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(MotorDealershipDbContext dbContext, IServiceProvider serviceProvider);
    }
}
