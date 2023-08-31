using System.Data;
using System.Data.SqlClient;

namespace SqlCommandClassADO.Net.Leyars
{
    public class DataLeyar
    {
        public DataSet GetData(string SpName, ref SqlParameter[] param, string Connectionstring)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlConnection? con = null;
                using(con = new SqlConnection(Connectionstring))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = SpName;
                    cmd.CommandTimeout = 120000;
                    cmd.Parameters.Clear();
                    foreach (SqlParameter p in param)
                        cmd.Parameters.Add(p);
                    SqlDataAdapter Adp = new SqlDataAdapter();
                    Adp.SelectCommand = cmd;
                    Adp.Fill(ds);
                    cmd.Dispose();
                    Adp.Dispose();
                }
                return ds;
            }
            catch(Exception ex)
            {
                DataSet ds = new DataSet();
                return ds;
            }
        }
        public string SaveData(string SpName, ref SqlParameter[] param, string Connectionstring)
        {
            try
            {
                SqlConnection? con = null;
                using (con = new SqlConnection(Connectionstring))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = SpName;
                    cmd.CommandTimeout = 120000;
                    cmd.Parameters.Clear();
                    foreach (SqlParameter p in param)
                        cmd.Parameters.Add(p);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                return param[0].Value.ToString();
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
