<%@ Page Title="Historia" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <p>Twoja historia<asp:GridView ID="GridView1" runat="server" 
           AutoGenerateColumns="False" DataSourceID="SqlDataSource1" 
           AllowPaging="True" AllowSorting="True">
       <Columns>
           <asp:BoundField DataField="Odbiorca" HeaderText="Odbiorca" 
               SortExpression="Odbiorca" />
           <asp:BoundField DataField="Tresc" HeaderText="Tresc" SortExpression="Tresc" />
           <asp:BoundField DataField="Data" HeaderText="Data" SortExpression="Data" />
           <asp:BoundField DataField="Godzina" HeaderText="Godzina" 
               SortExpression="Godzina" />
           <asp:BoundField DataField="Ilosc_dostarczonych" 
               HeaderText="Ilosc_dostarczonych" SortExpression="Ilosc_dostarczonych" />
           <asp:BoundField DataField="Ilosc_wyslanych" HeaderText="Ilosc_wyslanych" 
               SortExpression="Ilosc_wyslanych" />
       </Columns>
       </asp:GridView>
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:smscollectConnectionString %>" 
           SelectCommand="SELECT [Odbiorca], [Tresc], [Data], [Godzina], [Ilosc_dostarczonych], [Ilosc_wyslanych] FROM [tresc_sms]  WHERE (([imie] = @imie) AND ([nazwisko] = @nazwisko))">
       
       <SelectParameters>
        <asp:SessionParameter Name="imie" SessionField="name" Type="String" />
        <asp:SessionParameter Name="nazwisko" SessionField="lastname" Type="String" />
       </SelectParameters>
       </asp:SqlDataSource>
    </p>
            
    
</asp:Content>

