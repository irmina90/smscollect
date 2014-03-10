<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Account_Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="MainContent">
    <asp:Label ID="Label1" runat="server" Text="Login:"></asp:Label>
    <asp:TextBox ID="loginText" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Hasło:"></asp:Label>
    <asp:TextBox ID="hasloText" runat="server" TextMode="Password"></asp:TextBox>
    <br />
    <br />
    <br />
    <asp:Label ID="odpowiedzText" runat="server"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="zaloguj" runat="server" onclick="zaloguj_Click" 
        Text="Zaloguj" />
</asp:Content>
