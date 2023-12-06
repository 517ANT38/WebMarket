using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebMarket.Models.Adapters
{
    public class CheckAdapterCount : ICheckDataInDB
    {
        string connection = ConfigurationManager.ConnectionStrings["MarketDB"].ConnectionString;

        public bool IsChecked(int? id, params object[] parametrs)
        {
            int count;
            using (SqlConnection sql = new SqlConnection(connection))
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sql;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select Количество from Товары where id=@id";
                cmd.Parameters.AddWithValue("id", id);
                SqlDataReader s = cmd.ExecuteReader();
                s.Read();
                count = (int)s["Количество"];
            }
            
            return   count >= Convert.ToInt32(parametrs[0]);
        }
    }
}