<%@ Page Title="Login" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.cs" Inherits="ContactManagerEvolutyz.Web.Login" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
     <body>

<%--         <form id="form1" runat="server">--%>
          <div style="text-align: center; margin-top: 50px;">
              <h2>Login</h2>
              <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
              <table style="margin: 0 auto;">
                  <tr>
                      <td><asp:Label ID="lblUsername" runat="server" Text="Username:" /></td>
                      <td><asp:TextBox ID="txtUsername" runat="server" /></td>
                  </tr>
                  <tr>
                      <td><asp:Label ID="lblPassword" runat="server" Text="Password:" /></td>
                      <td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" /></td>
                  </tr>
                  <tr>
                      <td colspan="2" style="text-align: center;">
                          <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                          <asp:HyperLink id="hyperlink1" NavigateUrl="~/Register.aspx" Text="Register Here" runat="server"/> 
                      </td>
                  </tr>
              </table>
          </div>
<%--  </form>--%>
    
     </body>
      
    </main>
</asp:Content>

<%--<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
</head>
<body>
    
</body>
</html>--%>
