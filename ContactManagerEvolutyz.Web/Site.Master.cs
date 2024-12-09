using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ContactManagerEvolutyz.Web
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the user is logged in
                if (Session["SessionId"] != null)
                {
                    // User is logged in
                    plhLogin.Visible = false;
                    plhLogout.Visible = true;
                    plhListOfContacts.Visible = true; // Show the "List of Contacts" link
                }
                else
                {
                    // User is not logged in
                    plhLogin.Visible = true;
                    plhLogout.Visible = false;
                    plhListOfContacts.Visible = false; // Hide the "List of Contacts" link
                }
            }
        }
    }
}