<%@ Page Title="Manage Categories" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Category.aspx.cs" Inherits="ContactManagerEvolutyz.Web.Category" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center; margin-top: 50px;">
        <h2>Manage Categories</h2>

        <!-- Add Category Section -->
        <h4>Add Category</h4>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
        <table style="margin: 0 auto;">
            <tr>
                <td>Category Name:</td>
                <td><asp:TextBox ID="txtCategoryName" runat="server" /></td>
                <td><asp:Button ID="btnAddCategory" runat="server" Text="Add" OnClick="btnAddCategory_Click" /></td>
            </tr>
        </table>

        <!-- Categories List Section -->
        <h4>Category List</h4>
        
        <asp:GridView ID="gvCategories" runat="server" AutoGenerateColumns="False" OnRowEditing="gvCategories_RowEditing" 
            OnRowCancelingEdit="gvCategories_RowCancelingEdit" OnRowUpdating="gvCategories_RowUpdating" OnRowDeleting="gvCategories_RowDeleting" 
            DataKeyNames="CategoryId" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black">
            <Columns>
                <asp:BoundField DataField="CategoryId" HeaderText="Category ID" ReadOnly="True" />
                <asp:BoundField DataField="CategoryName" HeaderText="Category Name" ReadOnly="False" />
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <RowStyle BackColor="White" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
    </div>
</asp:Content>
