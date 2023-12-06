using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebMarket.Models.Adapters
{
    public class AdapterProducts : IAdapterProducts
    {
        string connection = ConfigurationManager.ConnectionStrings["MarketDB"].ConnectionString;

        public void Delete(int id)
        {

            using (SqlConnection sql = new SqlConnection(connection))
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sql;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete Товары where id=@id";
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();


            }
        }

        public Product Get(int id)
        {
            Product product = null;
            using (SqlConnection sql = new SqlConnection(connection))
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sql;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Товары where id=@id";
                cmd.Parameters.AddWithValue("id", id);
                SqlDataReader s = cmd.ExecuteReader();
                s.Read();
                product = new Product
                {
                    Id = Convert.ToInt32(s["Id"]),
                    Name = s["Название"].ToString(),
                    Price = Convert.ToDecimal(s["Стоимость_в_рублях_за_ед_товара"]),
                    Description = s["Описание"].ToString(),
                };

            }
            return product;
        }

        public bool Insert(Product item)
        {
            throw new NotImplementedException();
        }

        public void Insert(NewProduct product)
        {
            using (SqlConnection sql = new SqlConnection(connection))
            {

                sql.Open();
            
                SqlCommand cmd = sql.CreateCommand();
                cmd.Connection = sql;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Товары(Название,Описание,Количество,Стоимость_в_рублях_за_ед_товара)values(@f,@n,@p,@t)  ";
                cmd.Parameters.AddWithValue("@f", product.Name);
                cmd.Parameters.AddWithValue("@n", product.Description);
                cmd.Parameters.AddWithValue("@p", product.Count);
                cmd.Parameters.AddWithValue("@t", product.Price);
                
                cmd.ExecuteNonQuery();
                


            }
        }

        public IEnumerable<Product> Select()
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Товары", connection);
            DataSet data = new DataSet();
            sqlDataAdapter.Fill(data, "Товары");
            var res = data.Tables[0].AsEnumerable().Select(x => new Product
            {
                Name = x.Field<string>("Название"),
                Count = x.Field<int>("Количество"),
                Description = x.Field<string>("Описание"),
                Id = x.Field<int>("Id"),
                Price = x.Field<decimal>("Стоимость_в_рублях_за_ед_товара")
            });
            return res;
        }

        public void Update(Product item)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, int count)
        {
            using (SqlConnection sql = new SqlConnection(connection))
            {
                sql.Open();
                SqlCommand cmd = sql.CreateCommand();
                cmd.CommandText = "UPDATE Товары SET Количество=@count where Id=@id ";                
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("count", count);
                cmd.ExecuteNonQuery();
            }
        }
    }
}