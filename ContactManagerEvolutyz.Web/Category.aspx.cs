using ContactManagerEvolutyz.Web.ContactServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ContactManagerEvolutyz.Web
{
    public partial class Category : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
            }
        }

        private void LoadCategories()
        {
            string sessionId = Session["SessionId"]?.ToString();

            if (string.IsNullOrEmpty(sessionId))
            {
                lblMessage.Text = "You need to log in to view categories.";
                return;
            }

            using (var client = new ContactServiceClient())
            {
                try
                {
                    var categories = client.GetCategories(sessionId);
                    gvCategories.DataSource = categories;
                    gvCategories.DataBind();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = $"Error: {ex.Message}";
                }
            }
        }

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            string sessionId = Session["SessionId"]?.ToString();
            string categoryName = txtCategoryName.Text.Trim();

            if (string.IsNullOrEmpty(sessionId))
            {
                lblMessage.Text = "You need to log in to add a category.";
                return;
            }

            if (string.IsNullOrEmpty(categoryName))
            {
                lblMessage.Text = "Category name cannot be empty.";
                return;
            }

            using (var client = new ContactServiceClient())
            {
                try
                {
                    bool success = client.AddCategory(sessionId, categoryName);

                    if (success)
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Text = "Category added successfully!";
                        txtCategoryName.Text = string.Empty;
                        categoryName= string.Empty;
                        LoadCategories();
                    }
                    else
                    {
                        lblMessage.Text = "Failed to add category.";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = $"Error: {ex.Message}";
                }
            }
        }

        protected void gvCategories_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCategories.EditIndex = e.NewEditIndex;
            LoadCategories();
        }

        protected void gvCategories_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCategories.EditIndex = -1;
            LoadCategories();
        }

        protected void gvCategories_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string sessionId = Session["SessionId"]?.ToString();
            int categoryId = Convert.ToInt32(gvCategories.DataKeys[e.RowIndex].Value);
            string categoryName = ((TextBox)gvCategories.Rows[e.RowIndex].Cells[1].Controls[0]).Text;

            if (string.IsNullOrEmpty(sessionId))
            {
                lblMessage.Text = "You need to log in to update a category.";
                return;
            }

            if (string.IsNullOrEmpty(categoryName))
            {
                lblMessage.Text = "Category name cannot be empty.";
                return;
            }

            using (var client = new ContactServiceClient())
            {
                try
                {
                    bool success = client.UpdateCategory(sessionId, categoryId, categoryName);

                    if (success)
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Text = "Category updated successfully!";
                        gvCategories.EditIndex = -1;
                        LoadCategories();
                    }
                    else
                    {
                        lblMessage.Text = "Failed to update category.";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = $"Error: {ex.Message}";
                }
            }
        }

        protected void gvCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sessionId = Session["SessionId"]?.ToString();
            int categoryId = Convert.ToInt32(gvCategories.DataKeys[e.RowIndex].Value);

            if (string.IsNullOrEmpty(sessionId))
            {
                lblMessage.Text = "You need to log in to delete a category.";
                return;
            }

            using (var client = new ContactServiceClient())
            {
                try
                {
                    bool success = client.DeleteCategory(sessionId, categoryId);

                    if (success)
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Text = "Category deleted successfully!";
                        LoadCategories();
                    }
                    else
                    {
                        lblMessage.Text = "Failed to delete category.";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = $"Error: {ex.Message}";
                }
            }
        }
    }
}