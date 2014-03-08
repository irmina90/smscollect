<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logon.aspx.cs" Inherits="Logon" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="login:"></asp:Label>
        <asp:TextBox ID="loginText" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="hasło:"></asp:Label>
        <asp:TextBox ID="hasloText" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <br />
        <br />
    
    </div>
    <asp:Button ID="zaloguj" runat="server" onclick="zaloguj_Click" 
        Text="Zaloguj" />
    <br />
    <br />
    <br />
    <asp:Label ID="odpowiedzText" runat="server"></asp:Label>
    &nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="Przejdz do serwisu" />
    </form>
</body>
</html>
