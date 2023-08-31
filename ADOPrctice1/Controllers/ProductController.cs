using Microsoft.AspNetCore.Mvc;
using Prcatice1.Models;
using System.Data;
using System.Data.SqlClient;

namespace Prcatice1.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetAllProduct")]
        public List<Product> GetAllProduct()
        {
            List<Product> products = new List<Product>();
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("select * from tbl_Product", sqlConnection);
            SqlDataAdapter Da = new SqlDataAdapter(cmd);
            DataTable Dt = new DataTable();
            Da.Fill(Dt);
            for(int i = 0; i < Dt.Rows.Count; i++)
            {
                Product product = new Product();
                product.Id = int.Parse(Dt.Rows[i]["ID"].ToString());
                product.ProductName = Dt.Rows[i]["ProductName"].ToString();
                product.Productcategory = Dt.Rows[i]["Productcategory"].ToString();
                product.Price = int.Parse(Dt.Rows[i]["Price"].ToString());
                products.Add(product);
            }
            return products;
        }

        [HttpPost]
        [Route("SaveProdut")]
        public string SaveProdut(Product Obj) 
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            string Qry = "INSERT INTO tbl_Product(ProductName, Productcategory, Price) VALUES('"+ Obj.ProductName + "','" + Obj.Productcategory + "','" + Obj.Price + "')";
            SqlCommand cmd = new SqlCommand(Qry, sqlConnection);
            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return "Product Saved Sucsessfully";
        }
    }
}
