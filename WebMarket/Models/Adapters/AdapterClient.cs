using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace WebMarket.Models.Adapters
{
    public class AdapterClient : IAdapterClient
    {
        string connection = ConfigurationManager.ConnectionStrings["MarketDB"].ConnectionString;
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Client Get(int id)
        {
            throw new NotImplementedException();
        }

        public Client Get(string email)
        {
            Client client = null;
            using (SqlConnection sql = new SqlConnection(connection))
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sql;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Люди where E_mail=@E_mail";
                cmd.Parameters.AddWithValue("@E_mail", email);
                SqlDataReader s = cmd.ExecuteReader();
                s.Read();
                client = new Client
                {
                    Id = Convert.ToInt32(s["Id"]),
                    Name = s["Имя"].ToString(),
                    Family = s["Фамилия"].ToString(),
                    Email = s["E_mail"].ToString(),
                    Role = s["Роль"].ToString(),
                    Phone = s["Телефон"].ToString(),
                    Patronymic = s["Отечество"] == null ? "" : s["Отечество"].ToString(),
                    Password = s["Хэш_пароля"].ToString()

                };

            }
            return client;
        }

        public bool Insert(Client item)
        {
            bool ok = false;
            using (SqlConnection sql = new SqlConnection(connection))
            {

                sql.Open();
                SqlCommand check = sql.CreateCommand();
                check.CommandText = "SELECT * FROM Люди WHERE E_mail like @e";
                check.Parameters.AddWithValue("@e", item.Email);
                object o = check.ExecuteScalar();
                ok = o == null;
                if (o == null)
                {
                    SqlCommand cmd = sql.CreateCommand();
                    cmd.Connection = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into Люди(Фамилия,Имя,Отечество,Телефон,E_mail,Хэш_пароля,Роль)values(@f,@n,@p,@t,@e,@h,@r)  ";
                    cmd.Parameters.AddWithValue("@f", item.Family);
                    cmd.Parameters.AddWithValue("@n", item.Name);
                    cmd.Parameters.AddWithValue("@p", item.Patronymic);
                    cmd.Parameters.AddWithValue("@t", item.Phone);
                    cmd.Parameters.AddWithValue("@e", item.Email);
                    cmd.Parameters.AddWithValue("@h", item.Password);
                    cmd.Parameters.AddWithValue("@r", item.Role);
                    cmd.ExecuteNonQuery();
                }


            }
            return ok;
        }



        public IEnumerable<Client> Select()
        {
            throw new NotImplementedException();
        }

        public void Update(Client item)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, int count)
        {
            throw new NotImplementedException();
        }
    }
}