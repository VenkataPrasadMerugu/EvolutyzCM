<%@ Page Title="Registration" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ContactManagerEvolutyz.Web.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <td><asp:Label ID="lblMessages" runat="server" Text="" /></td>
    <asp:Panel ID="RegistrationPanel" runat="server" CssClass="registration-panel">
        <table>
            <tr>
                <td><asp:Label ID="lblUserName" runat="server" Text="Username:" /></td>
                <td><asp:TextBox ID="txtUserName" runat="server" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblName" runat="server" Text="Name:" /></td>
                <td><asp:TextBox ID="txtName" runat="server" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblEmail" runat="server" Text="Email:" /></td>
                <td><asp:TextBox ID="txtEmail" runat="server" TextMode="Email" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblPassword" runat="server" Text="Password:" /></td>
                <td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" /></td>
            </tr>
            
            <tr>
                <td colspan="2" style="text-align: center;">
                    <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    
    

</asp:Content>
