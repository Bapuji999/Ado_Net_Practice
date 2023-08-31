using SqlCommandClassADO.Net.DTOs;
using SqlCommandClassADO.Net.Leyars;
using SqlCommandClassADO.Net.Models;
using System.Data;
using System.Data.SqlClient;

namespace SqlCommandClassADO.Net.DataLayers
{
    public class productDataLayer
    {
        DataSet ds = new DataSet();
        DataLeyar ProductDl = new DataLeyar();
        public DataSet GetAllProducts(string connectionName)
        {
            try
            {
                int index = 0;
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[index] = new SqlParameter("@OutMessage", SqlDbType.VarChar, 500);
                parameter[index].Direction = ParameterDirection.Output;
                parameter[index].Value = "";
                ds = ProductDl.GetData("SpGetAllProducts", ref parameter, connectionName);
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public string SaveProduct(string connectionName, ProductDto Product)
        {
            try
            {
                int index = 0;
                SqlParameter[] parameter = new SqlParameter[4];
                parameter[index] = new SqlParameter("@OutMessage", SqlDbType.VarChar, 500);
                parameter[index].Direction = ParameterDirection.Output;
                parameter[index].Value = "";

                index = index + 1;
                parameter[index] = new SqlParameter("@ProductName", SqlDbType.VarChar, 500);
                parameter[index].Value = Product.ProductName;

                index = index + 1;
                parameter[index] = new SqlParameter("@ProductCategory", SqlDbType.VarChar, 500);
                parameter[index].Value = Product.ProductCategory;

                index = index + 1;
                parameter[index] = new SqlParameter("@Price", SqlDbType.Int);
                parameter[index].Value = Product.Price;

                return ProductDl.SaveData("SaveProduct", ref parameter,connectionName);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
