namespace Homework8
{
    using Homework6;
    using System.Data.Entity;
    using System.Linq;
    using MySql.Data.EntityFramework;
    using System.Data.Entity;
    [DbConfigurationType(typeof(MySqlEFConfiguration))]

    public class OrderDBContext : DbContext
    {

        public OrderDBContext()
            : base("name=OrderDB")
        {
            Database.SetInitializer(
                new DropCreateDatabaseIfModelChanges<OrderDBContext>());
        }

        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Orders> Orders { get; set; }

    }

}