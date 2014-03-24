<%@ Page Title="Strona glowna" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>
   

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script language="javascript">
    function OpenChild() {
        var WinSettings = "center:yes;resizable:no;dialogHeight:500px"
        var MyArgs = window.showModalDialog("Szablon.aspx", MyArgs, WinSettings);
        if (MyArgs == null) {
            window.alert("Nothing returned from child. No changes made to input boxes");
            }
    }
</script>
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
<script type="text/javascript" src="/SMSCollect/js/podglad.js"></script>

<script src="scripts/jquery-1.4.3.min.js" type="text/javascript"></script>

<script type="text/javascript">

    $(document).ready(function () {
        $("#btnShowSimple").click(function (e) {
            ShowDialog(false);
            e.preventDefault();
        });

        $("#btnShowModal").click(function (e) {
            ShowDialog(true);
            e.preventDefault();
        });

        $("#btnClose").click(function (e) {
            HideDialog();
            e.preventDefault();
        });

        $("#btnSubmit").click(function (e) {

            var brand = $("#brands input:radio:checked").val();
            //$("#MainContent_TextBox1").text(brand);
            document.getElementById("MainContent_TextBox1").value = brand;

            var autor = "prof Abc";
            var text = $("#MainContent_TextBox1").val();
            text = text.replace(/ą/g, 'a').replace(/Ą/g, 'A')
                .replace(/ć/g, 'c').replace(/Ć/g, 'C')
                .replace(/ę/g, 'e').replace(/Ę/g, 'E')
                .replace(/ł/g, 'l').replace(/Ł/g, 'L')
                .replace(/ń/g, 'n').replace(/Ń/g, 'N')
                .replace(/ó/g, 'o').replace(/Ó/g, 'O')
                .replace(/ś/g, 's').replace(/Ś/g, 'S')
                .replace(/ż/g, 'z').replace(/Ż/g, 'Z')
                .replace(/ź/g, 'z').replace(/Ź/g, 'Z');
            $("#MainContent_Label1").text("Od "+autor +": "+text);


            HideDialog();
            e.preventDefault();
        });

    })(jQuery);

    function ShowDialog(modal) {
        $("#overlay").show();
        $("#dialog").fadeIn(300);

        if (modal) {
            $("#overlay").unbind("click");
        }
        else {
            $("#overlay").click(function (e) {
                HideDialog();
            });
        }
    }

    function HideDialog() {
        $("#overlay").hide();
        $("#dialog").fadeOut(00);
    }
       
</script>

<style type="text/css">

.web_dialog_overlay
{
   position: fixed;
   top: 0;
   right: 0;
   bottom: 0;
   left: 0;
   height: 100%;
   width: 100%;
   margin: 0;
   padding: 0;
   background: #000000;
   opacity: .15;
   filter: alpha(opacity=15);
   -moz-opacity: .15;
   z-index: 101;
   display: none;
}
.web_dialog
{
   display: none;
   position: fixed;
   width: 380px;
   height: 300px;
   top: 50%;
   left: 50%;
   margin-left: -190px;
   margin-top: -100px;
   background-color: #ffffff;
   border: 2px solid #336699;
   padding: 0px;
   z-index: 102;
   font-family: Verdana;
   font-size: 10pt;
}
.web_dialog_title
{
   border-bottom: solid 2px #336699;
   background-color: #336699;
   padding: 4px;
   color: White;
   font-weight:bold;
}
.web_dialog_title a
{
   color: White;
   text-decoration: none;
}
.align_right
{
   text-align: right;
}

</style>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
   <div style="width: 54%; float: left; clear: right; margin-right: 2%; margin-left: 2em;">  
       <br />
       Do:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;              
    <asp:DropDownList ID="DropDownList1" runat="server" 
           onselectedindexchanged="DropDownList1_SelectedIndexChanged">       
    </asp:DropDownList>
       <!-- 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:smscollectConnectionString %>" 
           SelectCommand="SELECT [nazwa] FROM [grupa]"></asp:SqlDataSource> -->
       <br />
       <br />
        <body>

        <!-- Tutaj wstawiam dziwny kod -->

   
<!-- <input type="button" id="btnShowModal" value="Modal Dialog" /> -->
   
<br /><br />      

<!-- Pobieranie szablonoów zapisanych w bazie danych -->
<asp:SqlDataSource 
     ID="SqlDataSourceTemplate" 
     runat="server" 
     ConnectionString="<%$ ConnectionStrings:smscollectConnectionString %>" 
     SelectCommand="SELECT tresc_szablonu FROM szablony">
 </asp:SqlDataSource>

<div id="output"></div>
   
<div id="overlay" class="web_dialog_overlay"></div>
   
<div id="dialog" class="web_dialog">
   <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="0">
      <tr>
         <td class="web_dialog_title">Szablony do wyboru.</td>
         <td class="web_dialog_title align_right">
            <a href="#" id="btnClose">Zamknij</a>
         </td>
      </tr>
      <tr>
         <td>&nbsp;</td>
         <td>&nbsp;</td>
      </tr>
      <tr>
         <td colspan="2" style="padding-left: 15px;">
         </td>
      </tr>
      <tr>
         <td>&nbsp;</td>
         <td>&nbsp;</td>
      </tr>
      <tr>
         <td colspan="2" style="padding-left: 15px;">
            <div id="brands">
                <asp:RadioButtonList
                      id="RadioButtonList1"
                      runat="server"
                      DataTextField="tresc_szablonu"
                      DataSourceID="SqlDataSourceTemplate">
                </asp:RadioButtonList>
            </div>
         </td>
      </tr>
      <tr>
         <td>&nbsp;</td>
         <td>&nbsp;</td>
      </tr>
      <tr>
         <td colspan="2" style="text-align: center;">
            <input id="btnSubmit" type="button" value="Wybierz" />
         </td>
      </tr>
   </table>
</div>

<!-- Tutaj koncze dziwny kod -->
 <input type="button" id="btnShowModal" value="Wybierz szablon" />
 <!--  <P><BUTTON onclick="OpenChild()" type="button">Wybierz szablon</BUTTON></P>  -->
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
    <asp:CheckBox ID="CheckBox2" runat="server" 
           Text="Zapisz jako szablon" />
    <br />
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            
    <asp:Button ID="Button2" runat="server" style="margin-left: 0px" Text="Wyślij" 
        Width="77px" onclick="Button2_Click" />
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

