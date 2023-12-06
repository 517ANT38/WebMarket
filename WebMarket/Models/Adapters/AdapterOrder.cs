using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebMarket.Models.Adapters
{
    public class AdapterOrder : IAdapterOrder
    {
        string connection = ConfigurationManager.ConnectionStrings["MarketDB"].ConnectionString;
        public int Insert(OrderWriteDb order)
        {
            int id=0;
            using (SqlConnection sql = new SqlConnection(connection))
            {

                sql.Open();

                SqlCommand cmd = sql.CreateCommand();
                cmd.Connection = sql;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "stored_procedure";
                cmd.Parameters.AddWithValue("@f", order.Id_client);
                cmd.Parameters.AddWithValue("@n", order.DateOrder);
                cmd.Parameters.AddWithValue("@p", order.DateDelivery);
                cmd.Parameters.AddWithValue("@t", order.MethodOfIssue);
                SqlParameter pID = new SqlParameter("ID", SqlDbType.Int);
                pID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pID);
                cmd.ExecuteScalar();
                id=Convert.ToInt32(cmd.Parameters["ID"].Value.ToString());

            }
            return id;
        }

        public void InsertProductInOrder(IList<Product> products, int id_order)
        {
            using (SqlConnection sql = new SqlConnection(connection))
            {
                sql.Open();
                SqlCommand cmd = sql.CreateCommand();
                cmd.Connection = sql;
                cmd.CommandType = CommandType.Text;
                
                int i = 0;
                foreach (var item in products)
                {
                    cmd.CommandText = $"insert into ЗаказанныеТовары (id_товара,id_заказа,Количество_заказанного_товара) values(@tt{i},@g{i},@h{i})";
                    cmd.Parameters.Add(new SqlParameter( $"@tt{i}", item.Id));
                    cmd.Parameters.Add(new SqlParameter($"@g{i}", id_order));
                    cmd.Parameters.Add(new SqlParameter($"@h{i}", item.Count));
                    cmd.ExecuteNonQuery();
                    i++;
                }
                
            }
        }
        public List<Order> Select()
        {
            List<Order> orders = new List<Order>();
            using (SqlConnection sql = new SqlConnection(connection))
            {
                sql.Open();
                SqlCommand cmd = sql.CreateCommand();
                cmd.Connection = sql;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Заказы";
                SqlDataReader reader =cmd.ExecuteReader();
                while (reader.Read())
                {
                    Order order = new Order();
                    order.DateOrder = Convert.ToDateTime(reader["Дата_Заказа"]);
                    order.DateDelivery= Convert.ToDateTime(reader["Дата_Доставки"]);
                    order.MethodOfIssue=reader["Способ_получения_заказа"].ToString();
                    order.Id = Convert.ToInt32(reader["Id"]); 
                    order.Id_client = Convert.ToInt32(reader["id_людей"]);
                    orders.Add(order);
                }

                reader.Close();
                foreach (Order order in orders)
                {
                    cmd.CommandText = "select t.Id,zt.Количество_заказанного_товара,t.Название,t.Описание,t.Стоимость_в_рублях_за_ед_товара from  ЗаказанныеТовары as zt inner join Товары as t on(t.Id=zt.id_товара)  where(zt.id_заказа=@id)";
                    cmd.Parameters.AddWithValue("@id", order.Id);

                    SqlDataReader reader1 = cmd.ExecuteReader();
                    List<Product> products = new List<Product>();
                    while (reader1.Read())
                    {
                        products.Add(new Product
                        {
                            Id = Convert.ToInt32(reader1["Id"]),
                            Name = reader1["Название"].ToString(),
                            Description = reader1["Описание"].ToString(),
                            Price = Convert.ToDecimal(reader1["Стоимость_в_рублях_за_ед_товара"]),
                            Count = Convert.ToInt32(reader1["Количество_заказанного_товара"])
                        });
                    }
                    cmd.Parameters.RemoveAt("@id");
                    order.Products = products;  
                    reader1.Close();
                }
            }
            return orders;    

        }
    }
}