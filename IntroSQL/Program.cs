using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace IntroSQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);
 
             DapperDepartmentRepository instance = new DapperDepartmentRepository(conn);

             instance.InsertDepartment("toy");


             IEnumerable<Department> allDepartments = instance.GetAllDepartments();
            foreach (Department department in allDepartments)
            {
                Console.WriteLine(department.Name );
                Console.WriteLine(department.DepartmentID);
                Console.WriteLine();
            }

            DapperProductRepository instance2 = new DapperProductRepository(conn);
            instance2.CreateProduct("Pepperoni Pizza", 5.99, 10);

            Product productToUpdate = instance2.GetProduct(940);
            productToUpdate.Price = 10.99;
            productToUpdate.StockLevel = 10;
            productToUpdate.OnSale = false;

            instance2.UpdateProduct(productToUpdate);

            IEnumerable<Product> allProduct = instance2.GetAllProducts();
            foreach(Product product in allProduct)
            {
                Console.WriteLine(product.Name);

            }

            instance2.DeleteProduct(882);
        }
    }
}
