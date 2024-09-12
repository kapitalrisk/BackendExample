using InMemoryDatabase;
using Stock.Models.Entities;
using Stock.Repositories;
using Stock.Services;

namespace Stock
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            // Service DI
            builder.Services.AddStockServices();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                // Register databases entities and generate them in memory
                // Obviously in a real world application this should never exist / be done like that
                var dbGenerator = app.Services.GetService<IDatabaseGenerator>();
                dbGenerator.RegisterTableEntity(typeof(ProductEntity));
                dbGenerator.GenerateDatabaseAsync();

                // Insert random data in db, again never do that here
                var productRepo = app.Services.GetService<IDefaultRepository>();
                productRepo.Execute($"insert into products (id, name, description, number_in_stock) values (1, 'Table', 'Its a table with four legs', 10)");
                productRepo.Execute($"insert into products (id, name, description, number_in_stock) values (2, 'Chair', 'Its a chair with four legs', 40)");
                productRepo.Execute($"insert into products (id, name, description, number_in_stock) values (3, 'DeskLamp', 'Its a desk lamp', 20)");
                productRepo.Execute($"insert into products (id, name, description, number_in_stock) values (4, 'Pencil', 'Its a tool to write stuff', 2)");
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}