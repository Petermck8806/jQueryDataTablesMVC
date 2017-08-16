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
                        BirthDate = DateTime.Now.AddYears(-30),
                        StartDate = DateTime.Now,
                        FirstName = RandomString(7, RandomStringType.Alpha),
                        LastName = RandomString(10, RandomStringType.Alpha),
                        MiddleName = RandomString(1, RandomStringType.Alpha)
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