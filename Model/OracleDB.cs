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
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }

    }
    public class PropertyConvention : IStoreModelConvention<EdmProperty>
    {
        public void Apply(EdmProperty item, DbModel model)
        {
            item.Name = item.Name.ToUpperInvariant();
        }
    }
}
