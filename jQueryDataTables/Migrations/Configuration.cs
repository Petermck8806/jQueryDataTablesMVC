using jQueryDataTables.Models;
using System.Data.Entity;

namespace jQueryDataTables.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<jQueryDataTables.Models.ApplicationDbContext>
    {
        private static readonly Random _rng = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "jQueryDataTables.Models.ApplicationDbContext";
        }

        protected override void Seed(jQueryDataTables.Models.ApplicationDbContext context)
        {
            if (!context.Employees.Any())
            {
                for (int i = 0; i < 1000; i++)
                {
                    context.Employees.AddOrUpdate(new Models.Employee
                    {
                        BirthDate = Faker.DateTimeFaker.DateTime(DateTime.Parse("1/1/1950"),DateTime.Parse("1/1/1999")),
                        StartDate = Faker.DateTimeFaker.DateTime(),
                        FirstName = Faker.NameFaker.FirstName(),
                        LastName = Faker.NameFaker.LastName(),
                        MiddleName = RandomString(1, RandomStringType.Alpha),
                        Sex = _rng.Next() % 2 == 0 ? "M" : "F"
                    });
                }
            }
            
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }

        enum RandomStringType
        {
            Alpha,
            Numeric,
            AlphaNumeric
        }

        private static string RandomString(int size, RandomStringType type)
        {
            string chars;

            switch (type)
            {
                case RandomStringType.Alpha:
                    chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    break;
                case RandomStringType.Numeric:
                    chars = "0123456789";
                    break;
                case RandomStringType.AlphaNumeric:
                default:
                    chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    break;
            }

            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = chars[_rng.Next(chars.Length)];
            }
            return new string(buffer);
        }
    }
}