using CM.DAC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.DAC
{
    public class ContactRepository
    {
        private readonly string connectionString;

        public ContactRepository()
        {
            this.connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Evolutyz_CM;User Id=Venkat;Password=Admin@123;TrustServerCertificate=True";
        }
        
            //"Data Source=.\\sqlexpress;Initial Catalog=Evolutyz_CM;Integrated Security=True;Trust Server Certificate=True";
              
        

        public bool RegisterUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Data.Users (UserName, Name, Password, Email, Active) VALUES (@UserName, @Name, @Password, @Email, 1)", conn);
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Password", user.Password); // Hash for production
                cmd.Parameters.AddWithValue("@Email", user.Email);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }

            return false;
        }


        public int login(string username, string password)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "SELECT UserId FROM Data.Users WHERE UserName = @Username AND Password = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password); // Use hashed passwords in production

                connection.Open();
                var result = command.ExecuteScalar();

                if (result != null)
                {
                    return Convert.ToInt32(result); 
                }
            }

            return 0; 
        }
    }
}
