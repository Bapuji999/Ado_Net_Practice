using System.Data.SqlClient;
using System.Data;

namespace ConnectionPracticeInADO.Net
{
    public class DataLayer
    {
        private readonly IConfiguration _configuration;

        public DataLayer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Getdata()
        {
            try
            {
                SqlConnection conn = null;
                string msg = "";
                using (conn = new SqlConnection(_configuration.GetConnectionString("LocalConnection")))
                {
                    conn.Open();
                    if(conn.State == ConnectionState.Open)
                    {
                         msg = msg + "Connected";
                    }
                    else
                    {
                        return "not Open";
                    }
                }
                if (conn.State == ConnectionState.Open)
                {
                    msg = msg + " Still Opend";
                }
                else
                {
                    msg = msg + " and Closed";
                }
                return msg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
