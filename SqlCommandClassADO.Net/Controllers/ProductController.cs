using Microsoft.AspNetCore.Mvc;
using SqlCommandClassADO.Net.DataLayers;
using System.Data;
using Newtonsoft.Json;
using SqlCommandClassADO.Net.Models;
using SqlCommandClassADO.Net.DTOs;

namespace SqlCommandClassADO.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            DataSet ds = new DataSet();
            productDataLayer dl = new productDataLayer();
            try
            {
                string con = _configuration.GetConnectionString("localConnection");
                ds = dl.GetAllProducts(con);
                if (ds.Tables.Count != 0 )
                {
                    List<Product> products = new List<Product>();
                    for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Product product = new Product();
                        product.Id = int.Parse(ds.Tables[0].Rows[i]["ID"].ToString());
                        product.ProductName = ds.Tables[0].Rows[i]["ProductName"].ToString();
                        product.ProductCategory = ds.Tables[0].Rows[i]["Productcategory"].ToString();
                        product.Price = int.Parse(ds.Tables[0].Rows[i]["Price"].ToString());
                        products.Add(product);
                    }
                    return Ok(JsonConvert.SerializeObject(products));
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, "Data Not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("SaveProduct")]
        public IActionResult SaveProduct([FromBody] ProductDto Product)
        {
            productDataLayer dl = new productDataLayer(); 
            string message = string.Empty;
            try
            {
                string conn = _configuration.GetConnectionString("localConnection");
                message = dl.SaveProduct(conn, Product);
                return Ok(JsonConvert.SerializeObject(message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
