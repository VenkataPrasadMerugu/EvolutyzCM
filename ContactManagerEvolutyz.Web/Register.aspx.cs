using ContactManagerEvolutyz.Web.ContactService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ContactManagerEvolutyz.Web
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string name = txtName.Text.Trim();
            string password = txtPassword.Text.Trim();
            string email = txtEmail.Text.Trim();

            // Validate inputs
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                lblMessages.Text = "All fields are required.";
                lblMessages.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                // Create a new User object
                var user = new User
                {
                    UserID = 0,
                    UserName = userName,
                    Name = name,
                    Password = password,
                    Email = email,
                    Active=true
                };


                // Create an instance of the service client
                using (ContactServiceClient client = new ContactServiceClient())
                {
                    // Call the service method to register the user
                    bool isRegistered = client.RegisterUser(user);

                    if (isRegistered)
                    {
                        lblMessages.Text = "User registered successfully!";
                        lblMessages.ForeColor = System.Drawing.Color.Green;

                        // Optional: Clear form fields
                        txtUserName.Text = string.Empty;
                        txtName.Text = string.Empty;
                        txtEmail.Text = string.Empty;
                        txtPassword.Text = string.Empty;
                        
                    }
                    else
                    {
                        lblMessages.Text = "Registration failed. Please try again.";
                        lblMessages.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error and display a user-friendly message
                lblMessages.Text = "An error occurred while processing your request. Please try again later.";
                lblMessages.ForeColor = System.Drawing.Color.Red;

                // Optional: Log the exception (not shown here)
                // Example: Logger.LogError(ex);
            }
        }
    }
}