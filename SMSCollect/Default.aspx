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
<link href="Styles/Site.css" rel="stylesheet" type="text/css" media="all and (max-width: 1024px)"/> 
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

            var autor = $("#MainContent_lUser").Text; 
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
            $("#MainContent_Label1").text(text);


            HideDialog();
            e.preventDefault();
        });

        $("#Button1").click(function (e) {
            PageMethods.Usun_szablon(onSuccessMethod, onFailMethod);
            HideDialog();
            e.preventDefault();
        });
        function onSuccessMethod(result) {
        }


        function onFailMethod(error) {
        }

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
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<asp:ScriptManager ID="ScriptMgr" runat="server" EnablePageMethods="true">
                </asp:ScriptManager>
    <br />
    <body>
    <asp:LoginView ID="LoginView1" runat="server">
     <AnonymousTemplate>
            <p>
                Zawartośc przeznaczona jedynie dla zalogowanych użytkowników. 
            </p>
        </AnonymousTemplate>
        <LoggedInTemplate>
    <!-- poczatek lewej kolumny -->
    <div class="left-column"> 
     Wysyłasz wiadomość jako:&nbsp;<asp:Label ID="lUser" runat="server" Text="Label"></asp:Label> 
    <p>Odbiorca:</p>           
    <asp:DropDownList class="list-subjects" ID="DropDownList1" runat="server" onselectedindexchanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
        <!-- 
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:smscollectConnectionString %>" 
            SelectCommand="SELECT [nazwa] FROM [grupa]"></asp:SqlDataSource> -->
   
<br />      

<!-- Pobieranie szablonów zapisanych w bazie danych -->
<asp:SqlDataSource 
     ID="SqlDataSourceTemplate" 
     runat="server" 
     ConnectionString="<%$ ConnectionStrings:smscollectConnectionString %>" 
     
                SelectCommand="SELECT [tresc], [imie], [nazwisko] FROM [szablony] WHERE (([imie] = @imie) AND ([nazwisko] = @nazwisko))">
    <SelectParameters>
        <asp:SessionParameter Name="imie" SessionField="name" Type="String" />
        <asp:SessionParameter Name="nazwisko" SessionField="lastname" Type="String" />
    </SelectParameters>
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
                      DataTextField="tresc"
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
            <input id="Button1" type="button" value="Usuń" />
         </td>
      </tr>
   </table>
</div>

 <input type="button" id="btnShowModal" value="Wybierz szablon" onclick="return btnShowModal_onclick()" style="margin-bottom: 1em" />
    <p>Wpisz wiadomość:</p>
    <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" class="box-sms" ></asp:TextBox>                
   
    <br />
    <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" 
        Text="Wyświetlane na tablicy" />
    <br />
    <asp:CheckBox ID="CheckBox2" runat="server" 
           Text="Zapisz jako szablon" />
    <br />
       <asp:Button ID="Button2" runat="server" Text="Wyślij" onclick="Button2_Click" class="button-send" />
       <br />
       <br />
       </div> <!-- koniec lewej kolumny -->

    <div class="phonePic">  <!-- zdjecie telefonu, tylko dekstop -->
        <asp:Label ID="Label1" runat="server" Height="600px" style=" margin-left: 15%; margin-right: 15%; margin-top: 45%;" ></asp:Label>
    </div>
    </LoggedInTemplate>
    </asp:LoginView> 
    </body>

</asp:Content>

