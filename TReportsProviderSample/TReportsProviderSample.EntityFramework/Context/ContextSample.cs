using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

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

      var dataFolder = Path.Combine(Directory.GetCurrentDirectory(), "Data");
      var totvsFolder = Path.Combine(dataFolder, "Totvs");
      var googleFolder = Path.Combine(dataFolder, "Google");

      var CountryList = JsonConvert.DeserializeObject<List<Country>>(File.ReadAllText(Path.Combine(dataFolder,"Country.json"), Encoding.UTF7));
      var CompanyList = JsonConvert.DeserializeObject<List<Company>>(File.ReadAllText(Path.Combine(dataFolder, "Company.json"), Encoding.UTF7));

      modelBuilder.Entity<Country>().HasData(CountryList.ToArray());
      modelBuilder.Entity<Company>().HasData(CompanyList.ToArray());

      // Totvs
      var DepartmentTotvsList = JsonConvert.DeserializeObject<List<Department>>(File.ReadAllText(Path.Combine(totvsFolder, "Department.json"), Encoding.UTF7));
      var EmployeeTotvsList = JsonConvert.DeserializeObject<List<Employee>>(File.ReadAllText(Path.Combine(totvsFolder, "Employee.json"), Encoding.UTF7));
      var EmployeeSalaryHistoryTotvsList = JsonConvert.DeserializeObject<List<EmployeeSalaryHistory>>(File.ReadAllText(Path.Combine(totvsFolder, "EmployeeSalaryHistory.json"), Encoding.UTF7));
      var DependentTotvsList = JsonConvert.DeserializeObject<List<Dependent>>(File.ReadAllText(Path.Combine(totvsFolder, "Dependent.json"), Encoding.UTF7));

      modelBuilder.Entity<Department>().HasData(DepartmentTotvsList.ToArray());
      modelBuilder.Entity<Employee>().HasData(EmployeeTotvsList.ToArray());
      modelBuilder.Entity<EmployeeSalaryHistory>().HasData(EmployeeSalaryHistoryTotvsList.ToArray());
      modelBuilder.Entity<Dependent>().HasData(DependentTotvsList.ToArray());

      var CustomerTotvsList = JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText(Path.Combine(totvsFolder, "Customer.json"), Encoding.UTF7));
      var ProductTotvsList = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(Path.Combine(totvsFolder, "Product.json"), Encoding.UTF7));
      var OrdersTotvsList = JsonConvert.DeserializeObject<List<Orders>>(File.ReadAllText(Path.Combine(totvsFolder, "Orders.json"), Encoding.UTF7));
      var OrdersProductsTotvsList = JsonConvert.DeserializeObject<List<OrdersProducts>>(File.ReadAllText(Path.Combine(totvsFolder, "OrdersProducts.json"), Encoding.UTF7));

      modelBuilder.Entity<Customer>().HasData(CustomerTotvsList.ToArray());
      modelBuilder.Entity<Product>().HasData(ProductTotvsList.ToArray());
      modelBuilder.Entity<Orders>().HasData(OrdersTotvsList.ToArray());
      modelBuilder.Entity<OrdersProducts>().HasData(OrdersProductsTotvsList.ToArray());

      // Google
      var DepartmentGoogleList = JsonConvert.DeserializeObject<List<Department>>(File.ReadAllText(Path.Combine(googleFolder, "Department.json"), Encoding.UTF7));
      var EmployeeGoogleList = JsonConvert.DeserializeObject<List<Employee>>(File.ReadAllText(Path.Combine(googleFolder, "Employee.json"), Encoding.UTF7));
      var EmployeeSalaryHistoryGoogleList = JsonConvert.DeserializeObject<List<EmployeeSalaryHistory>>(File.ReadAllText(Path.Combine(googleFolder, "EmployeeSalaryHistory.json"), Encoding.UTF7));
      var DependentGoogleList = JsonConvert.DeserializeObject<List<Dependent>>(File.ReadAllText(Path.Combine(googleFolder, "Dependent.json"), Encoding.UTF7));

      modelBuilder.Entity<Department>().HasData(DepartmentGoogleList.ToArray());
      modelBuilder.Entity<Employee>().HasData(EmployeeGoogleList.ToArray());
      modelBuilder.Entity<EmployeeSalaryHistory>().HasData(EmployeeSalaryHistoryGoogleList.ToArray());
      modelBuilder.Entity<Dependent>().HasData(DependentGoogleList.ToArray());

      var CustomerGoogleList = JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText(Path.Combine(googleFolder, "Customer.json")));
      var ProductGoogleList = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(Path.Combine(googleFolder, "Product.json")));
      var OrdersGoogleList = JsonConvert.DeserializeObject<List<Orders>>(File.ReadAllText(Path.Combine(googleFolder, "Orders.json")));
      var OrdersProductsGoogleList = JsonConvert.DeserializeObject<List<OrdersProducts>>(File.ReadAllText(Path.Combine(googleFolder, "OrdersProducts.json")));

      modelBuilder.Entity<Customer>().HasData(CustomerGoogleList.ToArray());
      modelBuilder.Entity<Product>().HasData(ProductGoogleList.ToArray());
      modelBuilder.Entity<Orders>().HasData(OrdersGoogleList.ToArray());
      modelBuilder.Entity<OrdersProducts>().HasData(OrdersProductsGoogleList.ToArray());

    }
  }
}
