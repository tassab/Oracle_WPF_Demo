using Oracle.ManagedDataAccess.Client;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Model
{
    public class OracleDB : DbContext
    {
        public OracleDB() : base("name=SchemaOT")
        {
        }
        public OracleDB(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Add(new PropertyConvention());
        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

    }
    public class PropertyConvention : IStoreModelConvention<EdmProperty>
    {
        public void Apply(EdmProperty item, DbModel model)
        {
            item.Name = item.Name.ToUpperInvariant();
        }
    }
}
