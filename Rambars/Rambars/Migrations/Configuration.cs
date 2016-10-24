namespace Rambars.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Rambars.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Rambars.Models.RambarsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Rambars.Models.RambarsContext context)
        {
            context.Bars.AddOrUpdate(x => x.Id,
                new Bar() { Id = 1, Name = "Barnaby's", AddressLineOne = "15 South High Street", AddressLineTwo = "", City = "West Chester", ZipCode = 19382 },
                new Bar() { Id = 2, Name = "Kildare's Irish Pub", AddressLineOne = "18 West Gay Street", AddressLineTwo = "", City = "West Chester", ZipCode = 19380 },
                new Bar() { Id = 3, Name = "Jake's Bar", AddressLineOne = "549 South Matlack Street", AddressLineTwo = "", City = "West Chester", ZipCode = 19382 },
                new Bar() { Id = 4, Name = "Landmark Americana", AddressLineOne = "158 West Gay Street", AddressLineTwo = "", City = "West Chester", ZipCode = 19380 },
                new Bar() { Id = 5, Name = "Ryan's Pub", AddressLineOne = "124 West Gay Street", AddressLineTwo = "", City = "West Chester", ZipCode = 19380 }
                );

            context.Specials.AddOrUpdate(x => x.Id,
                new Special() { Id = 1, Name = "$2 Fireball!", Price = 2.00M, StartTime = DateTime.Now, EndTime = DateTime.Now, BarId = 1 },
                new Special() { Id = 2, Name = "25% off Sam Adams after 10pm!", Price = 5.00M, StartTime = new DateTime(2016, 10, 23, 10, 0, 0, DateTimeKind.Local), EndTime = new DateTime(2016, 10, 24, 1, 0, 0, DateTimeKind.Local), BarId = 2 },
                new Special() { Id = 3, Name = "$3 Bud and Bud Light 10pm-12am!", Price = 3.00M, StartTime = new DateTime(2016, 10, 24, 10, 0, 0, DateTimeKind.Local), EndTime = new DateTime(2016, 10, 25, 12, 0, 0, DateTimeKind.Local), BarId = 5 },
                new Special() { Id = 4, Name = "$2 You-Call-It's!", Price = 2.00M, StartTime = new DateTime(2016, 10, 24, 10, 0, 0, DateTimeKind.Local), EndTime = new DateTime(2016, 10, 25, 12, 0, 0, DateTimeKind.Local), BarId = 3 }
                );
        }
    }
}
