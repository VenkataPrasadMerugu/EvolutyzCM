using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ContactManagerEvolutyz.Web.ContactServices;
//using ContactManagerEvolutyz.Web.ContactServiceReference;

namespace ContactManagerEvolutyz.Web
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
                LoadContacts();
            }
        }

        private void LoadCategories()
        {
            using (var client = new ContactServiceClient())
            {
                var categories = client.GetCategories(Session["SessionId"].ToString());
                ddlCategory.DataSource = categories;
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataValueField = "CategoryID";
                ddlCategory.DataBind();
            }
        }

        private void LoadContacts()
        {
            using (var client = new ContactServiceClient())
            {
                var contacts = client.GetContacts(Session["SessionId"].ToString());
                gvContacts.DataSource = contacts;
                gvContacts.DataBind();
            }
        }

        protected void btnAddContact_Click(object sender, EventArgs e)
        {
            var contact = new ContactManagerEvolutyz.Web.ContactServices.Contact
            {
                Name = txtName.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                CategoryID = int.Parse(ddlCategory.SelectedValue)
            };

            using (var client = new ContactServiceClient())
            {
                bool success = client.AddContact(Session["SessionId"].ToString(), contact);

                if (success)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Contact added successfully!";
                    LoadContacts();
                }
                else
                {
                    lblMessage.Text = "Failed to add contact.";
                }
            }
        }

        protected void gvContacts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvContacts.EditIndex = e.NewEditIndex;
            LoadContacts();
        }

        protected void gvContacts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvContacts.EditIndex = -1;
            LoadContacts();
        }

        protected void gvContacts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var contactId = (int)gvContacts.DataKeys[e.RowIndex].Value;
            var name = ((TextBox)gvContacts.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            var phone = ((TextBox)gvContacts.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            var email = ((TextBox)gvContacts.Rows[e.RowIndex].Cells[3].Controls[0]).Text;

            DropDownList ddlCategoryEdit = (DropDownList)gvContacts.Rows[e.RowIndex].FindControl("ddlCategoryEdit");

            var contact = new ContactManagerEvolutyz.Web.ContactServices.Contact
            {
                ContactID = contactId,
                Name = name,
                Phone = phone,
                Email = email,
                CategoryID = int.Parse(ddlCategoryEdit.SelectedValue)
            };

            using (var client = new ContactServiceClient())
            {
                client.UpdateContact(Session["SessionId"].ToString(), contact);
            }

            gvContacts.EditIndex = -1;
            LoadContacts();
        }

        protected void gvContacts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex == gvContacts.EditIndex)
            {
                // Find the DropDownList in the editing row
                DropDownList ddlCategoryEdit = (DropDownList)e.Row.FindControl("ddlCategoryEdit");

                if (ddlCategoryEdit != null)
                {
                    using (var client = new ContactServiceClient())
                    {
                        // Get categories and bind them to the dropdown
                        var categories = client.GetCategories(Session["SessionId"].ToString());
                        ddlCategoryEdit.DataSource = categories;
                        ddlCategoryEdit.DataTextField = "CategoryName";
                        ddlCategoryEdit.DataValueField = "CategoryID";
                        ddlCategoryEdit.DataBind();
                    }

                    // Set the selected value to the current category of the contact being edited
                    int categoryId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "CategoryID"));
                    ddlCategoryEdit.SelectedValue = categoryId.ToString();
                }
            }
        }


        protected void gvContacts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var contactId = (int)gvContacts.DataKeys[e.RowIndex].Value;

            using (var client = new ContactServiceClient())
            {
                client.DeleteContact(Session["SessionId"].ToString(), contactId);
            }

            LoadContacts();
        }
    }
}
