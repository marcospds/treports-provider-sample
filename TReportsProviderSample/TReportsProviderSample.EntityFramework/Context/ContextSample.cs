using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TReportsProviderSampleEntityFramework
{
  public class ContextSample : DbContext
  {
    public virtual DbSet<Country> Country { get; set; }
    public virtual DbSet<Company> Company { get; set; }


    public virtual DbSet<Customer> Customer { get; set; }
    public virtual DbSet<Product> Product { get; set; }
    public virtual DbSet<Orders> Orders { get; set; }
    public virtual DbSet<OrdersProducts> OrdersProducts { get; set; }


    public virtual DbSet<Department> Department { get; set; }
    public virtual DbSet<Employee> Employee { get; set; }
    public virtual DbSet<Dependent> Dependent { get; set; }


    public ContextSample(DbContextOptions<ContextSample> options) : base(options) { }

    // Seed Data
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
      {
        relationship.DeleteBehavior = DeleteBehavior.Restrict;
      }

      var CountryList = JsonConvert.DeserializeObject<List<Country>>(File.ReadAllText(@"Data\Country.json"));
      var CompanyList = JsonConvert.DeserializeObject<List<Company>>(File.ReadAllText(@"Data\Company.json"));

      modelBuilder.Entity<Country>().HasData(CountryList.ToArray());
      modelBuilder.Entity<Company>().HasData(CompanyList.ToArray());

      // Totvs
      var DepartmentTotvsList = JsonConvert.DeserializeObject<List<Department>>(File.ReadAllText(@"Data\Totvs\Department.json"));
      var EmployeeTotvsList = JsonConvert.DeserializeObject<List<Employee>>(File.ReadAllText(@"Data\Totvs\Employee.json"));
      var DependentTotvsList = JsonConvert.DeserializeObject<List<Dependent>>(File.ReadAllText(@"Data\Totvs\Dependent.json"));

      modelBuilder.Entity<Department>().HasData(DepartmentTotvsList.ToArray());
      modelBuilder.Entity<Employee>().HasData(EmployeeTotvsList.ToArray());
      modelBuilder.Entity<Dependent>().HasData(DependentTotvsList.ToArray());

      var CustomerTotvsList = JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText(@"Data\Totvs\Customer.json"));
      var ProductTotvsList = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(@"Data\Totvs\Product.json"));
      var OrdersTotvsList = JsonConvert.DeserializeObject<List<Orders>>(File.ReadAllText(@"Data\Totvs\Orders.json"));
      var OrdersProductsTotvsList = JsonConvert.DeserializeObject<List<OrdersProducts>>(File.ReadAllText(@"Data\Totvs\OrdersProducts.json"));

      modelBuilder.Entity<Customer>().HasData(CustomerTotvsList.ToArray());
      modelBuilder.Entity<Product>().HasData(ProductTotvsList.ToArray());
      modelBuilder.Entity<Orders>().HasData(OrdersTotvsList.ToArray());
      modelBuilder.Entity<OrdersProducts>().HasData(OrdersProductsTotvsList.ToArray());

      // Google
      var DepartmentGoogleList = JsonConvert.DeserializeObject<List<Department>>(File.ReadAllText(@"Data\Google\Department.json"));
      var EmployeeGoogleList = JsonConvert.DeserializeObject<List<Employee>>(File.ReadAllText(@"Data\Google\Employee.json"));
      var DependentGoogleList = JsonConvert.DeserializeObject<List<Dependent>>(File.ReadAllText(@"Data\Google\Dependent.json"));

      modelBuilder.Entity<Department>().HasData(DepartmentGoogleList.ToArray());
      modelBuilder.Entity<Employee>().HasData(EmployeeGoogleList.ToArray());
      modelBuilder.Entity<Dependent>().HasData(DependentGoogleList.ToArray());

      var CustomerGoogleList = JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText(@"Data\Google\Customer.json"));
      var ProductGoogleList = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(@"Data\Google\Product.json"));
      var OrdersGoogleList = JsonConvert.DeserializeObject<List<Orders>>(File.ReadAllText(@"Data\Google\Orders.json"));
      var OrdersProductsGoogleList = JsonConvert.DeserializeObject<List<OrdersProducts>>(File.ReadAllText(@"Data\Google\OrdersProducts.json"));

      modelBuilder.Entity<Customer>().HasData(CustomerGoogleList.ToArray());
      modelBuilder.Entity<Product>().HasData(ProductGoogleList.ToArray());
      modelBuilder.Entity<Orders>().HasData(OrdersGoogleList.ToArray());
      modelBuilder.Entity<OrdersProducts>().HasData(OrdersProductsGoogleList.ToArray());

    }
  }
}
