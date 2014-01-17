<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>
   

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

<!-- <script type="text/javascript">     function tmp()
     { Label.valueOf = TextBox1.valueOf; }</script>-->
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
   <div style="width: 54%; float: left; clear: right; margin-right: 2%; margin-left: 2em;">  
       <br />
       Do:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;              
    <asp:DropDownList ID="DropDownList1" runat="server" onselectedindexchanged="DropDownList1_SelectedIndexChanged">
        <asp:ListItem>Poniedziałek</asp:ListItem>
        <asp:ListItem Value="    DSTD - Struktury dyskretne, wykład - 1WA">    DSTD - Struktury dyskretne, wykład - 1WA - 15:30</asp:ListItem>
        <asp:ListItem>    DSTD - Struktury Dyskretne - 1CA - 17.15 </asp:ListItem>
        <asp:ListItem>Środa</asp:ListItem>
        <asp:ListItem>    DSEM - Seminarium magisterskie - 1 CA - 10:00</asp:ListItem>
        <asp:ListItem>    DSEM - Seminarium magisterskie - 1CB - 11:45</asp:ListItem>
    </asp:DropDownList>
       <br />
       <br />
    Wpisz wiadomość:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                 
    <asp:Button ID="szablon" runat="server" Text="Wybierz szablon" 
           onclick="wybierz_szablon" />
    <br />
    <br />
    <asp:TextBox ID="TextBox1" runat="server" Height="162px" Width="395px" 
           TextMode="MultiLine"></asp:TextBox>
                                
   
    <br />
    <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" 
        Text="Wyświetlane na tablicy" />
    <br />
                                      <br />
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            
    <asp:Button ID="Button2" runat="server" style="margin-left: 0px" Text="Wyślij" 
        Width="77px" />
  </div> 

 <div style= " background: url(/SMSCollect/telefon.png) no-repeat; width: 37%;
    float: left;
    clear: right;
    margin-right: 2%;" >
    
        <asp:Label 
         ID="Label1" runat="server" Text="Label" Height="600px" style=" margin-left: 15%; margin-top: 45%;" ></asp:Label>
 
     </div>
  

</asp:Content>
