using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using SportShop.Persistence.Entities;
using SportShop.Persistence.Repositories;

namespace SportShop.Persistence
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Manufacturers.Any())
            {
                context.Manufacturers.AddRange(
                    new Manufacturer()
                    {
                        Name = "Firma sportowa JASIEK",
                        Country = "Poland"
                    },
                    new Manufacturer()
                    {
                        Name = "Sklep piłkarski",
                        Country = "Germany"                        
                    },
                    new Manufacturer()
                    {
                        Name = "Sklep narciarski- Grzgorz Brzęczyszczykiewicz",
                        Country = "Sweden"
                    },
                    new Manufacturer()
                    {
                        Name = "Baseny i ogrody Jana Pawła",
                        Country = "USA"
                    },
                    new Manufacturer()
                    {
                        Name = "Boiska koszykarskie Eugeniusza",
                        Country = "Russia"
                    });
            }
            
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Kajak",
                        Description = "Łódka przeznaczona dla jednej osoby",
                        Category = "Sporty wodne",
                        Price = 275,
                        ManufacturerId = 1
                    },
                    new Product
                    {
                        Name = "Kamizelka ratunkowa",
                        Description = "Chroni i dodaje uroku",
                        Category = "Sporty wodne",
                        Price = 48.95m,
                        ManufacturerId = 1

                    },
                    new Product
                    {
                        Name = "Piłka",
                        Description = "Zatwierdzone przez FIFA rozmiar i waga",
                        Category = "Piłka nożna",
                        Price = 19.50m,
                        ManufacturerId = 2

                    },
                    new Product
                    {
                        Name = "Flagi narożne",
                        Description = "Nadadzą twojemu boisku profesjonalny wygląd",
                        Category = "Piłka nożna",
                        Price = 34.95m,
                        ManufacturerId = 2

                    },
                    new Product
                    {
                        Name = "Stadiom",
                        Description = "Składany stadion na 35 000 osób",
                        Category = "Piłka nożna",
                        Price = 79500,
                        ManufacturerId = 2
                    },
                    new Product
                    {
                        Name = "Czapka",
                        Description = "Zwiększa efektywność mózgu o 75%",
                        Category = "Szachy",
                        Price = 16,
                        ManufacturerId = 3
                    },
                    new Product
                    {
                        Name = "Niestabilne krzesło",
                        Description = "Zmniejsza szanse przeciwnika",
                        Category = "Szachy",
                        Price = 29.95m,
                        ManufacturerId = 3
                    },
                    new Product
                    {
                        Name = "Ludzka szachownica",
                        Description = "Przyjemna gra dla całej rodziny!",
                        Category = "Szachy",
                        Price = 75,
                        ManufacturerId = 3
                    },
                    new Product
                    {
                        Name = "Błyszczący król",
                        Description = "Figura pokryta złotem i wysadzana diamentami",
                        Category = "Szachy",
                        Price = 1200,
                        ManufacturerId = 3
                    }
                );
            }
            


            context.SaveChanges();
        }
    }
}