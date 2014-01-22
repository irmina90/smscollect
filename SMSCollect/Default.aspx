<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>
   

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
<script language="javascript">
    function OpenChild() {
        var WinSettings = "center:yes;resizable:no;dialogHeight:500px"
        var MyArgs = window.showModalDialog("http://localhost:50058/SMSCollect/Szablon.aspx", MyArgs, WinSettings);
        if (MyArgs == null) {
            window.alert("Nothing returned from child. No changes made to input boxes");
            }
    }
</script>
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
<script type="text/javascript" src="/SMSCollect/js/podglad.js"></script>
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
        <body>
 
<P><BUTTON onclick="OpenChild()" type="button">Wybierz szablon</BUTTON></P>
</body>
    Wpisz wiadomość:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                 
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
         ID="Label1" runat="server" Height="600px" 
            style=" margin-left: 15%; margin-right: 15%; margin-top: 45%;" ></asp:Label>
 
     </div>
  

</asp:Content>

