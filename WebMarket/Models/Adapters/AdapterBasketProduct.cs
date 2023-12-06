using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebMarket.Models.Adapters
{
    public class AdapterBasketProduct : IAdapterBasketProduct
    {
        string connection = ConfigurationManager.ConnectionStrings["MarketDB"].ConnectionString;

        public void Delete(string email)
        {
            using (SqlConnection sql = new SqlConnection(connection))
            {
                sql.Open();
                SqlCommand cmd = sql.CreateCommand();
                cmd.CommandText = "DELETE Корзина where Email like @email ";
                cmd.Parameters.AddWithValue("@email", email);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(string email, int id)
        {
            using (SqlConnection sql = new SqlConnection(connection))
            {
                sql.Open();
                SqlCommand cmd = sql.CreateCommand();
                cmd.CommandText = "DELETE Корзина where email like @email and id=@id ";
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Insert(ProductBasket productBasket)
        {
            using (SqlConnection sql = new SqlConnection(connection))
            {

                sql.Open();

                SqlCommand cmd = sql.CreateCommand();
                cmd.Connection = sql;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Корзина (Id,Название,Описание,Количество,Стоимость_в_рублях_за_ед_товара,Email) values(@f,@n,@p,@t,@e,@h)  ";
                cmd.Parameters.AddWithValue("@f", productBasket.Id);
                cmd.Parameters.AddWithValue("@n", productBasket.Name);
                cmd.Parameters.AddWithValue("@p", productBasket.Description);
                cmd.Parameters.AddWithValue("@t", productBasket.Count);
                cmd.Parameters.AddWithValue("@e", productBasket.Price);
                cmd.Parameters.AddWithValue("@h", productBasket.Email);

                cmd.ExecuteNonQuery();

            }
        }
        public void Insert(IList<ProductBasket> productBaskets)
        {
            using (SqlConnection sql = new SqlConnection(connection))
            {

                sql.Open();

                SqlCommand cmd = sql.CreateCommand();
                cmd.Connection = sql;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Корзина (Id,Название,Описание,Количество,Стоимость_в_рублях_за_ед_товара,Email) values(@k,@n,@p,@t,@e,@h)  ";

                foreach (ProductBasket productBasket in productBaskets)
                {
                    cmd.Parameters.AddWithValue("@k", productBasket.Id);
                    cmd.Parameters.AddWithValue("@n", productBasket.Name);
                    cmd.Parameters.AddWithValue("@p", productBasket.Description);
                    cmd.Parameters.AddWithValue("@t", productBasket.Count);
                    cmd.Parameters.AddWithValue("@e", productBasket.Price);
                    cmd.Parameters.AddWithValue("@h", productBasket.Email);
                    cmd.ExecuteNonQuery();
                }


            }
        }

        public void InsertSafe(params ProductBasket[] products)
        {
            using (SqlConnection sql = new SqlConnection(connection))
            {

                sql.Open();

                SqlCommand cmd = sql.CreateCommand();
                cmd.Connection = sql;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "insert_safe";

                foreach (ProductBasket productBasket in products)
                {
                    cmd.Parameters.AddWithValue("@f", productBasket.Id);
                    cmd.Parameters.AddWithValue("@n", productBasket.Name);
                    cmd.Parameters.AddWithValue("@p", productBasket.Description);
                    cmd.Parameters.AddWithValue("@t", productBasket.Count);
                    cmd.Parameters.AddWithValue("@e", productBasket.Price);
                    cmd.Parameters.AddWithValue("@h", productBasket.Email);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }


            }
        }

        public IEnumerable<Product> Select(string email)
        {
            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("select * from Корзина where Email=@email", sqlConnection);
            cmd.Parameters.AddWithValue("email", email);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataSet data = new DataSet();
            sqlDataAdapter.Fill(data, "Корзина");
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

        public void UpdateCount(int id, string email, int count)
        {
            using (SqlConnection sql = new SqlConnection(connection))
            {
                sql.Open();
                SqlCommand cmd = sql.CreateCommand();
                cmd.CommandText = "UPDATE Корзина SET Количество=@count where email like @email and Id=@id ";
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("count", count);
                cmd.ExecuteNonQuery();
            }
        }
    }
}