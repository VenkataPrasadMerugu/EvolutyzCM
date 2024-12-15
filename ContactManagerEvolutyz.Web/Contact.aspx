<%@ Page Title="Manage Contacts" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Contact.aspx.cs" Inherits="ContactManagerEvolutyz.Web.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Manage Contacts</h2>
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />

    <!-- Add New Contact Form -->
    <table>
        <tr>
            <td>Name:</td>
            <td><asp:TextBox ID="txtName" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
        </tr>
        <tr>
            <td>Phone:</td>
            <td><asp:TextBox ID="txtPhone" runat="server" /></td>
        </tr>
        <tr>
            <td>Email:</td>
            <td><asp:TextBox ID="txtEmail" runat="server" /></td>
        </tr>
        <tr>
            <td>Category:</td>
            <td>
                <asp:DropDownList ID="ddlCategory" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnAddContact" runat="server" Text="Add Contact" OnClick="btnAddContact_Click" />
            </td>
        </tr>
    </table>

    <!-- GridView to List and Manage Contacts -->
    <h3>Contact List</h3>
    <asp:GridView ID="gvContacts" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical"
        DataKeyNames="ContactID"
        OnRowEditing="gvContacts_RowEditing"
        OnRowUpdating="gvContacts_RowUpdating"
        OnRowCancelingEdit="gvContacts_RowCancelingEdit"
        OnRowDeleting="gvContacts_RowDeleting"
        OnRowDataBound="gvContacts_RowDataBound">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
        <Columns>
            <asp:BoundField DataField="ContactID" HeaderText="Contact ID" ReadOnly="True" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Phone" HeaderText="Phone" />
            <asp:BoundField DataField="Email" HeaderText="Email" />

            <asp:TemplateField HeaderText="Category">
    <ItemTemplate>
        <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:DropDownList ID="ddlCategoryEdit" runat="server"></asp:DropDownList>
    </EditItemTemplate>
</asp:TemplateField>


          
            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
    <br />
    <br />
    
</asp:Content>
