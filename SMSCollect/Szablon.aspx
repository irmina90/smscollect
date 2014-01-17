<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Szablon.aspx.cs" Inherits="Szablon" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 217px;
        }
        .style2
        {
            width: 217px;
            height: 196px;
        }
        .style3
        {
            height: 196px;
            width: 199px;
        }
        .style4
        {
            width: 199px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border="2px"><tbody>
    <tr>
    <td class="style2"><asp:Label ID="szablon1" runat="server" 
            Text="Spóźnię się 15 minut. Stoję na przejeździe. Przepraszam"></asp:Label>
             <asp:Button ID="szablon11" runat="server" onclick="szablon11_Click" 
            Text="Wybierz" /> 
    <td class="style3"> Z przyczyn obiektywnych dzisiejsze zajęcia zostały odwołane. 
        Pozdrawiam Wojtyra-Tyrakowska.<asp:Button ID="szablon22" runat="server" Text="Wybierz" /></td>
    </tr>
    
    <tr>
    <td class="style1"> Drodzy studenci, dzisiejsza wejściówka została odwołana.<asp:Button ID="szablon44" runat="server" Text="Wybierz" /></td>
    <td class="style4">
     <asp:Label ID="szablon4" runat="server" Text="Dzisiejsze zajęcia odwołane."></asp:Label>
      <asp:Button ID="szablon33" runat="server" Text="Wybierz" />
     </td>
    </tr>
    </tbody></table>
    
        
       
       
   
    </form>
</body>
</html>
