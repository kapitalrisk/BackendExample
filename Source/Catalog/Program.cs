using Catalog.Models.Entities;
using Catalog.Repositories;
using Catalog.Services;
using InMemoryDatabase;

namespace Catalog
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
            builder.Services.AddInMemoryDatabase();
            builder.Services.AddSwaggerGen();

            // Add services
            builder.Services.AddScoped<ICatalogService, CatalogService>();

            // Add repositories
            builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                // Register databases entities and generate them in memory
                // Obviously in a real world application this should never exist / be done like that
                var dbGenerator = app.Services.GetService<IDatabaseGenerator>();
                dbGenerator.RegisterTableEntity(typeof(CatalogEntryEntity));
                dbGenerator.GenerateDatabaseAsync();

                // Insert random data in db, again never do that here
                var productRepo = app.Services.GetService<IDefaultRepository>();
                productRepo.Execute($"insert into products (name, description) values ('Table', 'Its a table with four legs')");
                productRepo.Execute($"insert into products (name, description) values ('Chair', 'Its a chair with four legs')");
                productRepo.Execute($"insert into products (name, description) values ('DeskLamp', 'Its a desk lamp')");
                productRepo.Execute($"insert into products (name, description) values ('Pencil', 'Its a tool to write stuff')");
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}