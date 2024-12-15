using ContactManagerEvolutyz.Web.ContactServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ContactManagerEvolutyz.Web
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the session ID from the session
            string sessionId = Session["SessionId"] as string;

            if (!string.IsNullOrEmpty(sessionId))
            {
                using (var client = new ContactServiceClient())
                {
                    // Call the WCF service's Logout method to invalidate the session
                    client.Logout(sessionId);
                }
            }

            // Clear the session
            Session.Clear();
            Session.Abandon();

            // Redirect to the login page
            Response.Redirect("~/Login.aspx");
        }
    }
}