<%@ Page Title="Historia" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <p>Twoja historia</p>
        <asp:Table ID="myTable" runat="server" Width="100%" Height="100%" 
            BorderStyle="Solid" GridLines="Both"> 
            <asp:TableRow style="font-weight: bold">
                <asp:TableCell>Odbiorca</asp:TableCell>
                <asp:TableCell>Tresc</asp:TableCell>
                <asp:TableCell>Data</asp:TableCell>
                <asp:TableCell>Godzina</asp:TableCell>
                <asp:TableCell>Ilosc dostarczonych</asp:TableCell>
            </asp:TableRow>
        </asp:Table>  
    
    
</asp:Content>

