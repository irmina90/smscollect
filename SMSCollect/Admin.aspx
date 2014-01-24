<%@ Page Title="Konfiguracja" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Admin.aspx.cs" Inherits="About" %>






<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
<script type="text/javascript">
</script>

</asp:Content>




<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <p>Zarzadzanie uzytkownikami</p>
        <asp:Table ID="myTable" runat="server" Width="100%" Height="100%" 
            BorderStyle="Solid" GridLines="Both"> 
            <asp:TableRow style="font-weight: bold">
                <asp:TableCell>Imie</asp:TableCell>
                <asp:TableCell>Nazwisko</asp:TableCell>
                <asp:TableCell>Indeks</asp:TableCell>
                <asp:TableCell>Numer telefonu</asp:TableCell>
            </asp:TableRow>
        </asp:Table>  
    
    
</asp:Content>

