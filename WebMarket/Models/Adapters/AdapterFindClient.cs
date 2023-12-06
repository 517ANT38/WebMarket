using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebMarket.Models.Adapters
{
    public class AdapterFindClient : IAdapterFind
    {
        string connection = ConfigurationManager.ConnectionStrings["MarketDB"].ConnectionString;
        public AuthorizedUser findCheck(EmailPassword emailPassword)
        {
            AuthorizedUser o = null;
            using (SqlConnection sql = new SqlConnection(connection))
            {
                sql.Open();
                SqlCommand cmd = sql.CreateCommand();
                cmd.CommandText = "SELECT * FROM Люди where E_mail like @email and Хэш_пароля like @pass";
                cmd.Parameters.AddWithValue("@email", emailPassword.Email);
                cmd.Parameters.AddWithValue("@pass", emailPassword.Password);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                    reader.Read();
                o = new AuthorizedUser()
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Family = reader["Фамилия"].ToString(),
                    Name = reader["Имя"].ToString(),
                    Patronymic = reader["Отечество"].ToString(),
                    Email = reader["E_mail"].ToString(),
                    Role = reader["Роль"].ToString()

                };


            }
            return o;
        }
    }
}