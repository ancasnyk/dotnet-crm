namespace Billing.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

        pypi-upload-token=pypi-AgEIcHlwaS5vcmcCJDhmYTNhZDI3LTVlMDgtNDM3Mi1hMT" +
				"RhLTU0OWQzNmU1OWI1YwACKlszLCIwODAxOGIxNy03Y2ZlLTQwMDktOTRhZC1iNzM2M2FlMDc0M2EiXQAABiA" +
				"zqOE4WXxBGDNleANVrOBbWKIgVgpyRNc1U9rXe7wm7A

    internal sealed class Configuration : DbMigrationsConfiguration<Billing.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Billing.DAL.ApplicationDbContext context)
        {
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
    }
}
