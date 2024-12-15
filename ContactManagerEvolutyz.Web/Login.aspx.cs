using ContactManagerEvolutyz.Web.ContactServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ContactManagerEvolutyz.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Clear any previous authentication on page load
            if (!IsPostBack)
            {
                FormsAuthentication.SignOut();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            using (ContactServiceClient client = new ContactServiceClient())
            {
                var response = client.Login(username, password);

                if (response.Success)
                {
                    Session["SessionId"] = response.SessionId;
                    Session["UserId"] = response.UserId;
                    Session["Username"] = username;

                    // Redirect to default page
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    lblMessage.Text = response.Message;
                }
            }

        }
    }
}