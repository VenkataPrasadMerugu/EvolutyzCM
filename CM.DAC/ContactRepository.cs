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

        public int GetUserIdByUsername(string username)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                string query = "SELECT UserId FROM Data.Users WHERE UserName = @UserName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserName", username);

                connection.Open();
                var result = command.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        public bool AddCategory(string categoryName, int userId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Data.Categories (CategoryName, UserID, Active) VALUES (@CategoryName, @UserID, 1)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CategoryName", categoryName);
                command.Parameters.AddWithValue("@UserID", userId);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }


        public List<Category> GetCategoriesByUser(int userId)
        {
            var categories = new List<Category>();
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "SELECT CategoryID, CategoryName FROM Data.Categories WHERE UserID = @UserID AND Active = 1";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    categories.Add(new Category
                    {
                        CategoryID = (int)reader["CategoryID"],
                        CategoryName = reader["CategoryName"].ToString()
                    });
                }
            }
            return categories;
        }

        public bool DeleteCategory(int categoryID, int userId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Data.Categories WHERE CategoryID = @CategoryID AND UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CategoryID", categoryID);
                command.Parameters.AddWithValue("@UserID", userId);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool UpdateCategory(int categoryId, string categoryName, int userId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Data.Categories 
                         SET CategoryName = @CategoryName 
                         WHERE CategoryID = @CategoryID AND UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CategoryID", categoryId);
                command.Parameters.AddWithValue("@CategoryName", categoryName);
                command.Parameters.AddWithValue("@UserID", userId);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public List<Contact> GetContacts(int userId)
        {
            var contacts = new List<Contact>();
            using (var conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT c.ContactID, c.Name, c.Phone, c.Email, c.CategoryID, cat.CategoryName, c.UserID
                    FROM Data.Contacts c
                    JOIN Data.Categories cat ON c.CategoryID = cat.CategoryID
                    WHERE c.UserID = @UserID";

                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", userId);

                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    contacts.Add(new Contact
                    {
                        ContactID = (int)reader["ContactID"],
                        Name = reader["Name"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Email = reader["Email"]?.ToString(),
                        CategoryID = (int)reader["CategoryID"],
                        CategoryName = reader["CategoryName"].ToString(),
                        UserID = (int)reader["UserID"]
                    });
                }
            }
            return contacts;
        }

        public bool AddContact(Contact contact)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Data.Contacts (Name, Phone, Email, CategoryID, UserID) 
                                 VALUES (@Name, @Phone, @Email, @CategoryID, @UserID)";

                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", contact.Name);
                cmd.Parameters.AddWithValue("@Phone", contact.Phone);
                cmd.Parameters.AddWithValue("@Email", contact.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CategoryID", contact.CategoryID);
                cmd.Parameters.AddWithValue("@UserID", contact.UserID);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateContact(Contact contact)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Data.Contacts 
                                 SET Name = @Name, Phone = @Phone, Email = @Email, CategoryID = @CategoryID 
                                 WHERE ContactID = @ContactID";

                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ContactID", contact.ContactID);
                cmd.Parameters.AddWithValue("@Name", contact.Name);
                cmd.Parameters.AddWithValue("@Phone", contact.Phone);
                cmd.Parameters.AddWithValue("@Email", contact.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CategoryID", contact.CategoryID);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteContact(int contactId)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Data.Contacts WHERE ContactID = @ContactID";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ContactID", contactId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }


    }
}
