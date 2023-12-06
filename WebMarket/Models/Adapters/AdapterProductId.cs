using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebMarket.Models.Adapters
{
    public class AdapterProductId : IAdapter<int>
    {
        string connection = ConfigurationManager.ConnectionStrings["MarketDB"].ConnectionString;

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Get(int id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(int item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> Select()
        {
            IList<int> list = new List<int>();
            using (SqlConnection sql = new SqlConnection(connection))
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand("select id from Товары", sql);

                SqlDataReader s = cmd.ExecuteReader();
                while (s.Read())
                {
                    list.Add(s.GetInt32(0));
                }


            }
            return list;
        }

        public void Update(int item)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, int count)
        {
            throw new NotImplementedException();
        }
    }
}